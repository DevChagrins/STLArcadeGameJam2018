using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : MonoBehaviour {

	public GameObject ArrowPrefab;

	public float HeightOfDisplay;

	public Sprite UpSprite;
	public Sprite UpRightSprite;
	public Sprite RightSprite;
	public Sprite DownRightSprite;
	public Sprite DownSprite;
	public Sprite DownLeftSprite;
	public Sprite LeftSprite;
	public Sprite UpLeftSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateArrow(InputCommand inp) {
		Debug.Log (ArrowPrefab);
		GameObject newArrow = Instantiate (ArrowPrefab,FindObjectOfType<EscapeToQuit>().transform);
		newArrow.GetComponent<RectTransform>().localPosition = new Vector3 (286f,144f,0f);
		Vector3 dir = inp.direction;
		newArrow.GetComponent<Signal> ().Speed = HeightOfDisplay / inp.Delay;
		if (dir.y > 0f && dir.x == 0f) {
			newArrow.GetComponent<Image> ().sprite = UpSprite;
		} else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<Image> ().sprite = UpRightSprite;
		}  else if (dir.y == 0f && dir.x > 0f) {
			newArrow.GetComponent<Image> ().sprite = RightSprite;
		}  else if (dir.y < 0f && dir.x > 0f) {
			newArrow.GetComponent<Image> ().sprite = DownRightSprite;
		}  else if (dir.y < 0f && dir.x == 0f) {
			newArrow.GetComponent<Image> ().sprite = DownSprite;
		}  else if (dir.y < 0f && dir.x < 0f) {
			newArrow.GetComponent<Image> ().sprite = DownLeftSprite;
		}  else if (dir.y  == 0f && dir.x < 0f) {
			newArrow.GetComponent<Image> ().sprite = LeftSprite;
		}  else if (dir.y > 0f && dir.x < 0f) {
			newArrow.GetComponent<Image> ().sprite = UpLeftSprite;
		}
		newArrow.GetComponent<Signal> ().mInput = inp;
	}
}
