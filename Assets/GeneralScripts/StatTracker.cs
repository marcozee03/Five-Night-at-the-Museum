using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Device;
public class StatTracker : MonoBehaviour
{
    private void Awake()
    {
        hud = GetComponentInChildren<HUDManager>();
        MouseSens = PlayerPrefs.GetFloat("MouseSens");
        ControllerSens = PlayerPrefs.GetFloat("ControllerSens");
    }
    InputAction actions;
    #region Switch Button Prompts
    public static bool OnController { get; private set; } = false;
    private System.IDisposable m_EventListener;
    void OnEnable()
    {
        // Start listening.
        m_EventListener =
            InputSystem.onAnyButtonPress
                .Call(OnButtonPressed);
    }
    private void OnDisable()
    {
        m_EventListener.Dispose();
    }
    private void OnButtonPressed(InputControl control)
    { 
        string deviceClass = control.device.description.deviceClass;
        OnController = !(deviceClass.Equals("Keyboard") || deviceClass.Equals("Mouse"));
    }

    #endregion

    public static float MouseSens;
    public static float ControllerSens;

    public static HUDManager hud;
}
