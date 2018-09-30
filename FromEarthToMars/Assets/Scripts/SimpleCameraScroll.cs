using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraScroll : MonoBehaviour {

	public Vector3 CamSpeed = new Vector3(1.0f,0f,0f);

	public Vector2 MaxCoords;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (pos.x < MaxCoords.x && pos.y < MaxCoords.y)
			transform.position = pos + (CamSpeed * Time.deltaTime);
	}
}
