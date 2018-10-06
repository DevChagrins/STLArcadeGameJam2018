using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

	public GameObject Results;
	public Text Time;
	public Text Disc;
	public Text Disc2;
	public HighScoreManager ScoreManager;
	bool GameEnding = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

	public void GameOver(float _totalTime, int _pickupCount, int _pickupCount2, bool SinglePlayer)
    {
		if (GameEnding)
			return;
		
		Results.SetActive (true);
		Time.text = "Time: " + _totalTime;
		Disc.text = "Player 1: " + _pickupCount;
		Disc2.text = "Player 2: " + _pickupCount2;
		ScoreManager.InitializeName (SinglePlayer, _totalTime);
		GameEnding = true;
    }
}
