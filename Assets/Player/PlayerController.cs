using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Adjustable Stats
    public float airAcceleration;
    public float acceleration;
    public float deceleration;
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
    public bool DisableMovement;
    public void EnableMovement()
    {
        DisableMovement = false;
    }
    public float CurrentStamina { get; private set; }
    float LateralSpeed => Mathf.Sqrt(Mathf.Pow(controller.velocity.x, 2) + Mathf.Pow(controller.velocity.z, 2)); // the current speed combining the X and Z components
    public CharacterController controller;
    private void OnValidate()
    {
        if (controller == null)
        {
            Debug.LogWarning("Please Assign CharacterController to field");
        }

    }

    private void Start()
    {
        CurrentStamina = maxStamina;
        CurrentStamina = maxStamina;
    }
    private void Update()
    {
        HandleHorizontal();
        HandleVertical();
        HandleStamina();
        controller.Move(velocity * Time.deltaTime);
        HoverInteractableObject();
    }
    #endregion

    #region MovementHandling
    private Vector3 velocity;
    float AHI, AVI; //Adjusted Vertical/Horizontal Input
    private void HandleHorizontal()
    {
        float rotation = -Camera.main.gameObject.transform.eulerAngles.y;
        AHI = horizontalInput * Mathf.Cos(Mathf.Deg2Rad * rotation) - verticalInput * Mathf.Sin(Mathf.Deg2Rad * rotation);
        AVI = horizontalInput * Mathf.Sin(Mathf.Deg2Rad * rotation) + verticalInput * Mathf.Cos(Mathf.Deg2Rad * rotation);
        float x = 0, z = 0;
        float targetSpeed = CanSprint ? sprintSpeed : walkSpeed;
        if (!GroundCheck())
        {
            if (input.magnitude > .01)
            {
                x = Mathf.MoveTowards(velocity.x, targetSpeed * AHI, airAcceleration * Time.deltaTime);
                z = Mathf.MoveTowards(velocity.z, targetSpeed * AVI, airAcceleration * Time.deltaTime);
            }
            else
            {
                x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
                z = Mathf.MoveTowards(velocity.z, 0, deceleration * Time.deltaTime);
            }
        }
        else if (input.magnitude > .01)
        {
            Vector3 forward = transform.forward * Mathf.MoveTowards(LateralSpeed, targetSpeed * verticalInput, acceleration);
            Vector3 side = transform.right * Mathf.MoveTowards(LateralSpeed, targetSpeed * horizontalInput, acceleration);
            //Vector3 side = transform.right * (LateralSpeed, targetSpeed * horizontalInput, acceleration);
            x = forward.x + side.x;
            z = forward.z + side.z;
        }
        else
        {
            Vector3 forward = transform.forward * Mathf.MoveTowards(LateralSpeed, 0, deceleration);
            Vector3 side = transform.right * Mathf.MoveTowards(LateralSpeed, 0, deceleration);
            x = forward.x + side.x;
            z = forward.z + side.z;
        }
        velocity = new Vector3(x, velocity.y, z);
    }
    private void HandleVertical()
    {
        velocity.y -= gravity * Time.deltaTime;
        if (CanJump)
        {
            velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }
    }
    #endregion

    #region Collision
    [Min(0)] public float GroundDistance = .1f;
    private bool GroundCheck()
    {
        return Physics.SphereCast(transform.position, controller.radius, transform.up * -1, out RaycastHit hitInfo, GroundDistance);
    }
    #endregion

    #region Stamina
    private float TimeExhaustedStamina = float.MinValue;
    private void HandleStamina()
    {
        if (CanSprint && (new Vector2(horizontalInput, verticalInput).magnitude >= walkSpeed / sprintSpeed))
        {
            ReduceStamina(staminaLossRate * Time.deltaTime);
        }
        else
        {
            ReplenishStamina(staminaRecoveryRate * Time.deltaTime);
        }
    }
    public void ReplenishStamina(float amount)
    {
        CurrentStamina += amount;
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, maxStamina);

    }

    private bool CanSprint => sprinting && !Exhausted;
    public bool Exhausted => Time.time - TimeExhaustedStamina < RecoveryCooldown;

    public void ReduceStamina(float amount)
    {
        CurrentStamina -= amount;
        CurrentStamina = Mathf.Clamp(CurrentStamina, 0, maxStamina);
        if (CurrentStamina == 0 && !Exhausted)
        {
            TimeExhaustedStamina = Time.time;
        }
    }
    #endregion

    #region interact
    public float maxObjectDistance = 1;

    private bool HoveredIsInteractble(RaycastHit hovered, out InteractableObject obj)
    {
        obj = hovered.collider.gameObject.GetComponent<InteractableObject>();
        return obj != null;
    }
    private void HoverInteractableObject()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, maxObjectDistance) && HoveredIsInteractble(hitInfo, out InteractableObject obj))
        {
            if (obj != null)
            {
                StatTracker.hud.SetHoverText(obj.HoverText());
            }
        }
        else
        {
            StatTracker.hud.SetHoverText("");
        }
    }
    public void Interact(InputAction.CallbackContext context)
    {
        //Debug.Log("context given" + context.started);
        if (!context.started) return;
        string debugString = "Interact Pressed";
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, maxObjectDistance))
        {
            debugString += " Ray collided";
            InteractableObject obj = hitInfo.collider.gameObject.GetComponent<InteractableObject>();
            if (obj != null)
            {
                debugString += " found interactableObject";
                obj.Interact(GetComponent<PlayerController>());
            }
        }
        Debug.Log(debugString);
    }
    #endregion

    #region Unity InputSystem Events
    private float horizontalInput;
    private float verticalInput;
    private Vector2 input;
    public void LogMoveInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    bool jumpPressed = false;
    bool jumpHeld = false;
    private float timeJumpWasPressed = float.MinValue;
    public float JumpBuffer = .5f;
    private float TimeSinceJumpWasPressed => Time.time - timeJumpWasPressed;
    private bool CanJump => GroundCheck() && TimeSinceJumpWasPressed <= JumpBuffer;
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            timeJumpWasPressed = Time.time;
        }
        jumpPressed = context.started;
        jumpHeld = context.performed || jumpPressed;
    }

    public void TriggerSprint(InputAction.CallbackContext context)
    {
        sprinting = !context.canceled;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - (transform.up * GroundDistance), controller.radius);
    }
    #endregion
}
