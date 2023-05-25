using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
// movement using Character Controller
    public static float moveSpeed = 10;
    public static bool isMoving;
    public float jumpHeight = 10;
    public float gravity = 9.81f;
    public float airControl = 5;
    private CharacterController controller;
    private Vector3 input, moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        //retrieving component
        controller = GetComponent<CharacterController>();

        
    }

    // Update is called once per frame
    void Update()
    {
        //getting inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // setting vector to combine inputs 
        //normalizing this vector makes sure that diagonal movements are not faster than horizontal or vertical movements 
        input = (transform.right * moveHorizontal + transform.forward  * moveVertical).normalized;
        input *= moveSpeed;
  
        isMoving = !(input == Vector3.zero);
        if(controller.isGrounded){ //isGrounded = is touching ground 
            //can jump
            moveDirection = input;
            if(Input.GetButton("Jump")){
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);


            }
            else{

                moveDirection.y = 0.0f;
            }
        
        }

        else{
            // in air 
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);

        }
            moveDirection.y -= gravity * Time.deltaTime;
            //move player using CharacterController.Move(...) function
            controller.Move(moveDirection * Time.deltaTime);


        }

}
