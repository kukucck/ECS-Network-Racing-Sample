using MessagePack;
using MessagePack.Resolvers;
using UnityEngine;

namespace Unity.Entities.Racing.Serialization
{
    /// <summary>
    /// Initializes MessagePack serialization options for Unity.
    ///
    /// MessagePack-CSharp v3+ ships source generators that require the .NET 6+ Roslyn
    /// compiler host. Unity bundles its own older Roslyn version and does not run
    /// source generators as part of the Unity compilation pipeline, so the generated
    /// resolver code is never produced. Additionally, Unity's IL2CPP backend performs
    /// ahead-of-time (AOT) compilation which forbids any runtime code-generation or
    /// unrestricted reflection. Both constraints require the attribute-based approach
    /// shown here instead of the source-generator-based approach.
    ///
    /// Usage rules for defining MessagePack-serializable types in Unity:
    /// <list type="bullet">
    ///   <item>Annotate every serializable class or struct with <c>[MessagePackObject]</c>.</item>
    ///   <item>Annotate every serialized member with <c>[Key(int)]</c> (integer keys are
    ///         preferred over string keys for performance and size).</item>
    ///   <item>Prefer regular property <c>set</c> accessors over <c>init</c> accessors
    ///         to maximise compatibility across Unity LTS versions.</item>
    ///   <item>Prefer regular classes or structs over C# 9 <c>record</c> types to avoid
    ///         compiler-generated members that may interact unexpectedly with IL2CPP.</item>
    ///   <item>Add any serializable assembly to <c>link.xml</c> so IL2CPP does not strip
    ///         the types at build time.</item>
    /// </list>
    /// </summary>
    public static class MessagePackInitializer
    {
        private static bool s_initialized;

        /// <summary>
        /// Registers the <see cref="StaticCompositeResolver"/> and sets it as the default
        /// MessagePack resolver. Called automatically before the first scene is loaded.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            if (s_initialized)
                return;

            // StaticCompositeResolver is required for IL2CPP / AOT builds.
            // It avoids any dynamic code generation that is not allowed on
            // platforms like iOS or WebGL.
            // Do NOT use ContractlessStandardResolver here: it relies on
            // reflection-based member discovery which may be stripped by IL2CPP.
            StaticCompositeResolver.Instance.Register(
                StandardResolver.Instance
            );

            var options = MessagePackSerializerOptions.Standard
                .WithResolver(StaticCompositeResolver.Instance);

            MessagePackSerializer.DefaultOptions = options;

            s_initialized = true;
        }
    }
}
