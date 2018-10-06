using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeToQuit : MonoBehaviour {
    
	/*private static EscapeToQuit m_instance;

	public static EscapeToQuit Instance
	{
		get { return m_instance; }
		set { m_instance = value; }
	}

	void Awake()
	{

		if (m_instance == null)
		{
			m_instance = this;
		}
		else if (m_instance != this)
		{
			Destroy(gameObject);
			return;
		}

		//DontDestroyOnLoad(gameObject);

	}
*/
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
            Debug.Log("Application has been quit!");
        }
    }
}
