using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashlightBehavior : MonoBehaviour
{

    private bool flashlightOn;

    public Light beam;

    public float batteryLife = 10;

    public Slider batterySlider;

    // Start is called before the first frame update
    void Start()
    {
        this.flashlightOn = this.beam.gameObject.activeSelf;
        this.batterySlider.value = this.batteryLife;
    }

    // Update is called once per frame
    void Update()
    {

        if (this.IsFlashlightOn())
        {
            this.batteryLife = Mathf.Max(0, this.batteryLife - Time.deltaTime);
        }

        if (this.batteryLife == 0)
        {
            this.FlashlightOff();
        }

        if (Input.GetKeyDown(KeyCode.F) && this.IsFlashlightOn())
        {
            this.FlashlightOff();
        }
        else if (
            Input.GetKeyDown(KeyCode.F)
            && !this.IsFlashlightOn()
            && this.batteryLife > 0)
        {
            this.FlashlightOn();
        }

        this.batterySlider.value = this.batteryLife;
    }


    private void FlashlightOn()
    {
        this.beam.gameObject.SetActive(true);
    }

    private void FlashlightOff()
    {
        this.beam.gameObject.SetActive(false);
    }

    private bool IsFlashlightOn()
    {
        return this.beam.gameObject.activeSelf;
    }
}
