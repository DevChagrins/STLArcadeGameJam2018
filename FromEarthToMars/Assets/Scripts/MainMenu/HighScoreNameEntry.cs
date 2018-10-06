using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreNameEntry : MonoBehaviour {

	bool Active;
	public string m_entryName;

	Text m_text;
	int m_charID;
	public int m_numChar;
	char m_currentChar;
	bool m_done = false;

	public float Score;
	public bool SinglePlayer;
	private char[] avaliableChars = new char[]
	{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',' '};

	// Use this for initialization
	void Start () {
		m_entryName = "";
		m_currentChar = 'A';
		m_charID = 0;
		m_numChar = 0;
		m_text = GetComponent<Text> ();
		//m_done = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_done)
			return;
		
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			m_charID = Mathf.Abs((m_charID + 1) % 27);
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			if (m_charID == 0)
				m_charID = 27;
			m_charID = Mathf.Abs((m_charID - 1) % 27);
		}
			
		m_currentChar = avaliableChars[m_charID];
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (m_numChar > 0) {
				m_entryName = m_entryName.Substring (0, m_entryName.Length - 1);
				m_numChar --;
			}
		} else if ((Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown( KeyCode.BackQuote)
			|| Input.GetKeyDown( KeyCode.RightArrow))) {
			if (m_numChar >= 2) {
				m_done = true;
				m_entryName += m_currentChar;
				FindObjectOfType<HighScoreManager> ().InsertEntry (SinglePlayer, m_entryName, Score);
				m_entryName = m_entryName.Substring (0, m_entryName.Length - 1);
			} else {
				m_entryName += m_currentChar;
			}
			m_numChar ++;
		}
		m_text.text = "Congratulations!\nEnter Name:\n" + m_entryName + m_currentChar;
	}

	public void ClearName() {
		Debug.Log ("Clearing name");
		GetComponent<Text> ().text = "";
		m_done = true;
	}
}
