﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {
    public float initialTime = 180;
    private Text timerText;
    public float timeLeft { get; private set; }

	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
        timeLeft = initialTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        } else
        {
            timeLeft = 0;
            //EndGame();
        }
        var displaySeconds = System.Math.Ceiling(timeLeft);
        var displayMinutes = System.Math.Floor(displaySeconds / 60);
        displaySeconds = displaySeconds % 60;
        timerText.text = string.Format("{0}:{1}", displayMinutes, displaySeconds);
    }

	public void ModifyTime(float delta) {
		timeLeft += delta;
	}
    void EndGame () {
         #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
         #elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
         #else
         Application.Quit();
         #endif
    }
}
