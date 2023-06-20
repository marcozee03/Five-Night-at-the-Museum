using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menu.activeSelf)
            {
                menu.SetActive(true);
                HUDManager.UnlockAndShowCursor();
                Camera.main.GetComponent<FirstPersonCamera>().DisableCameraMovement();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().DisableMovement();
            }
            else
            {
                menu.SetActive(false);
                HUDManager.LockAndHideCursor();
                Camera.main.GetComponent<FirstPersonCamera>().EnableCameraMovement();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().EnableMovement();
            }
        }

    }
}
