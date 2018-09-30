using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

	public GameObject Results;
	public Text Time;
	public Text Disc;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver(float _totalTime, int _pickupCount)
    {
		Results.SetActive (true);
		Time.text = "Time: " + _totalTime;
		Disc.text = "Discoveries: " + _pickupCount;

    }
}
