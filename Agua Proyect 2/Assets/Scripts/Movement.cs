using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public Fludd fld;

    //REFERENCE VARIABLES
    PlayerInput playerInput;
    CharacterController characterController;

    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // VARIABLES PARA ALMACENAR LOS VALORES DEL PLAYER INPUT
    Vector2 currentMovementInput;
    Vector3 currentMovement;
    public Vector3 velocity;
    bool isMovementPressed;
    public bool isGrounded;

    public float gravity = -9.81f;

    //jump variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 5.0f;
    float maxJumpTime = 0.90f;
    bool isJumping = false;

    //Awake se llama antes de Start en Unity
    void Awake()
    {
        //Pone referencias para las variables
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Jump.started += onJump;
        playerInput.CharacterControls.Jump.canceled += onJump;

        setupJumpVariables();
    }

    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    void handleJump()
    {
        if (isGrounded && isJumpPressed && !isJumping) {
            isJumping = true;
            if (!(Input.GetKey(KeyCode.J))){
            velocity.y = initialJumpVelocity * 0.9f;
            }
        } else if (!isJumpPressed && isGrounded && isJumping)
        {
            isJumping = false;
        }
    }

    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();

    }


    void handleRotation()
    {

        if (isMovementPressed)
        {
            float targetAngle = Mathf.Atan2(currentMovement.x, currentMovement.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        }


    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x * 4.0f;
        currentMovement.z = currentMovementInput.y * 6.0f;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void handleGravity()
    {

        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (fld.fHoverTime > 0 && (Input.GetKey(KeyCode.J)) && fld.fWater > 0)
        {
            if (!isJumpPressed){
            
            velocity.y = 4f * Time.deltaTime;
            }
        }
        else 
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        if (fld.fHoverTime > 0 && (Input.GetKey(KeyCode.J)) && fld.fWater > 0 && isJumpPressed)
        {
            velocity.y = 4f * Time.deltaTime;
        }
        
        characterController.Move (velocity * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        handleRotation();
        characterController.Move(currentMovement * Time.deltaTime);

        handleJump();

        handleGravity();


        if (isGrounded == true)
        {
            fld.fHoverTime = 3f;
        }

    }

    void OnEnable()
    {
        // habilita el mapa de controles del character
        playerInput.CharacterControls.Enable();
    }

    void OnDisable()
    { 
        //deshabilita el mapa de controles del character
        playerInput.CharacterControls.Disable();
    }
}
