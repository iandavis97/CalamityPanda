using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public List<string> Levels;
    public int CurrentLevel;
    public bool InGame { get; set; }
    public static LevelManager Instance;
    private Camera cam;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        StartGame();
        Instance = this;
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
        GoToLevel(CurrentLevel + 1);
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
    }
}
