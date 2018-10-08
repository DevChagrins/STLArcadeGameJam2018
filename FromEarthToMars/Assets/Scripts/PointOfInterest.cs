using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    private Collider2D pointCollider = null;
    public float timeValue = 10f;

    private bool selfDestruct = false;
    private float countdown;

    public delegate void DestroyMe(int id);
    private DestroyMe destroyCallback;
    private int poiID;

    // Use this for initialization
    void Start()
    {
        pointCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selfDestruct)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f)
            {
                destroyCallback.Invoke(poiID);
                GameObject.DestroyImmediate(this.gameObject);
            }
        }
    }

    public void DisableCollision()
    {
        if (pointCollider != null)
        {
            pointCollider.enabled = false;
        }
    }

    public void EnableSelfDestruction(float _countdownTime)
    {
        DisableCollision();
        countdown = _countdownTime;
        selfDestruct = true;
    }

    public void SetDestroyCallback(DestroyMe callback, int id)
    {
        destroyCallback = callback;
        poiID = id;
    }

    public float GetTimeValue()
    {
        return timeValue;
    }
}
