using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public GameObject controls;

    public void StartButton()
    {
        SceneManager.LoadScene("Ingame_UI");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void TitleScreenButton()
    {
        SceneManager.LoadScene("Title_Screen");
    }

    public void EndGameScreen()
    {
        SceneManager.LoadScene("End_Game_Screen");
    }

    public void showControls()
    {
        if(controls != null)
        {
            if (controls.activeInHierarchy)
            {
                controls.SetActive(false);
            }
            else
            {
                controls.SetActive(true);
            }
        }
    }

}
