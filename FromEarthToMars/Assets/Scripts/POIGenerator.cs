using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIGenerator : MonoBehaviour
{
    public GameObject POIPrefab = null;
    public Transform startPoint;
    public Transform endPoint;
    public int instanceCount = 15;
    public float minRange = 2.56f;
    public float maxRange = 10.24f;

    void Start()
    {
        Vector2 startPos = startPoint.position;
        Vector2 endPos = new Vector2(startPoint.position.x + Random.Range(minRange, maxRange), endPoint.position.y);

        for(int i = 0; i< instanceCount; i++) {
            GameObject lastGeneratedPOI = generatePoint(startPos, endPos);
            startPos = lastGeneratedPOI.transform.position;
            startPos.y = startPoint.position.y;
            endPos = new Vector2(startPos.x + Random.Range(minRange, maxRange), endPoint.position.y);
            if((endPos.x > endPoint.position.x) || (endPoint.position.x - endPos.x <= 1.28))
                break;
            
        }
        
    }

    GameObject generatePoint(Vector2 Start, Vector2 End) 
    {
        float positionX = Start.x;
        float positionY = Start.y;
        float ranW = Random.Range(0f, 359.9f);

        //positionX = Random.Range(Start.x, End.x);
        positionX = End.x;
        positionY = Random.Range(Start.y, End.y);
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