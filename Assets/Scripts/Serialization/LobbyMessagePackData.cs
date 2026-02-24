using MessagePack;

namespace Unity.Entities.Racing.Serialization
{
    // -------------------------------------------------------------------------
    // Example MessagePack-serializable types for the lobby / race system.
    //
    // Unity C# 9 compatibility rules applied here:
    //   1. [MessagePackObject] + [Key(int)] attributes – no source generators.
    //      Unity does not run source generators in its compiler pipeline,
    //      so the generated resolver used by MessagePack 3.x is never produced.
    //   2. Regular classes/structs – C# 9 "record" types work on Unity 2021.2+
    //      but may generate compiler-synthesised members that interact unexpectedly
    //      with IL2CPP; plain classes are safer and more portable.
    //   3. Regular property setters – "init" accessors work on Unity 2021.2+ but
    //      plain setters maximise compatibility across older LTS versions.
    //   4. No use of features that require the .NET 6+ Roslyn host
    //      (e.g. CallerArgumentExpression, required members).
    // -------------------------------------------------------------------------

    /// <summary>
    /// Represents a player's state inside the pre-race lobby, suitable for
    /// serialization with MessagePack over a custom transport layer.
    /// </summary>
    [MessagePackObject]
    public class PlayerLobbyState
    {
        /// <summary>Player display name. Should stay within 64 characters to match the
        /// <c>FixedString64Bytes</c> used by the ECS <c>PlayerName</c> component; that
        /// constraint is enforced at the ECS layer, not here.</summary>
        [Key(0)]
        public string PlayerName { get; set; }

        /// <summary>Index of the car skin chosen by the player.</summary>
        [Key(1)]
        public int SkinId { get; set; }

        /// <summary>Whether the player has pressed "Ready".</summary>
        [Key(2)]
        public bool IsReady { get; set; }
    }

    /// <summary>
    /// Represents the final result for one player at the end of a race.
    /// </summary>
    [MessagePackObject]
    public class RaceResult
    {
        /// <summary>Player display name.</summary>
        [Key(0)]
        public string PlayerName { get; set; }

        /// <summary>Total race time in seconds.</summary>
        [Key(1)]
        public float CompletionTime { get; set; }

        /// <summary>Finishing position (1 = first place).</summary>
        [Key(2)]
        public int Rank { get; set; }

        /// <summary>Round-trip latency in milliseconds at race end.</summary>
        [Key(3)]
        public int Ping { get; set; }
    }

    /// <summary>
    /// Snapshot of the full lobby, containing all connected players.
    /// Useful for persisting lobby state or sending it over a side-channel
    /// transport that is separate from Unity NetCode's ghost system.
    /// </summary>
    [MessagePackObject]
    public class LobbySnapshot
    {
        /// <summary>All players currently in the lobby.</summary>
        [Key(0)]
        public PlayerLobbyState[] Players { get; set; }

        /// <summary>Number of players who have marked themselves ready.</summary>
        [Key(1)]
        public int ReadyCount { get; set; }

        /// <summary>Whether the countdown to race start has begun.</summary>
        [Key(2)]
        public bool CountdownStarted { get; set; }
    }
}
