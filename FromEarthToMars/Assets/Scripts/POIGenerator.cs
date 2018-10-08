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

    public Transform viewStart;
    public Transform viewEnd;

    private List<PointOfInterest> points;
    private List<int> availableId;

    private bool checkPoints;

    // 3 seconds to start
    private float timer = 5f;

    void Start()
    {
        checkPoints = false;
        points = new List<PointOfInterest>();
        availableId = new List<int>();

		Generate ();
    }

	public void Generate() {
		Vector2 startPos = startPoint.position;
		Vector2 endPos = new Vector2(startPoint.position.x + Random.Range(minRange, maxRange), endPoint.position.y);

		for(int i = 0; i< instanceCount; i++) {
            // Create Point
			GameObject lastGeneratedPOI = GeneratePoint(startPos, endPos);

            // Set a callback
            PointOfInterest poiComp = lastGeneratedPOI.GetComponent<PointOfInterest>();
            poiComp.SetDestroyCallback(RemovePoint, points.Count);
            points.Add(poiComp);

            // Get the next point
            startPos = lastGeneratedPOI.transform.position;
			startPos.y = startPoint.position.y;

			endPos = new Vector2(startPos.x + Random.Range(minRange, maxRange), endPoint.position.y);
			if((endPos.x > endPoint.position.x) || (endPoint.position.x - endPos.x <= 1.28))
				break;
		}
	}

    void RemovePoint(int id)
    {
        points[id] = null;
        availableId.Add(id);
        checkPoints = true;
    }

    GameObject GeneratePoint(Vector2 Start, Vector2 End) 
    {
        float positionX = Start.x;
        float positionY = Start.y;
        float ranW = Random.Range(0f, 359.9f);

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
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer = 0.5f;
            checkPoints = true;
        }

        int viewCount = 0;
        bool needsPoint = false;
        if (checkPoints)
        {
            needsPoint = true;
            foreach (PointOfInterest point in points)
            {
                if (point != null)
                {
                    if (point.gameObject.GetComponent<Renderer>().isVisible)
                    {
                        viewCount++;
                    }

                    if (viewCount >= 2)
                    {
                        needsPoint = false;
                        break;
                    }
                }
            }
        }

        if (needsPoint)
        {
            Vector2 viewStartPoint = new Vector2(viewStart.position.x, viewStart.position.y);
            Vector2 viewEndPoint = new Vector2(viewEnd.position.x, viewEnd.position.y);
            for (int index = 0; index < 2 - viewCount; index++)
            {
                GameObject lastGeneratedPOI = GeneratePoint(viewStartPoint, viewEndPoint);
                PointOfInterest poiComp = lastGeneratedPOI.GetComponent<PointOfInterest>();
                int poiID = 0;
                if (availableId.Count > 0)
                {
                    poiID = availableId[0];
                    availableId.RemoveAt(0);
                }
                else
                {
                    poiID = points.Count;
                }

                poiComp.SetDestroyCallback(RemovePoint, poiID);
                points.Add(poiComp);
            }
        }
    }
}