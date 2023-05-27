using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    #region Adjustable Stats
    public float acceleration;
    public float airAcceleration;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpHeight;
     public float gravity;
    public float maxStamina;
    public float staminaRecoveryRate;
    public float staminaLossRate;
    [Tooltip("how long it takes after they stop sprinting to start recovering stamina")]
    public float RecoveryCooldown;
    #endregion

    #region Internal References
    private bool sprinting = false;
    private Vector3 targetVelocity; // the speed the player will be moving at the end of this frame;
    Vector3 Speed => controller.velocity;
    float LateralSpeed => Mathf.Sqrt(Mathf.Pow(controller.velocity.x, 2) + Mathf.Pow(controller.velocity.z, 2)); // the current speed combining the X and Z components
    CharacterController controller;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        HandleLateral();
        HandleVertical();
        Move();
        jumpPressed = false;
    }
    #endregion
    #region Movement
    private void HandleLateral()
    {
        if (controller.isGrounded)
        {
            HandleGroundLateral();
        }
        else
        {
            HandleAerialLateral();
        }
    }
    private void HandleGroundLateral()
    {
        
        float TopSpeed = sprinting ? sprintSpeed : walkSpeed;
        float accel = controller.isGrounded ? acceleration : airAcceleration;
        targetVelocity = (transform.right * horizontalInput + transform.forward * verticalInput) * Mathf.MoveTowards(LateralSpeed, TopSpeed, accel * Time.fixedDeltaTime);
    }
    private void HandleAerialLateral()
    {
        float TopSpeed = sprinting ? sprintSpeed : walkSpeed;
        targetVelocity = (transform.right * horizontalInput + transform.forward * verticalInput) * Mathf.MoveTowards(LateralSpeed, TopSpeed, airAcceleration * Time.fixedDeltaTime);
    }
    private void HandleVertical()
    {
        float y = Speed.y;
            y -= gravity * Time.fixedDeltaTime;
        if (jumpPressed && controller.isGrounded)
        {
            
            y = Mathf.Sqrt(2 * gravity * jumpHeight);
            Debug.Log("JumpSpeedApplied" + y);
        }
        else if (controller.isGrounded)
        {
            y = 0;
        }
        targetVelocity = new Vector3(targetVelocity.x, y, targetVelocity.z);
        //Debug.Log(targetVelocity);
    }
    #endregion
    //called at the end of update applies the speed calculated by HandleLateral and HandleVertical
    private void Move() {
        if(targetVelocity.y > 0)
        {
            Debug.Log(targetVelocity.y);
        }
        //controller.SimpleMove(targetVelocity);
        //controller.Move(new Vector3(0,targetVelocity.y * Time.fixedDeltaTime));
        controller.Move(targetVelocity * Time.fixedDeltaTime);
    }
    #region Unity InputSystem Events
    private float horizontalInput;
    private float verticalInput;
    public void LogMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }
    bool jumpPressed = false;
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpPressed = true;
        }
    }

    public void TriggerSprint(InputAction.CallbackContext context)
    {
        sprinting = !context.canceled;
    }
    #endregion
}
