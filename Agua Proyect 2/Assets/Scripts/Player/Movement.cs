using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private GameMaster gm;
    public Fludd fld;
    public Health health;
    public bool canDash = true;
    private bool haveFluud = false;

    public bool canRun = false;
    private float run = 0f;

    public GameObject script;

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
    public float fHealth = 200f;

    //jump variables
    bool isJumpPressed = false;
    float initialJumpVelocity;
    float maxJumpHeight = 5.0f;
    float maxJumpTime = 0.90f;
    bool isJumping = false;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        script = GameObject.Find("Player test 1");
        characterController.enabled = false;
        characterController.transform.position = gm.LastCheckPointPos;
        characterController.enabled = true;
    }

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

        health.setMaxHealth(fHealth);    
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

        

        if (canRun == true)
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x * 12.0f;
            currentMovement.z = currentMovementInput.y * 12.0f;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        }
        else
        {
            currentMovementInput = context.ReadValue<Vector2>();
            currentMovement.x = currentMovementInput.x * 6.0f;
            currentMovement.z = currentMovementInput.y * 6.0f;
            isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
        }
       
        
    }

    // Start is called before the first frame update
    

    void handleGravity()
    {

        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (fld.fHoverTime > 0 && (Input.GetKey(KeyCode.J)) && fld.fWater > 0 && haveFluud == true)
        {
            if (!isJumpPressed){
            
            velocity.y = 4f * Time.deltaTime;
            }
        }
        else 
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        if (fld.fHoverTime > 0 && (Input.GetKey(KeyCode.J)) && fld.fWater > 0 && isJumpPressed && haveFluud == true)
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

        if(fHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }

        if (Input.GetKey(KeyCode.K) && fld.bInWater == true)
        {
            fld.fWater += fld.fChangePerSecond * Time.deltaTime;

            fld.WI.SetWater(fld.fWater);
        }

        

        if (run > 3.5f)
        {
            canRun = true;
        }
        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            canRun = false;

            run = 0f;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            run += Time.deltaTime;
        }

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            fHealth -= 20f;
            health.SetHealth(fHealth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            fHealth -= 20f;
            health.SetHealth(fHealth);
        }
       
        if (other.gameObject.tag == "Out")
        {
            fHealth -= 20f;
            health.SetHealth(fHealth);
            characterController.enabled = false;
            characterController.transform.position = gm.LastCheckPointPos;
            characterController.enabled = true;

        }
        if (other.gameObject.tag == "Dash")
        {
            Debug.Log("Dashin");
            currentMovement.z = currentMovementInput.y * 30.0f;
            currentMovement.x = currentMovementInput.x * 30.0f;
            
        }
        if (other.gameObject.tag == "Fludish")
        {
            script.GetComponent<Fludd>().enabled = true;
            haveFluud = true;
            Destroy(other.gameObject);

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);

        
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
