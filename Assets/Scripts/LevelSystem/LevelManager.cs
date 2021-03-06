﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public List<string> Levels;
    public int CurrentLevel;
    public bool InGame { get; set; }
    public static LevelManager Instance;
    private Camera cam;
    public Canvas UI;
    private UIManager uiManager;
    public string VictoryScreen;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        StartGame();
        Instance = this;
        uiManager = UI.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update () {
        if (InGame)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
	}

    private void StartGame()
    {
        CurrentLevel = 0;
        InGame = true;

        StartCoroutine(ChangeLevel(CurrentLevel));
    }

    public void NextLevel()
    {
        if (CurrentLevel < Levels.Count - 1)
        {
            GoToLevel(CurrentLevel + 1);
        }
        else
        {
            SceneManager.LoadScene(VictoryScreen);
        }
    }

    public void Reload()
    {
        GoToLevel(CurrentLevel);
    }

    public void GoToLevel(int i)
    {
        if(enabled)
        {
            StartCoroutine(ChangeLevel(i));
        }
    }

    // Change levels without unloading the UI
    private IEnumerator ChangeLevel(int i)
    {
        UI.enabled = false;
        enabled = false;
        cam.enabled = true;
        Scene old = SceneManager.GetSceneByName(Levels[CurrentLevel]);
        if (old.isLoaded)
        {
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(Levels[CurrentLevel]));
        }
        CurrentLevel = i;
        yield return SceneManager.LoadSceneAsync(Levels[i], LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(Levels[i]));
        enabled = true;
        cam.enabled = false;
        UI.enabled = true;
        uiManager.LevelStart();
    }
}
