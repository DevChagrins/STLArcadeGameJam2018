using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : MonoBehaviour
{
    public Vector3 moveDirection;
    public float moveSpeed;
    public float lifeTime;

    public delegate void Destroyed();
    public Destroyed destroyEvent = null;
    public bool initialized = false;

    private ParticleSystemRenderer particles;

    // Use this for initialization
    void Start()
    {
        particles = GetComponent<ParticleSystemRenderer>();
        lifeTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0f && (particles && !particles.isVisible))
            {
                destroyEvent?.Invoke();
                GameObject.DestroyImmediate(this);
            }
        }
    }
}
