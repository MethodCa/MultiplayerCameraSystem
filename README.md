# Multiplayer Camera System

Camera system for 2D multiplayer games that updates the camera to position it on the centre of the 4 players in the X axis. The camera zoom in/out to maintain all players in the frame to a maximum Camera size provided as a parameter, it also reset the camera in the Y axis per every change in zoom to maintain correct camera flow. 
If one of the players is out of frame the camera zoom must resize to frame all players.
To achieve the correct position and zoom of the camera the system finds the X position where the camera should be positioned taking into account 4 players, this is done by finding the player that is situated at the most left, and most right and positioning the camera at the centre of them.


![cameraSystem2](https://github.com/MethodCa/MultiplayerCameraSystem/assets/15893276/6e0ac8f9-80a1-4301-844e-926424e28e9f)

To achive the change in zoom the Camera's orthographic size is re-calculated based in the palyers' position.

![cameraSystem](https://github.com/MethodCa/MultiplayerCameraSystem/assets/15893276/83eab164-8cd6-4201-b266-08969dacf0ce)
