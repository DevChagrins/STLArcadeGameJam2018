using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 rotationSpeeds;
    public Vector3 startRotation;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(startRotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeeds * Time.deltaTime);
    }
}
