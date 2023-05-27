using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Stats
    // movement using Character Controller
    [Header("MovementStats")]
    public float moveSpeed = 10;
    public float sprintSpeed;
    public float jumpHeight = 10;
    public float gravity = 9.81f;
    public float airControl = 5;
    [Header("Stamina")]
    public float MaxStamina = 100;
    [Min(0)] public float StaminaLossRate = 10;
    [Min(0)] public float StaminaRecoveryRate = 10;
    #region Private References
    public float currentStamina { get; private set; }
    private CharacterController controller;
    private Vector3 input, moveDirection;
    #endregion



    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //retrieving component
        currentStamina = MaxStamina;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ToggleSprinting();
        HandleMovement();
        HandleStamina();
    }
    private bool sprinting;
    private void ToggleSprinting()
    {
        sprinting = Input.GetKey(KeyCode.LeftShift);
    }
    public void HandleMovement()
    {
        //getting inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // setting vector to combine inputs 
        //normalizing this vector makes sure that diagonal movements are not faster than horizontal or vertical movements 
        input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
        input *= (sprinting & currentStamina > 0) ? sprintSpeed : moveSpeed;

        if (controller.isGrounded)
        { //isGrounded = is touching ground 
            //can jump
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0.0f;
            }

        }

        else
        {
            // in air 
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);

        }
        moveDirection.y -= gravity * Time.deltaTime;
        //move player using CharacterController.Move(...) function
        controller.Move(moveDirection * Time.deltaTime);
    }
    private void HandleStamina()
    {
        if (sprinting && (Mathf.Abs(controller.velocity.x) > .1 || Mathf.Abs(controller.velocity.z) > .1))
        {
            ReduceStamina(StaminaLossRate * Time.deltaTime);
        }
        if(!sprinting)
        {
            ReplenishStamina(StaminaRecoveryRate* Time.deltaTime);
        }
    }
    public void ReplenishStamina(float amount)
    {
        currentStamina += amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, MaxStamina);
    }

    public void ReduceStamina (float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Clamp(currentStamina, 0, MaxStamina);
    }
}
