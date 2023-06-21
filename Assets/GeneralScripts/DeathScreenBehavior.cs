using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathScreenBehavior : MonoBehaviour
{

    public GameObject deathMenu;
    public static bool isInDeathScreen;
    private Color color = Color.clear;

    // Start is called before the first frame update
    void Start()
    {
        isInDeathScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver && LevelManager.playerDead && !isInDeathScreen)
        {
            print("Reached death screen.");
            DeathMenu();
        }
    }

    void DeathMenu()
    {
        isInDeathScreen = true;
        Time.timeScale = 0f;
        StatTracker.hud.HideHUD();
        this.deathMenu.SetActive(true);

        HUDManager.UnlockAndShowCursor();
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        this.deathMenu.SetActive(false);

        HUDManager.LockAndHideCursor();
        
        
        FindAnyObjectByType<LevelManager>().LoadCurrentLevel();
    }
}
