using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour {
    public float initialTime = 180f;
    private Text timerText;
	public float timeLeft;
	public float totalTime = 0f;
	public bool SinglePlayer = true;
    private int pickupCounter1;
	private int pickupCounter2;
    private GameObject masterObject = null;

	private float lastTime;

	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
        totalTime = timeLeft = initialTime;
        masterObject = GameObject.FindGameObjectWithTag("Master");
        pickupCounter1 = 0;
		pickupCounter2 = 0;
		lastTime = timeLeft;
	}
	
	// Update is called once per frame
	void Update () {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        } else
        {
            timeLeft = 0;
            EndGame();
        }
        var displaySeconds = System.Math.Ceiling(timeLeft);
        var displayMinutes = System.Math.Floor(displaySeconds / 60);
        displaySeconds = displaySeconds % 60;
		timerText.text = string.Format("{0}:{1}", displayMinutes, displaySeconds.ToString("00"));
		timeColor ();
    }

	private void timeColor() {
		float d = timeLeft - lastTime;
		if (d > 0f)
			timerText.color = Color.green;
		else if (d < (-Time.deltaTime * 2f))
			timerText.color = Color.red;
		else
			timerText.color = Color.white;
		lastTime = timeLeft;
	}
	public void ModifyTime(float delta) {
		timeLeft += delta;
	}
    public void AddTime(float _additionalTime)
    {
        timeLeft = timeLeft + _additionalTime;
        totalTime += _additionalTime;
    }
	public void AddPickUp(int numPickup, bool p1) {
		if (p1) {
			pickupCounter1 += numPickup;
		} else {
			pickupCounter2 += numPickup;
		}
	}


    // This should instead end the current round and unload levels
    void EndGame () {
		masterObject?.GetComponent<GameController>()?.GameOver(totalTime, pickupCounter1,pickupCounter2,SinglePlayer);
    }
}
