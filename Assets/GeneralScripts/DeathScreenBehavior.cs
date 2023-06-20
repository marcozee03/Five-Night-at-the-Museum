using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenBehavior : MonoBehaviour
{

    public GameObject deathMenu;

    private bool isInDeathScreen;

    // Start is called before the first frame update
    void Start()
    {
        this.isInDeathScreen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.isGameOver && !this.isInDeathScreen)
        {
            print("Reached death screen.");
            DeathMenu();
        }
    }

    void DeathMenu()
    {
        this.isInDeathScreen = true;
        Time.timeScale = 0f;
        this.deathMenu.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Respawn()
    {
        Time.timeScale = 1f;
        this.deathMenu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //FindAnyObjectByType<LevelManager>().LevelLost();
    }
}
