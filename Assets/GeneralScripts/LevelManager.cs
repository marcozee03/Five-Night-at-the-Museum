using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text gameText;
    public static bool isGameOver = false;
    public static bool clearConditionSatisfied= false;
    public static bool viewingNumpad = false;
    public static bool level2Solved = false;
    public static int currentLevel = 0;
    public Image fadeToBlack;
  
    public float fadeSpeed = 3.0f; // in seconds

    //public AudioClip gameOverSFX;
    //public AudioClip gameWonSFX;

    public string nextLevel;


    private float countDown;
    private Color color = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

        //might have to edit how this bool is initialized when implementing more levels 
        clearConditionSatisfied = false;
        viewingNumpad = false;
        level2Solved = false;
        currentLevel++;
        
        color.a = 0;
        

    }

    // Update is called once per frame
    void Update()
    {
        //fading to black inspired by https://forum.unity.com/threads/solved-how-can-i-change-a-value-smoothly-from-1-to-0-over-a-given-number-of-seconds.485260/
        if(isGameOver == true){
            fadeToBlack.gameObject.SetActive(true);
            fadeToBlack.color = color;
            color.a = Mathf.MoveTowards(color.a, 1, (1/fadeSpeed) * Time.deltaTime);
            
        }

    }

    





    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";
        gameText.gameObject.SetActive(true);

        // Camera.main.GetComponent<AudioSource>().pitch = 1;
        // AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        Invoke("LoadCurrentLevel", 2);


    }
    public void LevelBeat()
    {

        isGameOver = true;
        if (gameText != null)
        {
            gameText.text = "YOU WIN!";
        }


        gameText.gameObject.SetActive(true);

        


        //change pitch on last level 

        //AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);


        
        if(!string.IsNullOrEmpty(nextLevel)){
          Invoke("LoadLevel", fadeSpeed + 0.5f); //extra 0.5f just in case fade to black takes longer
        }
        

    }
    
    //load next level 
    void LoadLevel(){
        SceneManager.LoadScene(nextLevel);

    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
