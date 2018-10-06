using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigator : MonoBehaviour
{
    public enum MenuState
    {
        MAIN,
        HOWTO,
        RESULTS,
        GAME,
    }

    public MenuState menuState = 0;

    public GameObject mainScreen = null;
    public GameObject howToScreen = null;
    public GameObject resultsScreen = null;

    public LevelLoader levelLoader = null;
    public GenerationManager generationMan = null;

	public int NumPlayers = 2;

    private float delayForInput = 0;

    // Use this for initialization
    void Start()
    {
        menuState = MenuState.MAIN;
    }

    // Update is called once per frame
    void Update()
    {
        delayForInput -= Time.deltaTime;

        if (delayForInput < 0f)
        {
            if(menuState == MenuState.HOWTO)
            {
				//Debug.Log ("Loading game");
                levelLoader?.LoadGameScenes(GoToGame);
				FindObjectOfType<CountDownTimer> ().timeLeft = 90f;
				menuState = MenuState.GAME;
            }

            switch (menuState)
            {
			case MenuState.MAIN:
				{
					if (Input.GetKey(KeyCode.Period) || Input.GetKey( KeyCode.BackQuote)) {
						NumPlayers = 1;
						MenuTransition (MenuState.HOWTO, 5f);	
						FindObjectOfType<CountDownTimer> ().SinglePlayer = true;

					} else if (Input.GetKey(KeyCode.Slash) || Input.GetKey( KeyCode.L)) {
						NumPlayers = 2;
						MenuTransition (MenuState.HOWTO, 5f);
						FindObjectOfType<CountDownTimer> ().SinglePlayer = false;
						//PlayerPrefs.DeleteAll ();
					}

					break;
				}
                case MenuState.HOWTO:
                    {
                    }
                    break;
                case MenuState.RESULTS:
                    {
                        if (Input.anyKey)
                        {
                            MenuTransition(MenuState.MAIN, 0.1f);
                        }
                    }
                    break;
            }
        }
    }

    void MenuTransition(MenuState _newState, float _delayInput)
    {
        if(menuState != _newState)
        {
            menuState = _newState;
            delayForInput = _delayInput;

            switch (menuState)
            {
                case MenuState.MAIN:
                    {
                        GoToMain();
                        break;
                    }
                case MenuState.HOWTO:
                    {
                        GoToHowTo();
                        break;
                    }
                case MenuState.GAME:
                    {
                        GoToGame();
                        break;
                    }
            }
        }
    }

    public void GoToMain()
    {
        mainScreen.SetActive(true);
        howToScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void GoToHowTo()
    {
        mainScreen.SetActive(false);
        howToScreen.SetActive(true);
        resultsScreen.SetActive(false);
    }

    public void GoToGame()
    {
        //Debug.Log("Go To Game");
        mainScreen.SetActive(false);
        howToScreen.SetActive(false);
        resultsScreen.SetActive(false);

        generationMan?.EnableGeneration();
		if (NumPlayers == 1)
			Destroy (GameObject.Find ("Player2"));
        menuState = MenuState.GAME;
    }

    public void GoToResults()
    {
        MenuTransition(MenuState.RESULTS, 3f);
        mainScreen.SetActive(false);
        howToScreen.SetActive(false);
        resultsScreen.SetActive(true);

        generationMan.DisableGeneration();
        levelLoader.UnloadGameScene();
    }
}
