# Galaxy Calamity
My Game Software Engineering Disseration on a first person shooter project with a focus on Procedural Animation and NPC Behaviour.

This project was made in the Unity Game Engine(ver. 2021.2.1f). The game is a first person environment driven narrative shooter.
It has a number of features implemented:

- Procedural Animation (Using Inverse Kinematics and Animation Rigging)
- State Based AI System (Finite State Machine)
- Unity's Shader Graph
- Weaponary and Upgrade System
- Visuals rendered in Unity's Universal Render Pipeline
- Performance Testing Environment (For the procedural animation to record how this technique impacts performance)

Download the playable version here: https://leonlol-dev.itch.io/galaxy-calamity 

or

Download this repo's release build and execute galaxycalamity.exe.

## Stack
- [NavMeshComponent](https://github.com/Unity-Technologies/NavMeshComponents) - Unity's NavMeshComponents used for NavMeshAgents.

## Gameplay Video
[![youtube](https://img.youtube.com/vi/4h4_3-uvBak/0.jpg)](https://www.youtube.com/watch?v=4h4_3-uvBak)

(click the thumbnail to open the YouTube video)

## Procedural Animation

- Robotic Spider


https://user-images.githubusercontent.com/59918677/190015829-cfb0d2f1-6de1-4be6-a292-24c2dae5f802.mp4


- Robot Worm


https://user-images.githubusercontent.com/59918677/190016125-1dbe17f7-ba31-4dbe-8b01-47de941f6e98.mp4

## AI Behaviour

- Finite State Machine System

![statemachineumldiagram](https://user-images.githubusercontent.com/59918677/196291960-b3f4785e-03a9-4aff-b976-1da62574d565.png)

To build the states of the state machine, using abstraction to build a base or prototype abstract state that all concrete states derive from. This allows developers not to worry about the inner complexities of the base state and just worry about what each state individually does, increasing efficiency for development. The abstract state defines methods that will be used by all states, including: Start, Enter, Update, Fixed Update and OnTriggerEnter. Furthermore, we can use an abstract state as a variable within the state machine as the current state.


## Website
[leonlol-dev.github.io](https://leonlol-dev.github.io/Portfolio/index.html)
