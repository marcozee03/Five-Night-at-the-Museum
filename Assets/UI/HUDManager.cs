using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //Total points player gathered 
    public static float points;
    public Text scoreText;
    public Slider staminaBar;
    public Image staminaFill;
    public Text hoverText;
    public Slider flashlight;
    public Text Batteries;

    public static int distractions;

    public static bool isGameOver;
    PlayerController player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        staminaBar.maxValue = player.maxStamina;
        flashlight.maxValue = 100;
    }
    void Update()
    {
        SetScoreText();
        SetStaminaBar();
    }
    public void SetBatteryAmount(float batteryRemaining, int spareBatteries)
    {
        Batteries.text = batteryRemaining.ToString("000") + "%\n" + spareBatteries;
    }

    public void SetFlashlightBar(float batteryRemaining)
    {
        flashlight.value = batteryRemaining;
    }

    void SetScoreText()
    {
        scoreText.text = points.ToString("f0");
    }
    public void SetHoverText(string txt)
    {
        hoverText.text = txt;
    }
    void SetStaminaBar()
    {

        staminaBar.value = player.CurrentStamina;
        if (player.Exhausted)
        {
            staminaFill.color = Color.red;
        }
        else if(DeathScreenBehavior.isInDeathScreen){
            staminaFill.color = Color.clear;
        }
        else
        {
            staminaFill.color = Color.green;
        }
    }

    public static void LockAndHideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //TODO: show Crosshair
    }

    public static void UnlockAndShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //TODO: hide crosshair
    }

}
