using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public POIGenerator poiGenScript;
    public SandstormGenerator sandGenScript;
    public SimpleCameraScroll cameraScrollScript;
    public ArrowManager arrowManScript;
    public GameObject cloudParticles;
    public RockGenerator rockGenScript;
    public CountDownTimer timer;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableGeneration()
    {
        poiGenScript.enabled = true;
        sandGenScript.enabled = true;
        cameraScrollScript.enabled = true;
        arrowManScript.enabled = true;
        rockGenScript.enabled = true;
        cloudParticles.SetActive(true);
        timer.enabled = true;
    }

    public void DisableGeneration()
    {
        poiGenScript.enabled = false;
        sandGenScript.enabled = false;
        cameraScrollScript.enabled = false;
        arrowManScript.enabled = false;
        rockGenScript.enabled = false;
        cloudParticles.SetActive(false);
        timer.enabled = false;
    }
}
