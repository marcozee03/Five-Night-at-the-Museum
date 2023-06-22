using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : AMenu
{
    public GameObject menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
            {
                Time.timeScale = 0;
                menu.SetActive(true);
                HUDManager.UnlockAndShowCursor();
                Camera.main.GetComponent<FirstPersonCamera>().DisableCameraMovement();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().DisableMovement();
                StatTracker.hud.HideHUD();
            }
            else
            {
                Back();
            }
        }


    }
    protected override void Back()
    {
        StatTracker.hud.ShowHUD();
        menu.SetActive(false);
        HUDManager.LockAndHideCursor();
        Camera.main.GetComponent<FirstPersonCamera>().EnableCameraMovement();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnableMovement();
        Time.timeScale = 1;
        //base.Back();

    }
    public void Leave() => Back();

    public AMenu settings;
    public void Settings()
    {
        settings.LoadAsNext(this);
    }
    public override void Unload()
    {
        menu.SetActive(false);
        this.enabled = false;
    }

    public override void LoadAsLast()
    {
        menu.SetActive(true);
        this.enabled = true;
    }
    public void Exit()
    {
        PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }
}
