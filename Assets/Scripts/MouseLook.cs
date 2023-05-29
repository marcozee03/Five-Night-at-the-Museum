using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerBody;
    public float mouseSensitivity = 10;

    //starting look "height"
    private float pitch = 0;
    // Start is called before the first frame update
    void Start()
    {
        //getting reference to parent object
        playerBody = transform.parent.parent.transform;

        //hiding and locking cursor 
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //retrieving mouse inputs 
        float moveX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //applying rotations, yaw (looking left right)
        playerBody.Rotate(Vector3.up * moveX); 

        //applying rotations, pitch (looking up down)
        pitch -= moveY;

        //clamping pitch value so you can't "breka your neck" (in game)
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        //local rotation to make it correlate with parent's rotation
        // "rotates object in relation to its parent's transform rotation."
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);

    }
}
