using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SaveSensitivity : MonoBehaviour
{
    public TMP_InputField mouseSens;
    public TMP_InputField controllerSens;
    private void OnEnable()
    {
        mouseSens.text = PlayerPrefs.GetFloat("MouseSens", 1).ToString();
        controllerSens.text = PlayerPrefs.GetFloat("ControllerSens", 1).ToString();
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MouseSens", float.Parse(mouseSens.text));
        StatTracker.MouseSens = PlayerPrefs.GetFloat("MouseSens", 1);
        StatTracker.ControllerSens = PlayerPrefs.GetFloat("ControllerSens");
    }
}
