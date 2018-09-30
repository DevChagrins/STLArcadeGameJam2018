using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chagrins.Utility;

public class SandstormGenerator : MonoBehaviour
{
    public GameObject sandstormPrefab = null;
    public Camera mainCamera = null;
    public Transform startPoint = null;
    public Transform endPoint = null;

    public float minLifeTime = 10f;
    public float maxLifeTime = 30f;

    public float minSpeed = 0.32f;
    public float maxSpeed = 5.12f;

    public float buffer = 1f;

    private GameObject currentSandstorm = null;
    private Rect bounds;

    // Use this for initialization
    void Start()
    {
        bounds.min = new Vector2(startPoint.position.x, startPoint.position.y);
        bounds.max = new Vector2(endPoint.position.x, endPoint.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSandstorm == null)
        {
            GenerateSandstorm();
        }
    }

    void KillSandstorm()
    {
        currentSandstorm = null;
    }

    void GenerateSandstorm()
    {
        Vector2 randomPoint = Maths.RandomPointOnRect(bounds);
        Vector3 point = new Vector3(randomPoint.x, randomPoint.y, 0f);
        float speed = Random.Range(minSpeed, maxSpeed);
        float lifeTime = Random.Range(minLifeTime, maxLifeTime);
        // Probably make direction point towards a random point around the camera
        Vector3 direction = point - mainCamera.transform.position;
        direction.z = 0f;
        float dirSign = Mathf.Sign(direction.magnitude);
        direction = direction.normalized * -dirSign;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        currentSandstorm = GameObject.Instantiate(sandstormPrefab, point - (direction * -dirSign * buffer), rotation);

        Sandstorm sandstorm = currentSandstorm?.GetComponent<Sandstorm>();
        sandstorm.lifeTime = lifeTime;
        sandstorm.moveSpeed = speed;
        sandstorm.moveDirection = direction;
        sandstorm.destroyEvent = KillSandstorm;
        sandstorm.initialized = true;
    }
}
