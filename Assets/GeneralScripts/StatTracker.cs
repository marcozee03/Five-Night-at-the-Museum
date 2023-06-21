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
        MouseSens = PlayerPrefs.GetFloat("MouseSens");
        ControllerSens = PlayerPrefs.GetFloat("ControllerSens");
    }
    private void Start()
    {
       hud = GameObject.FindAnyObjectByType<HUDManager>();
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

    public static float MouseSens = 1;
    public static float ControllerSens = 1;

    public static HUDManager hud;
}
