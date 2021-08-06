using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float xBound;
    [SerializeField] private FloorTileSpawner floorTileSpawner;

    private void FixedUpdate()
    {
        // Visualize the boundaries in the SceneViewer or Debugger
        /*Vector3[] b = new[]
        {
            new Vector3(boundaries.x,boundaries.y,-2),
            new Vector3(boundaries.x+5,boundaries.y,-2),
            new Vector3(boundaries.x+5,boundaries.y-5,-2),
            new Vector3(boundaries.x,boundaries.y-5,-2)
        };
        Debug.DrawLine(b[0],b[1]);
        Debug.DrawLine(b[1],b[2]);
        Debug.DrawLine(b[2],b[3]);
        Debug.DrawLine(b[3],b[0]);*/
        
        // As long as the player stays in the boundary, the camera move with the player.
        // Upon leaving the boundary with the player, the camera stays at the last position.
        var playerPosition = player.transform.position;
        transform.position = new Vector3(Mathf.Clamp(playerPosition.x, xBound, floorTileSpawner.GetEndX()-8.9f), 0, -10);
    }
}
