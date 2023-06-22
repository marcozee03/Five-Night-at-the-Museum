using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Device;
public class StatTracker : MonoBehaviour
{


    // Response to the first button press. Calls our delegate
    // and then immediately stops listening.

    #region Switch Button Prompts
    public static bool OnController = false;
    private System.IDisposable m_EventListener;

    // When enabled, we install our button press listener.
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
        Debug.Log(control.device.description.deviceClass);
        //if(control.device.description.interfaceName)
        string deviceClass = control.device.description.deviceClass;
        OnController = !(deviceClass.Equals("Keyboard") || deviceClass.Equals("Mouse"));
        //Debug.Log(OnController ? "GamePad" : "keyboard") ;
    }
    #endregion
    private void Awake()
    {
        hud = GetComponentInChildren<HUDManager>();

    }
    public static HUDManager hud;
}
