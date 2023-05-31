using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    Transform playerBody;
    public float Sensitivity = 1;

    //starting look "height"
    private float pitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        //getting reference to parent object
        playerBody = transform.parent.transform;

        //hiding and locking cursor 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    float moveX, moveY;
    // Update is called once per frame
    private void Update()
    {
        playerBody.Rotate(Vector3.up * moveX);
        pitch -= moveY;
        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    public void CameraMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        moveX = input.x * Sensitivity * Time.deltaTime * 100;
        moveY = input.y * Sensitivity * Time.deltaTime * 100;
    }
}
