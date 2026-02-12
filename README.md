# ECS Network Racing

![ECSNetworkRacingHeader](https://user-images.githubusercontent.com/3436237/209012220-f888baf2-568b-4c06-bda2-8146333c76d6.jpg)

* Requires LFS, please clone the repository using LFS, do not download as a ZIP.
* Requires Unity 6000.2.11f1 or later.

Current tested version 6000.1.15f1
https://unity.com/releases/editor/whats-new/6000.2.11f1

Current Version project: *3.0.0*

## 📚 Learning Resources

**New to ECS?** Check out our comprehensive beginner's guides:
- **[ECS初学者指南 (Chinese)](./ECS初学者指南.md)** - 完整的 ECS 核心功能目录和学习路径
- **[ECS Beginners Guide (English)](./ECS-BEGINNERS-GUIDE.md)** - Complete ECS core features directory and learning path

These guides provide:
- Core ECS functionality directory structure
- Recommended code reading order for beginners
- Detailed explanations of Components, Systems, and Authoring
- Code examples and learning tips

# Unity Hub URL
unityhub://6000.2.11f1/7134d7685e5d

# Running the demo

* Open Multiplayer > Window: PlayMode Tools
* Select PlayMode Type -> Client Server
* Open scene "Project/Assets/Scenes/MainMenu.unity"
* Choose your car for the race.
* When you are in the lobby you can wait for other players or just press the Left Menu button and then Start button to start the race.

# New Features
* [Vehicle package implementation](https://docs.unity3d.com/Packages/com.unity.vehicles@0.1/manual/index.html)
* [Detailed Static Mesh Collision](https://docs.unity3d.com/Packages/com.unity.physics@1.4/manual/ghost-collisions.html)
* [New Input system](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.16/manual/index.html)
* [Multiplayer Play Mode tool](https://docs.unity3d.com/Packages/com.unity.multiplayer.playmode@2.0/manual/index.html)

# Known Issues

* Build times are taking much longer than expected. Since all the Shader Variants are being compiled, and not only the ones being used in the project.

# Controls
Keyboard: 

  Up Arrow / W: Throttle
  Down Arrow / S: Brake / Reverse
  Left and Right Arrows / A and D: Car Steering

# Features

* Netcode for Entities
* Baking
* ISystem
* Unity Physics
* Jobs
* Burst
* Vivox
