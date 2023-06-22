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
    public Image Crosshair;

    public static int distractions;

    public static bool isGameOver;
    public PlayerController player;
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
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
        if (Batteries == null) return;
        Batteries.text = batteryRemaining.ToString("000") + "%\n" + spareBatteries;
    }

    public void SetFlashlightBar(float batteryRemaining)
    {
        if (flashlight == null) return;
        flashlight.value = batteryRemaining;
    }

    void SetScoreText()
    {
        if (scoreText == null) return;
        scoreText.text = points.ToString("f0");
    }
    public void SetHoverText(string txt)
    {
        Debug.Log("tried to set hover");
        if (hoverText == null) return;
        hoverText.text = txt;
    }
    void SetStaminaBar()
    {
        if (staminaBar == null) return;
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
        StatTracker.hud.ShowCrosshair();
    }

    public static void UnlockAndShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StatTracker.hud.HideCrosshair();
    }
    [ContextMenu("Hide HUD")]
    public void HideHUD()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    [ContextMenu("Show HUD")]
    public void ShowHUD()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    public void HideCrosshair()
    {
        if (Crosshair == null) return;
        Crosshair.gameObject.SetActive(false);

    }
    public void ShowCrosshair()
    {
        if (Crosshair == null) return;
        Crosshair.gameObject.SetActive(true);
    }
}
