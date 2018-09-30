using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    private Collider2D pointCollider = null;

    // Use this for initialization
    void Start()
    {
        pointCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisableCollision()
    {
        if (pointCollider != null)
        {
            pointCollider.enabled = false;
        }
    }
}
