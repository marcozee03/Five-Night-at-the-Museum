using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    #region Adjustable Stats
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
    public float CurrentStamina { get; private set; }
    private Vector3 targetVelocity; // the speed the player will be moving at the end of this frame;
    Vector3 Speed => controller.velocity;
    float LateralSpeed => Mathf.Sqrt(Mathf.Pow(controller.velocity.x, 2) + Mathf.Pow(controller.velocity.z, 2)); // the current speed combining the X and Z components
    CharacterController controller;
    private void OnValidate()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        CurrentStamina = maxStamina;
    }

    private void Update()
    {
        HandleMovement();
        HandleStamina();
        HoverInteractableObject();
    }
    #endregion

    #region Movement
    Vector3 input, moveDirection;
    AudioSource [] movementSFXs;
    float walkSoundBuffer;
    float sprintSoundBuffer;

    private void HandleMovement()
    {
        //getting inputs
        // setting vector to combine inputs 
        //normalizing this vector makes sure that diagonal movements are not faster than horizontal or vertical movements 
        input = (transform.right * horizontalInput + transform.forward * verticalInput);
        input *= (CanSprint & CurrentStamina > 0) ? sprintSpeed : walkSpeed;

        movementSFXs = GetComponents<AudioSource>();

        if (GroundCheck())
        { //isGrounded = is touching ground 
          //can jump
            HandleGrounded();
        }

        else
        {
            HandleAerial();

        }
        moveDirection.y -= gravity * Time.deltaTime;
        //move player using CharacterController.Move(...) function
        controller.Move(moveDirection * Time.deltaTime);
        if(walkSoundBuffer > 0){
            walkSoundBuffer -= Time.deltaTime;
        }
       
        if(sprintSoundBuffer > 0){
            sprintSoundBuffer -= Time.deltaTime;
        }
        

        if(!sprinting && input != Vector3.zero && !movementSFXs[0].isPlaying && walkSoundBuffer <= 0 && GroundCheck()){
            if(movementSFXs[1].isPlaying){
                movementSFXs[1].Stop();
            }
            movementSFXs[0].Play();
            walkSoundBuffer = 0.75f;


           
        }
        else if(sprinting && input != Vector3.zero && !movementSFXs[1].isPlaying && sprintSoundBuffer <= 0 && GroundCheck()){
            //first movementSFXs element is walking sfx, second is sprinting
            if(movementSFXs[0].isPlaying){
                movementSFXs[0].Stop();
            }
          
            movementSFXs[1].Play();
            sprintSoundBuffer = 0.5f;
            
        }
     
        

    }
    
    private void HandleGrounded()
    {
        moveDirection = input;
        if (CanJump)
        {
            
            moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }
    }

    private void HandleAerial()
    {
        // in air 
        walkSoundBuffer = 0.2f;
        sprintSoundBuffer = 0.3f;
        input.y = moveDirection.y;
        moveDirection = Vector3.Lerp(moveDirection, input, airAcceleration * Time.deltaTime);
    }
    #endregion

    #region Collision
    [Min(0)]public float GroundDistance = .1f;
    private bool GroundCheck()
    {
        return Physics.SphereCast(transform.position, controller.radius,transform.up * -1, out RaycastHit hitInfo ,GroundDistance);
    }
    #endregion

    #region Stamina
    private float TimeExhaustedStamina = float.MinValue;
    private void HandleStamina()
    {
        if (CanSprint && (Mathf.Abs(controller.velocity.x) > .1 || Mathf.Abs(controller.velocity.z) > .1))
        {
            ReduceStamina(staminaLossRate * Time.deltaTime);
        }
        if (LateralSpeed <= walkSpeed || !sprinting)
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

    private bool HoveredIsInteractble(RaycastHit hovered, out InteractableObject obj){
        obj= hovered.collider.gameObject.GetComponent<InteractableObject>();
        return obj != null;
    }
    private void HoverInteractableObject()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, maxObjectDistance) && HoveredIsInteractble(hitInfo, out InteractableObject obj))
        {
            if (obj != null)
            {
                GameObject.FindFirstObjectByType<HUDManager>().SetHoverText(obj.HoverTextMnK());
            }
        }        
        else
        {
            GameObject.FindFirstObjectByType<HUDManager>().SetHoverText("");
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
    public void LogMoveInput(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        horizontalInput = input.x;
        verticalInput = input.y;
    }

    bool jumpPressed = false;
    bool jumpHeld = false;
    private float timeJumpWasPressed = 0;
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
