using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] public float rotationSpeed = 30f; // Rotation speed in degrees per second


    void Update()

    {

        transform.Rotate(new Vector3(0, 0,rotationSpeed * Time.deltaTime));

    }

}
