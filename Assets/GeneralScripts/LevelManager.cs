using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Text gameText;
    public static bool isGameOver = false;
    public static bool hasLevel1Keycard = false;


    //public AudioClip gameOverSFX;
    //public AudioClip gameWonSFX;

    //public string nextLevel;


    float countDown;


    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;

        //might have to edit how this bool is initialized when implementing more levels 
        hasLevel1Keycard = false;

    }

    // Update is called once per frame
    void Update()
    {


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


        /*
        if(!string.IsNullOrEmpty(nextLevel)){
          Invoke("LoadLevel", 2);
        }
        */

    }
    /*
    //load next level 
    void LoadLevel(){
        SceneManager.LoadScene(nextLevel);

    }
*/
    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
