using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Results : MonoBehaviour {

	float cTime = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		cTime += Time.deltaTime;
		if (cTime > 5.0f)
			SceneManager.LoadScene ("UIScene");
	}
}
