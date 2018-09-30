using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chagrins {
	public class EndWarp : MonoBehaviour {

		public Vector3 StartPosition;
		public Vector3 StartPlayerPosition;
		private GameObject m_startPoint;
		// Use this for initialization
		void Start () {
			//m_startPoint = GameObject.Find ("StartPoint");
		}
		
		// Update is called once per frame
		void Update () {
		}

		internal void OnTriggerEnter2D(Collider2D c) {
			if (c.GetComponent<Camera> () != null) {
				Debug.Log("Am here");
				c.transform.position = StartPosition;
				FindObjectOfType<Player> ().transform.position = StartPlayerPosition;
			}
		}
	}
}
