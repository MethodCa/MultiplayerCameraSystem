using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    //private GameObject[] _playersList;
    public Camera mainCamera;
    public float minOrthographicSize = 3;
    public float maxOrthographicSize = 6;
    public float cameraOffsetX;
    public float cameraOffsetY;
    private Vector3 _pos;
    private float _lastDistanceX;
    private float _lastDistanceY;

    // Start is called before the first frame update
    void Start()
    {
        var position = transform.position;
        _pos = position;
    }

    // Update is called once per frame 
    void Update()
    {
        //this._playersList= GameObject.FindGameObjectsWithTag("Player");
        UpdateCamera();
    }

    /*
     * This function updates the camera allowing to position the camera on the center of the 4 players
     * in the X axis, zooming in/out to maintain all players in the frame to a maximum Camera size provided as a
     * requirement and repositioning the camera in the Y axis.
     *
     * If one of the players is out of frame in the Y axis the camera most resize to frame all players.
     */
    private void UpdateCamera()
    {
        /*
         *The following finds the X position where the camera should be positioned having into account 4 players,
         *this is done finding the player that is situated at the most left, and most right and positioning the
         *camera at the center of them.
         */
        // Get positions of all players to compare and get the player most in the left side and most in the right side.
        var position1 = this.player1.transform.position;
        var position2 = this.player2.transform.position;
        var position3 = this.player3.transform.position;
        var position4 = this.player4.transform.position;

        // Calculate the most left / right player with Mathf.Min and Mathf.Max, the values are stored in leftPlayer and rightPlayer.
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

        // Calculate the X position of the camera by subtracting the rightPlayer POS with the distance/2, that operation will return
        // the X position of the camera at the center of the action of the 4 Players.
        var centeredCameraX = rightPlayer - distanceX / 2;
        this._pos.x = centeredCameraX;
        // Offset the Y position of the camera to compensate zoom in/out 
        var centeredCameraY = (mainCamera.orthographicSize - cameraOffsetY);
        // Calculate POS Y depending if bottomCharacter is above or bellow 0
        this._pos.y = CalculateCameraPosY(bottomPlayer, centeredCameraY, distanceY);
        // Update the pos of the camera.
        transform.position = this._pos;
    }

    /*
     * Function to apply zoom to the camera, bool axis describes the axis where the zoom is being calculated,
     * false to X, true to Y.
     */
    private float CalculateZoomValue(float distanceX, float distanceY)
    {
        var zoomValueW = (distanceX + cameraOffsetX * 2) / (2 * mainCamera.aspect);
        var zoomValueH = (distanceY + cameraOffsetY * 2) / 2;
        var zoomValue = Mathf.Max(zoomValueH, zoomValueW);

        if (!(zoomValue <= maxOrthographicSize && zoomValue >= minOrthographicSize))
        {
            zoomValue = zoomValue > maxOrthographicSize
                ? maxOrthographicSize
                : minOrthographicSize;
        }
        return zoomValue;
    }

    private float CalculateCameraPosY(float bottomPlayer, float centeredCameraY, float distance)
    {
        var posY = centeredCameraY;
        if (!(bottomPlayer < 0)) return posY;
        posY = bottomPlayer + centeredCameraY;
        if (distance >= (mainCamera.orthographicSize * 2) - (cameraOffsetY * 2))
            posY = centeredCameraY * -1;
        return posY;
    }
}