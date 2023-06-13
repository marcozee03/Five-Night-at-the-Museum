using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    #region Adjustable Stats
    [Header("Speed")]
    [Tooltip("Players max walk speed")]
    public float walkSpeed;
    [Tooltip("Players max sprinting speed")]
    public float sprintSpeed;

    [Header("Acceleration")]
    [Tooltip("how fast player reaches top speed while grounded")]
    public float acceleration;
    [Tooltip("how fast the character slows down while grounded")]
    public float deceleration;
    [Tooltip("Acceleration while player is in the air")]
    public float airAcceleration;
    [Tooltip("how fast character slows down in the air")]
    public float airDeceleration;

    [Header("Jump")]
    [Tooltip("How high the player can Jump")]
    public float jumpHeight;
    [Tooltip("How long a Jump input is held for so that an input isnt \"eaten\" ")]
    public float JumpBuffer = .5f;
    [Tooltip("Acceleration due to gravity")]
    public float gravity;




    [Tooltip("how long it takes after they stop sprinting to start recovering stamina")]
    public float RecoveryCooldown;
    #endregion

    #region Internal
    private bool sprinting = false;
    private bool LockPlayer;
    public CapsuleCollider col;
    private void OnValidate()
    {
        if (controller == null)
        {
            Debug.LogWarning("Please attach a Character Controller component to object called \"" + gameObject.name + "\"");
        }
        if (col == null)
        {
            Debug.LogWarning("Please attach a CapsuleCollider component to object called \"" + gameObject.name + "\"");
        }
    }
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        col = GetComponent<CapsuleCollider>();
    }
    float LateralSpeed => Mathf.Sqrt(Mathf.Pow(controller.velocity.x, 2) + Mathf.Pow(controller.velocity.z, 2)); // the current speed combining the X and Z components
    public CharacterController controller;
    private void Start()
    {
        CurrentStamina = maxStamina;
    }
    private void Update()
    {
        if (!LockPlayer)
        {
            HandleHorizontal();
            HandleVertical();
            HandleSnappingToGround();
            HandleStamina();
            controller.Move(velocity * Time.deltaTime);
        }
        HoverInteractableObject();
    }
    #endregion

    #region External
    public float CurrentStamina { get; private set; }
    public void EnableMovement()
    {
        LockPlayer = false;
    }
    public void DisableMovement()
    {
        LockPlayer = true;
    }

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

    #endregion

    #region MovementHandling

    private Vector3 velocity;

    private void HandleHorizontal()
    {
        float AHI, AVI; //Adjusted Vertical/Horizontal Input
        float rotation = -transform.eulerAngles.y;
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
        if (GroundCheck())
        {
            velocity.y = 0;
        }
        velocity.y -= gravity * Time.deltaTime;
        Debug.Log(velocity.y);

        if (CanJump)
        {
            timeJumpWasPressed = Mathf.NegativeInfinity; //prevents a double jump input if you manage to land on
                                                         //the ground again within the jump buffer window
            velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            tempDisableSnapping = true;
        }

    }
    #endregion

    #region Collision
    [Header("Collision")]
    [Min(0)] public float GroundDistance = .1f;
    [Tooltip("If the player should snap to the ground ")]
    public bool EnableSnapping = true;
    [Min(0)] public float SnapDistance;
    private bool tempDisableSnapping;

    private void HandleSnappingToGround()
    {
        if (!tempDisableSnapping && EnableSnapping)
        {
            SnapToGround();
        }
    }
    private void SnapToGround()
    {
        if (Physics.Raycast(new Ray(transform.position - new Vector3(0, col.height / 2), transform.up * -1), out RaycastHit hit, SnapDistance))
        {
            controller.Move(new Vector3(0, -hit.distance, 0));
        }
        else
        {
            tempDisableSnapping = true;
        }

    }
    private bool GroundCheck()
    {
        bool val = Physics.SphereCast(transform.position, controller.radius, transform.up * -1, out RaycastHit hitInfo, GroundDistance);
        if (val) tempDisableSnapping = false;
        return val;
    }
    #endregion

    #region Stamina
    #region stats
    [Header("Stamina")]
    public float maxStamina;
    public float staminaRecoveryRate;
    public float staminaLossRate;
    #endregion
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
    [Header("Interaction")]
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



    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position - (transform.up * GroundDistance), controller.radius);
        Gizmos.DrawLine(transform.position - new Vector3(0, col.height / 2), transform.position - new Vector3(0, col.height + SnapDistance));
    }
    #endregion
}
