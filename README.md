# Multiplayer Camera System for Unity 2D games

Camera system for 2D multiplayer games that updates the camera to position it on the centre of the 4 players in the X axis. The camera zoom in/out to maintain all players in the frame to a maximum Camera size provided as a parameter, it also reset the camera in the Y axis per every change in zoom to maintain correct camera flow. 
If one of the players is out of frame the camera zoom must resize to frame all players.
To achieve the correct position and zoom of the camera the system finds the X position where the camera should be positioned taking into account 4 players, this is done by finding the player that is situated at the most left, and most right and positioning the camera at the centre of them.


![cameraSystem2](https://github.com/MethodCa/MultiplayerCameraSystem/assets/15893276/6e0ac8f9-80a1-4301-844e-926424e28e9f)

To achive the change in zoom the Camera's orthographic size is re-calculated based in the palyers' position as shown in the following code block:
```c#
 // Calculate the most left / right player
 //with Mathf.Min and Mathf.Max, the values are stored in leftPlayer and rightPlayer.
 var leftPlayer = Mathf.Min(position1.x, position2.x, position3.x, position4.x);
 var rightPlayer = Mathf.Max(position1.x, position2.x, position3.x, position4.x);
 var topPlayer = Mathf.Max(position1.y, position2.y, position3.y, position4.y);
 var bottomPlayer = Mathf.Min(position1.y, position2.y, position3.y, position4.y);

 // Calculate the distance between leftPlayer and rightPlayer
 var distanceX = Vector2.Distance(new Vector2(leftPlayer, 0), new Vector2(rightPlayer, 0));
 // Calculate the distance between topPlayer and bottomPlayer
 var distanceY = Vector2.Distance(new Vector2(0, bottomPlayer), new Vector2(0, topPlayer));

 // Apply zoom incrementing/decrementing camera orthographic size.
 var newOrthographicSize = CalculateZoomValue(distanceX, distanceY);
 mainCamera.orthographicSize = newOrthographicSize;
```



![cameraSystem](https://github.com/MethodCa/MultiplayerCameraSystem/assets/15893276/83eab164-8cd6-4201-b266-08969dacf0ce)


> [!CAUTION]
> This system is still under development and could contain bugs/errors.
