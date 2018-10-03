using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DifficultyLevel {
	public Vector3 CamSpeed;
	public float Duration;
}

public class SimpleCameraScroll : MonoBehaviour {

	public Vector3 CamSpeed = new Vector3(1.0f,0f,0f);
	public float TotalXOffset = 0f;

	public List<DifficultyLevel> Levels;

	public Vector2 MaxCoords;

	private float m_timeSinceLastLevel;
	private DifficultyLevel m_currentLevel;
	private int index;

	void Start () {
		m_timeSinceLastLevel = 0f;
		SetLevel (Levels [0]);
		index = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		if (pos.x < MaxCoords.x && pos.y < MaxCoords.y)
			transform.position = pos + (CamSpeed * Time.deltaTime);
		TotalXOffset += (CamSpeed.x * Time.deltaTime);
		updateLevels ();
	}

	private void updateLevels() {
		m_timeSinceLastLevel += Time.deltaTime;
		if (m_timeSinceLastLevel > m_currentLevel.Duration) {
			if (index < Levels.Count)
				SetLevel (Levels [index]);
			index += 1;
		}
	}

	public void SetLevel(DifficultyLevel dl) {
		m_currentLevel = dl;
		CamSpeed = dl.CamSpeed;
		m_timeSinceLastLevel = 0f;
	}
}
