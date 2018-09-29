using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIGenerator : MonoBehaviour
{
    public GameObject POIPrefab = null;
    public Transform startPoint;
    public Transform endPoint;
    public int instanceCount = 15;

    void Start()
    {
        Vector2 startPos = startPoint.position;
        Vector2 endPos = new Vector2(startPoint.position.x + Random.Range(50, 50), endPoint.position.y);

        for(int i = 0; i< instanceCount; i++){
            GameObject lastGeneratedPOI = generatePoint(startPos, endPos);
        }
        
    }

    GameObject generatePoint(Vector2 Start, Vector2 End) 
    {
        float positionX = startPoint.position.x;
        float positionY = startPoint.position.y;
        float ranW = Random.Range(0f, 359.9f);

        positionX = Random.Range(startPoint.position.x, endPoint.position.x);
        positionY = Random.Range(startPoint.position.y, endPoint.position.y);
        GameObject POI = GameObject.Instantiate(POIPrefab, new Vector3(positionX, positionY, 0f), Quaternion.identity);

        // Set random rotation on POI object
        Transform rotatePOI = POI.GetComponent<Transform>();
        rotatePOI.Rotate(0f, 0f, ranW);
        return POI;
    }

    void Update()
    {

    }
}