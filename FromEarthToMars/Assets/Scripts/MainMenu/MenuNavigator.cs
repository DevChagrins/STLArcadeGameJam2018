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
    public GameObject gameScreen = null;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToResults()
    {
        mainScreen.SetActive(false);
        howToScreen.SetActive(false);
        resultsScreen.SetActive(true);
        mainScreen.SetActive(false);
    }
}
