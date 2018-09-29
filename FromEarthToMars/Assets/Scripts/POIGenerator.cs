using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIGenerator : MonoBehaviour
{
    public GameObject POIPrefab = null;
    public Transform startPoint;
    public Transform endPoint;


    void Start()
    {
        float positionX = startPoint.position.x;
        float positionY = startPoint.position.y;

        
        GameObject POI = GameObject.Instantiate(POIPrefab, new Vector3(positionX, positionY, 0f), Quaternion.identity);
        
    }

    void Update()
    {

    }
}