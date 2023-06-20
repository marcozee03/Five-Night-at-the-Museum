using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FirstPersonCamera : MonoBehaviour
{
    Transform playerBody;

    //starting look "height"
    private float pitch = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        playerBody = transform.parent.transform;
    }
    void Start()
    {
        HUDManager.LockAndHideCursor();
    }
    float moveX, moveY;
    // Update is called once per frame
    private bool disabled;
    public void DisableCameraMovement()
    {
        disabled = true;
    }

    public void EnableCameraMovement()
    {
        disabled = false;
    }
    private void Update()
    {
        if (disabled) return;
            playerBody.Rotate(Vector3.up * moveX);
            pitch -= moveY;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    public void CameraMove(InputAction.CallbackContext context)
    {

        bool mouseSens = context.control.device.description.deviceClass.Equals("Mouse");
        Vector2 input = context.ReadValue<Vector2>();
        float Sensitivity = mouseSens ?  StatTracker.MouseSens : StatTracker.ControllerSens;
        moveX = input.x * Sensitivity;
        moveY = input.y * Sensitivity;
    }
}
