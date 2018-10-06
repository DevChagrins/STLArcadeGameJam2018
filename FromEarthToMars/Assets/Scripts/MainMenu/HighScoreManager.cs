using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ScoreEntry {
	public string mName;
	public float mTime;
}

public class HighScoreManager : MonoBehaviour {

	public List<ScoreEntry> HighSoloScores;
	public List<ScoreEntry> HighCoopScores;

	public GameObject SoloScoreList;
	public GameObject CoopScoreList;

	public GameObject NameEntry;
	public bool DoneDisplay = false;
	public float cTime = 0f;

	public GameObject EntryPrefab;


	// Use this for initialization
	void Start () {
		HighSoloScores = new List<ScoreEntry> ();
		HighCoopScores = new List<ScoreEntry> ();
		initializeEmptyScores ();
		initializeEntryList ();
		DoneDisplay = false;
	}

	void Update() {
		if (DoneDisplay) {
			cTime += Time.deltaTime;
			if (cTime > 5.0f)
				SceneManager.LoadScene ("UIScene");
		}
	}
	private void DisplayEntries() {
		if (DoneDisplay)
			return;
		//Debug.Log ("Dispalying....");
		DoneDisplay = true;
		cTime = 0f;
		for (int i = 0; i < 5; i++) {
			GameObject go = Instantiate (EntryPrefab, SoloScoreList.transform);
			ScoreEntry se = HighSoloScores [i];
			//Debug.Log (se.mName);
			go.GetComponent<Text> ().text = se.mName + "\t" + se.mTime;
		}
		for (int i = 0; i < 5; i++) {
			GameObject go = Instantiate (EntryPrefab, CoopScoreList.transform);
			ScoreEntry se = HighCoopScores [i];
			go.GetComponent<Text> ().text = se.mName + "\t" + se.mTime;
		}
	}

	private void initializeEmptyScores() {
		for (int i = 0; i < 5; i++) {
			if (!PlayerPrefs.HasKey("Solo-name"+i.ToString())) {
				//Debug.Log ("Initialized empty: " + i);
				PlayerPrefs.SetString ("Solo-name", "AAA");
				PlayerPrefs.SetFloat ("Solo-score", 10 - (i * 2));
			}
		}
		for (int i = 0; i < 5; i++) {
			if (!PlayerPrefs.HasKey("Coop-name"+i.ToString())) {
				PlayerPrefs.SetString ("Coop-name", "AAA");
				PlayerPrefs.SetFloat ("Coop-score", 10 - (i * 2));
			}
		}
	}

	private void initializeEntryList() {
		for (int i = 0; i < 5; i++) {
			ScoreEntry se = new ScoreEntry ();
			//Debug.Log ("Found thing: " + i + " as " +  PlayerPrefs.GetString("Solo-name"+i.ToString()));
			se.mName = PlayerPrefs.GetString("Solo-name"+i.ToString());
			se.mTime = PlayerPrefs.GetFloat("Solo-score"+i.ToString());
			HighSoloScores.Add (se);
		}
		for (int i = 0; i < 5; i++) {
			ScoreEntry se = new ScoreEntry ();
			se.mName = PlayerPrefs.GetString("Coop-name"+i.ToString());
			se.mTime = PlayerPrefs.GetFloat("Coop-score"+i.ToString());
			HighCoopScores.Add (se);
		}
	}

	private void SaveEntries() {
		//Debug.Log("1:" + HighSoloScores [0].mName);
		for (int i = 0; i < 5; i++) {
			ScoreEntry se = HighSoloScores[i];
			//Debug.Log ("Saving thing: " + "Solo-name"+i.ToString() + " as " +  se.mName);
			PlayerPrefs.SetString("Solo-name"+i.ToString(),se.mName);
			PlayerPrefs.SetFloat("Solo-score"+i.ToString(),se.mTime);
		}
		for (int i = 0; i < 5; i++) {
			ScoreEntry se = HighCoopScores[i];
			PlayerPrefs.SetString("Coop-name"+i.ToString(),se.mName);
			PlayerPrefs.SetFloat("Coop-score"+i.ToString(),se.mTime);
		}
	}
	public bool IsHighScore(bool SinglePlayer, float score) {
		initializeEntryList ();
		if (SinglePlayer) {
			return (score > HighSoloScores [4].mTime);
		} else {
			return (score > HighCoopScores [4].mTime);
		}
	}

	public void InsertEntry(bool SinglePlayer, string name, float score) {
		initializeEntryList ();
		if (SinglePlayer) {
			for (int i = 0; i < 5; i++) {
				if (score > HighSoloScores [i].mTime) {
					//Debug.Log ("Score inserted at: " + i);
					ScoreEntry se = new ScoreEntry ();
					se.mName = name;
					se.mTime = score;
					HighSoloScores.Insert ( i, se);
					break;
				}
			}
		} else {
			for (int i = 0; i < 5; i++) {
				if (score > HighCoopScores [i].mTime) {
					//Debug.Log ("Coop Score inserted at: " + i);
					ScoreEntry se = new ScoreEntry ();
					se.mName = name;
					se.mTime = score;
					HighCoopScores.Insert (i, se);
					break;
				}
			}
		} 
		//Debug.Log("1:" + HighSoloScores [0].mName);
		DisplayEntries ();

		SaveEntries();
		//Debug.Log("1:" + HighSoloScores [0].mName);
	}

	public void InitializeName(bool SinglePlayer, float score) {
		
		if (IsHighScore(SinglePlayer, score)) {
			NameEntry.SetActive(true);
			NameEntry.GetComponent<HighScoreNameEntry> ().Score = score;
			NameEntry.GetComponent<HighScoreNameEntry> ().SinglePlayer = SinglePlayer;
		} else {
			NameEntry.GetComponent<HighScoreNameEntry> ().ClearName ();
			DisplayEntries();
		}
	}

}
