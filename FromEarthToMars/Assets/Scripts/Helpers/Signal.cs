using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Signal : MonoBehaviour {

	public float Speed;
	public InputCommand mInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TrailRenderer> ().time = mInput.duration;
		RectTransform rt = GetComponent<RectTransform> ();
		rt.localPosition = new Vector3(rt.localPosition.x, rt.localPosition.y - (Time.deltaTime * Speed), rt.localPosition.z) ;
		if (mInput.IsDead(Time.timeSinceLevelLoad))
			Destroy (gameObject);
	}
}
