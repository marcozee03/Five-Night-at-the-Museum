using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class FlashlightBehavior : MonoBehaviour
{


    public AudioClip flashlightOnSFX;
    public AudioClip flashlightOffSFX;
    public Light beam;

    public float batteryLife = 10;

    public Slider batterySlider;
    private bool IsFlashLightOn => this.beam.gameObject.activeSelf;
    // Start is called before the first frame update
    void Start()
    {
        SetLight(false);
        this.batterySlider.value = this.batteryLife;
    }

    // Update is called once per frame
    void Update()
    {
        ReduceBattery();
        this.batterySlider.value = this.batteryLife;
    }

    public void ToggleFlashLight(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.started)
        {
            Debug.Log("Flashlight button pressed");
            this.beam.gameObject.SetActive(!beam.isActiveAndEnabled);
            AudioSource.PlayClipAtPoint(IsFlashLightOn ? flashlightOffSFX : flashlightOnSFX, transform.position);
            if (batteryLife == 0)
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
            this.batteryLife = Mathf.Max(0, this.batteryLife - Time.deltaTime);
        }
    }
}
