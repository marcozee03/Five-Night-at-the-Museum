using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject transitionPanel;
    public static bool isGameOver = false;
    public static bool clearConditionSatisfied= false;
    public static bool level2Solved = false;
    public static int currentLevel = 0;
    public static bool playerDead = false;
    public Image fadeToBlack;

    public int maxLevelCashAmt;
    public Text cashText;
    public Text pctCompleteText;
  
    public float fadeSpeed = 3.0f; // in seconds

    //public AudioClip gameOverSFX;
    //public AudioClip gameWonSFX;

    public string nextLevel;


    private float countDown;
    private Color color = Color.black;
    public GameObject deathScreen;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        playerDead = false;

        //might have to edit how this bool is initialized when implementing more levels 
        clearConditionSatisfied = false;
        level2Solved = false;
        currentLevel++;
        
        color.a = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        //fading to black inspired by https://forum.unity.com/threads/solved-how-can-i-change-a-value-smoothly-from-1-to-0-over-a-given-number-of-seconds.485260/
        if (isGameOver == true)
        {
            fadeToBlack.gameObject.SetActive(true);
            fadeToBlack.color = color;
            color.a = Mathf.MoveTowards(color.a, 1, (1 / fadeSpeed) * Time.deltaTime);
        }

    }

    





    public void LevelLost()
    {
        isGameOver = true;
        playerDead = true;

        // Camera.main.GetComponent<AudioSource>().pitch = 1;
        // AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        //LoadCurrentLevel();

    }
    public void LevelBeat()
    {

        isGameOver = true;

        //change pitch on last level 

        //AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);

        cashText.text = $"{HUDManager.points}";
        int pct = Mathf.RoundToInt((HUDManager.points / maxLevelCashAmt) * 100);
        pctCompleteText.text = $"{pct}%";
        
        if(!string.IsNullOrEmpty(nextLevel)){
          Invoke("LoadLevel", 12);
          Invoke("LevelTransitionScreen", 7);
        }
        

    }
    
    //load next level 
    void LoadLevel(){
        SceneManager.LoadScene(nextLevel);

    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void LevelTransitionScreen()
    {
        transitionPanel.SetActive(true);
    }
}
