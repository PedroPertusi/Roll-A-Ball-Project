using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    void Start()
    {
        // Ensure the offset is such that the camera is above the player
        // You might need to adjust the y-value to get the desired height
        offset = new Vector3(0, 10, 0); // Example offset, adjust the '10' as needed

        // Set the camera to look down at the player from the start
        transform.position = player.transform.position + offset;
        transform.LookAt(player.transform);
    }

    void LateUpdate()
    {
        // Follow the player with the offset
        transform.position = player.transform.position + offset;

        // Optionally keep the camera always looking at the player
        transform.LookAt(player.transform);
    }
}
