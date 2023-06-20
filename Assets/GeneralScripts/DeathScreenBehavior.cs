using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DeathScreenBehavior : MonoBehaviour
{

    public GameObject deathMenu;
    public Image [] UIImages;
    public Text UIText;

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
        foreach(Image n in UIImages){
            
            n.color = color;
            color.a = 0; //redundant since Color.clear is 0,0,0?

        }
        UIText.color = color;
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
        
        
        FindAnyObjectByType<LevelManager>().LoadCurrentLevel();
    }
}
