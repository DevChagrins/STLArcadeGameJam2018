using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public GenerationManager genManager;
    public Camera mainCamera;
    public string[] gameScenes;
    private int sceneLoadedCount;

    public delegate void LevelLoadCallBack();
    private LevelLoadCallBack callbackDelegate;

    // Use this for initialization
    void Start()
    {
        sceneLoadedCount = 0;

        genManager.DisableGeneration();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadGameScenes(LevelLoadCallBack _callback)
    {
        if(gameScenes.Length > 0)
        {
            callbackDelegate = _callback;
            foreach(string scene in gameScenes)
            {
                AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                loadOperation.completed += new System.Action<AsyncOperation>(SceneLoaded);
            }
        }
    }

    public void SceneLoaded(AsyncOperation _operation)
    {
        if(_operation.isDone)
        {
            sceneLoadedCount++;

            if(sceneLoadedCount >= gameScenes.Length)
            {
                callbackDelegate?.Invoke();
            }
        }
    }

    public void SceneUnloaded(AsyncOperation _operation)
    {
        if (_operation.isDone)
        {
            sceneLoadedCount--;
        }
    }

    public void UnloadGameScene()
    {
        if (gameScenes.Length > 0)
        {
            foreach (string scene in gameScenes)
            {
                AsyncOperation loadOperation = SceneManager.UnloadSceneAsync(scene);
                loadOperation.completed += new System.Action<AsyncOperation>(SceneUnloaded);
            }
        }
    }
}
