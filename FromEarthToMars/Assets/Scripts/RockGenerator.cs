using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chagrins.Utility;

public class RockGenerator : MonoBehaviour
{
    public GameObject RockLongPrefab = null;
    public GameObject RockRoughPrefab = null;
    public GameObject RockTallPrefab = null;
    public GameObject RockLargePrefab = null;

    //public Canvas UIBorder;
    public Transform startPoint;
    public Transform endPoint;
    public int instanceCount = 15;
    public int onScreenCount = 0;
    public int passoverCount = 4;
    public float minRange = 2.56f;
    public float maxRange = 10.24f;
    

    void Start()
    {
        for(int r = 0; r < passoverCount; r++)
        {
            Vector2 startPos = startPoint.position;
            Vector2 endPos = new Vector2(startPoint.position.x + Random.Range(minRange, maxRange), endPoint.position.y);

            for (int i = 0; i < instanceCount; i++)
            {

                GameObject lastGeneratedRock = generatePoint(startPos, endPos);
                Vector3 size = lastGeneratedRock.gameObject.GetComponent<Collider2D>().bounds.size;
                Vector3 position = lastGeneratedRock.transform.position;

                Collider2D[] box = Physics2D.OverlapBoxAll(new Vector2(position.x, position.y), new Vector2(size.x, size.y), 0.3f);
                foreach (Collider2D cd in box)
                {

                    if (!cd.isTrigger && !cd.gameObject.CompareTag("Rock"))
                    {
                        GameObject.Destroy(lastGeneratedRock);
                        return;
                    }
                }
                startPos = lastGeneratedRock.transform.position;
                startPos.y = startPoint.position.y;
                endPos = new Vector2(startPos.x + Random.Range(minRange, maxRange), endPoint.position.y);
                if ((endPos.x > endPoint.position.x) || (endPoint.position.x - endPos.x <= 1.28))
                    break;
            }
        }
        
    }

    GameObject generatePoint(Vector2 Start, Vector2 End) 
    {
        float positionX = Start.x;
        float positionY = Start.y;
        float ranW = Random.Range(0f, 359.9f);

        positionX = End.x;
        positionY = Random.Range(Start.y, End.y);

        float r = Random.Range(0, 3);
        GameObject Rock = null;

        if(Maths.Equals(r, 2f))
        {
            Rock = GameObject.Instantiate(RockRoughPrefab, new Vector3(positionX, positionY, 0f), Quaternion.identity);
        }
        else if (Maths.Equals(r, 1f))
        {
            Rock = GameObject.Instantiate(RockLongPrefab, new Vector3(positionX, positionY, 0f), Quaternion.identity);
        }
        else if (Maths.Equals(r, 0f))
        {
            Rock = GameObject.Instantiate(RockLargePrefab, new Vector3(positionX, positionY, 0f), Quaternion.identity);
        }
        else 
        {
            Rock = GameObject.Instantiate(RockTallPrefab, new Vector3(positionX, positionY, 0f), Quaternion.identity);
        }

        // Set random rotation on Rock object
        Transform rotateRock = Rock.GetComponent<Transform>();
        rotateRock.Rotate(0f, 0f, ranW);
        return Rock;
    }

    void Update()
    {
        ////Find the object you're looking for
        //GameObject tempObject = GameObject.Find("UICanvas");
        //if (tempObject != null)
        //{
        //    //If we found the object , get the Canvas component from it.
        //    UIBorder = tempObject.GetComponent<Canvas>();
        //    Debug.Log("DID IT!!!!!!!!!!!!!!!!!!!");
        //    if (UIBorder == null)
        //    {
        //        Debug.Log("Could not locate Canvas component on " + tempObject.name);
        //    }
        //}
    }
}