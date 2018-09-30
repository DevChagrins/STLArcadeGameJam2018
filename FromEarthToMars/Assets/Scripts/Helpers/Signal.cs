using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal : MonoBehaviour {

	public float Speed;
	public InputCommand mInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TrailRenderer> ().time = mInput.duration;
		transform.position = new Vector3(transform.position.x, transform.position.y + Speed, transform.position.z) ;
		if (mInput.IsDead(Time.timeSinceLevelLoad))
			Destroy (gameObject);
	}
}
