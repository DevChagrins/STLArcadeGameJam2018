using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chagrins {
	public class PlayerOffscreenControl : MonoBehaviour {

		public PlayerController pc;
		public float ScrollSpeed = 0.1f;
		public Vector2 MinCoord;
		public Vector2 MaxCoord;
		public Vector2 CenterCoord;
		public float xOff = 0f;
		public float ImageOffset = 1.5f;

		private bool ImageOn = false;

		public Vector3 PPos;

		public GameObject ChildArrow;
		private SimpleCameraScroll m_cameraScroll;
		// Use this for initialization
		void Start () {
			if (pc == null)
				pc = FindObjectOfType<PlayerController> ();
			ChildArrow = transform.Find ("Arrow").gameObject;
			m_cameraScroll = FindObjectOfType<SimpleCameraScroll> ();
		}
		
		// Update is called once per frame
		void Update () {
			if (pc == null)
				Destroy (gameObject);
			else {

				xOff = m_cameraScroll.transform.position.x;

				PPos = pc.transform.position;
				ImageOn = (PPos.x < MinCoord.x + xOff || PPos.x > MaxCoord.x + xOff ||
				PPos.y < MinCoord.y || PPos.y > MaxCoord.y);
				if (ImageOn)
					FindObjectOfType<CountDownTimer> ().ModifyTime (-Time.deltaTime * 2f);
			
				UpdateImage (ImageOn);
			}
		}

		void UpdateImage(bool On) {
			float a = Mathf.Atan2 (pc.transform.position.y - CenterCoord.y, pc.transform.position.x - (CenterCoord.x + xOff));
			if (On)
				transform.position = new Vector3 (xOff + CenterCoord.x + 1.5f * Mathf.Cos(a) , CenterCoord.y + 1.5f * Mathf.Sin(a), 0f);
			else
				transform.position = new Vector3(-10f,20f,0f);
			ChildArrow.transform.localRotation = Quaternion.Euler(0f,0f, a * Mathf.Rad2Deg);
		}
	}
}