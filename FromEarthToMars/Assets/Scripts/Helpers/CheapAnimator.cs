using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TO DO: FILL OUT LATER!!
public class CheapAnimator : MonoBehaviour
{
    public UnityEngine.UI.Image spriteRenderer;
    public Sprite[] spriteFrames;
    public float animationSpeed;
    private float animationTimer;
    private int currentFrame;

    // Use this for initialization
    void Start()
    {
        animationTimer = animationSpeed;
        spriteRenderer = GetComponent<UnityEngine.UI.Image>();
        currentFrame = 0;

        if (spriteRenderer)
        {
            spriteRenderer.sprite = spriteFrames[currentFrame];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTimer > 0f)
        {
            animationTimer -= Time.deltaTime;
        }

        if (animationTimer <= 0f)
        {
            animationTimer += animationSpeed;
            currentFrame++;

            if (currentFrame >= spriteFrames.Length)
            {
                currentFrame = 0;
            }
            else if (currentFrame < 0)
            {
                currentFrame = spriteFrames.Length - 1;
            }

            spriteRenderer.sprite = spriteFrames[currentFrame];
        }
    }
}