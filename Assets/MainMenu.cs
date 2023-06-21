using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : AMenu
{
    public AMenu settings;
    protected override void Back()
    {

    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel", 1));
    }

    public void Settings()
    {
        settings.LoadAsNext(this);
        Unload();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
