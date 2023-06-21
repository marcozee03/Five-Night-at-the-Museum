using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class Flashlight : MonoBehaviour
{


    public AudioClip flashlightOnSFX;
    public AudioClip flashlightOffSFX;
    public Light beam;

    public float BatteryRemaining { get; private set; }
    public float batteryDrainRate = 5;
    public float spareBatteryRecharge = 50;
    [Min(0)]public int spareBatteries;
    private bool IsFlashLightOn => this.beam.gameObject.activeSelf;
    // Start is called before the first frame update
    void Start()
    {
        BatteryRemaining = 100;
        SetLight(false);
    }

    // Update is called once per frame
    void Update()
    {
        ReduceBattery();
        UpdateBatteryUI();
        
    }
    public void Reload(InputAction.CallbackContext context)
    {
        Debug.Log("reload pressed");
        if (context.started)
        {
            if (spareBatteries > 0 && BatteryRemaining < 100)
            {
                spareBatteries--;
                BatteryRemaining = Mathf.Min(100, BatteryRemaining + spareBatteryRecharge);
            }

        }
        UpdateBatteryUI();
    }
    public void UpdateBatteryUI()
    {
        StatTracker.hud.SetBatteryAmount(BatteryRemaining, spareBatteries);
        StatTracker.hud.SetFlashlightBar(BatteryRemaining);
    }

    public void ToggleFlashLight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            this.beam.gameObject.SetActive(!beam.isActiveAndEnabled);
            AudioSource.PlayClipAtPoint(IsFlashLightOn ? flashlightOffSFX : flashlightOnSFX, transform.position);
            if (BatteryRemaining == 0)
            {
                SetLight(false);
            }
        }
    }
    private void SetLight(bool lightOn)
    {
        beam.gameObject.SetActive(lightOn);
    }
    private void ReduceBattery()
    {
        if (IsFlashLightOn)
        {
            this.BatteryRemaining = Mathf.Max(0, this.BatteryRemaining - batteryDrainRate * Time.deltaTime);
            if(BatteryRemaining == 0)
            {
                SetLight(false);
            }
        }
    }
}
