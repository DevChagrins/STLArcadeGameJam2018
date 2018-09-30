using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

	public GameObject ArrowPrefab;

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

	public Signal CreateArrow(Vector3 dir) {
		GameObject newArrow = Instantiate (ArrowPrefab);

		if (dir.y > 0f && dir.x == 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpSprite;
		} else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}  else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}  else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}  else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}  else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}  else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}  else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}  else if (dir.y > 0f && dir.x > 0f) {
			newArrow.GetComponent<SpriteRenderer> ().sprite = UpRightSprite;
		}
		return newArrow.GetComponent<Signal> ();
	}
}
