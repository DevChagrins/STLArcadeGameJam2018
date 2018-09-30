﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signal : MonoBehaviour {

	public float Speed;
	public InputCommand mInput;
	public float Delay;

	private float m_time_alive = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TrailRenderer> ().time = mInput.duration;
		RectTransform rt = GetComponent<RectTransform> ();
		m_time_alive += Time.deltaTime;
		if (m_time_alive < Delay) {
			rt.localPosition = new Vector3 (rt.localPosition.x, rt.localPosition.y - (Time.deltaTime * Speed), rt.localPosition.z);
		} else if (GetComponent<Image> () != null) {
			Destroy (GetComponent<Image> ());
		}
		if (mInput.IsDead(Time.timeSinceLevelLoad))
			Destroy (gameObject);
	}
}
