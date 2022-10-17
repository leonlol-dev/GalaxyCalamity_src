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

Achieving procedural animation for the spider boss of this project was to first utilise Unity’s Animation Rigging package, once we have the package installed we need to apply some of the features to our spider model. Firstly, the spider model requires a rig; Rigs acts as a container for source objects and constraints that modify the animated hierarchy’s pose, there’s a variable we can change called weight, which controls the contribution of weight to the final pose (Unity 3D, 2022).  Once the rig is setup, the Inverse Kinematic two boned constraint is then applied to each leg. 

- Robotic Spider


https://user-images.githubusercontent.com/59918677/190015829-cfb0d2f1-6de1-4be6-a292-24c2dae5f802.mp4

The movement of the worm uses Cinemachine features which originally was meant for cameras within Unity, we use the Cinemachine Dolly Cart and Cinemachine Dolly Track; these features were meant to move cameras around during a cutscene in a cinematic style by using dolly path which is an array of waypoints within a scene and the dolly cart is a component which constrains the transform of the game object to the path (Unity, 2022). However, in this project we’ve reinvented the purpose of these two features for the movement of the worm (See Figure 13). The worm contains a script that has a FindPath() function, this function finds two random vector3 locations, these two location will be the start and end of the track. And resets the position of the worm after finding a path. The dolly cart is placed on the worms model, a Move() function is used to move the dolly cart by having a IEnumerator function which the dolly cart will follow the path then finds a new path at the end of the function, using the yield return new WaitUntil() function, the script waits until the position of the worm is at the end of the track. Once its at the end FindPath() function is called.

- Robot Worm


https://user-images.githubusercontent.com/59918677/190016125-1dbe17f7-ba31-4dbe-8b01-47de941f6e98.mp4

## AI Behaviour

- Finite State Machine System

![statemachineumldiagram](https://user-images.githubusercontent.com/59918677/196291960-b3f4785e-03a9-4aff-b976-1da62574d565.png)

To build the states of the state machine, using abstraction to build a base or prototype abstract state that all concrete states derive from. This allows developers not to worry about the inner complexities of the base state and just worry about what each state individually does, increasing efficiency for development. The abstract state defines methods that will be used by all states, including: Start, Enter, Update, Fixed Update and OnTriggerEnter. Furthermore, we can use an abstract state as a variable within the state machine as the current state.

Concrete States are the individual states that have their own behaviours and qualities. They can contain their own methods and variables, additionally they will have the abstract state’s methods as well. A drawback to using this method is that since the states are using the state class and they do not have access to the MonoBehaviour class functionality, which is why we need to pass in data from a MonoBehaviour class. A MonoBehaviour class is the base class in which every Unity Script derives from (Unity, 2022). The actual state machine will be of class MonoBehaviour within Unity. 
The actual MonoBehaviour class state machine does a number of things to allow this state machine to work, firstly it creates instances of the concrete states. Additionally, it passes data to the current state. Furthermore, it holds the reference to the current state – as a state machine can only be using one state at a time. It also where the transition function resides meaning that all states can transition as all states have access to the function.



## Website
[leonlol-dev.github.io](https://leonlol-dev.github.io/Portfolio/index.html)
