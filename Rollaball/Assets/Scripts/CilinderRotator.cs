using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderRotator : MonoBehaviour
{
    public float rotationSpeed = 45.0f; // Rotation speed in degrees per second

    // Update is called once per frame
    void Update()
    {
        // Rotate around the Z-axis at the specified speed
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
