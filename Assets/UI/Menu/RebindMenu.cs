using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class RebindMenu : AMenu
{
    public InputActionAsset actions;
    public TMP_InputField mouseSens;
    public TMP_InputField controllerSens;
    public GameObject Overlay;
    private bool overriden = false;
    private void Start()
    {
        if (!overriden)
        {
            gameObject.SetActive(false);
            overriden = true;
        }
    }

    public void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
        mouseSens.text = PlayerPrefs.GetFloat("MouseSens", 1).ToString();
        controllerSens.text = PlayerPrefs.GetFloat("ControllerSens", 1).ToString();
    }

    public void OnDisable()
    {
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        PlayerPrefs.SetFloat("MouseSens", float.Parse(mouseSens.text));
        StatTracker.MouseSens = PlayerPrefs.GetFloat("MouseSens", 1);
        StatTracker.ControllerSens = PlayerPrefs.GetFloat("ControllerSens", 1);
    }
    private bool RebindInProgress = false;
    private void Update()
    {       
        if (!RebindInProgress && module.input.GetButtonDown("Cancel"))
        {
            print(Overlay.activeSelf);
                Back();
        }
        RebindInProgress = Overlay.activeSelf;
    }
}
