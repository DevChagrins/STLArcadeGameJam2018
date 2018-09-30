using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputCommand {
	public float ActiveTime = 0.0f;
	public Vector3 direction = new Vector3();
	public float duration = 0.0f;
	public float Delay = 0.0f;

	public bool IsActive(float time) {
		return (time > ActiveTime && time <= ActiveTime + duration);
	}
	public bool IsDead(float time) {
		return (time >= ActiveTime + duration);
	}
}
