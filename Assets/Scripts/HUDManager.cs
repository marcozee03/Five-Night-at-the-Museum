using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //Total points player gathered 
    public static float points;
    //TODO: Make stamina a bar rather than number 
    //Actual changing stamina value for player
    public Text scoreText;
    public Slider staminaBar;
    public Image staminaFill;
    public Text hoverText;

    public static int distractions;

    public static bool isGameOver;
    PlayerController player;
    // Update is called once per frame
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        staminaBar.maxValue = player.maxStamina;
    }
    void Update()
    {
        SetScoreText();
        SetStaminaText();
    }

    void SetScoreText(){

        scoreText.text = points.ToString("f0");
    }
    public void SetHoverText(string txt)
    {
        hoverText.text = txt;
    }
    void SetStaminaText(){

        staminaBar.value = player.CurrentStamina;
        if (player.Exhausted)
        {
            staminaFill.color = Color.red;
        }
        else
        {
            staminaFill.color = Color.green;
        }
    }

 
}
