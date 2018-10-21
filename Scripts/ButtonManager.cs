using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

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

}
