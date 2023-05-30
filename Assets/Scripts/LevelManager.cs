using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //Total points player gathered 
    public static float points;
    //TODO: Make stamina a bar rather than number 
    //Actual changing stamina value for player
    public Text scoreText;
    public Text staminaText;

    public static int distractions;

    public static bool isGameOver;

    // Update is called once per frame
    void Update()
    {
        SetScoreText();
        SetStaminaText();
    }

    void SetScoreText(){

        scoreText.text = points.ToString("f0");
    }

    void SetStaminaText(){

        staminaText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().currentStamina.ToString("f0");
    }

 
}
