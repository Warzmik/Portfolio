using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [Space]
    public UnityEvent onMovementStart;
    public UnityEvent onMovementStop;
    public UnityEvent onJumpStart;
    public UnityEvent onAir;
    public UnityEvent onTouchGround;
    

    private new Rigidbody rigidbody;
    private LayerMask groundMask;
    private InputAction moveAction;
    private InputAction jumpAction;

    private float coyoteTimer;
    private bool isTouchGroundInvoked;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        groundMask = LayerMask.GetMask("Ground");

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    
    private void Update()
    {
        if (!playerData.canBeControlled) return;

        CheckGround();
        Movement();
        Jump();
    }


    private void CheckGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, 1.2f, groundMask))
        {
            playerData.inAir = false; // Is in ground

            if (!isTouchGroundInvoked) // Invoke touch ground event once
            {
                isTouchGroundInvoked = true;
                playerData.canJump = true;
                onTouchGround?.Invoke();
            }
        }
        else
        {
            coyoteTimer += Time.deltaTime;

            if (coyoteTimer < playerData.coyoteTime) return;

            playerData.inAir = true; // Is in air
            coyoteTimer = 0f;

            if (isTouchGroundInvoked)
            {
                isTouchGroundInvoked = false;
                onAir?.Invoke();
            }
        }
    }


    private void Movement()
    {
        if (!playerData.canMove) return;

        // Check if start
        if (moveAction.WasPressedThisFrame())
        {
            onMovementStart?.Invoke();
        }

        // Check if stop
        if (moveAction.WasReleasedThisFrame())
        {
            onMovementStop?.Invoke();
        }
        
        // Check if is moving
        if (moveAction.IsPressed())
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            rigidbody.linearVelocity = new Vector3(moveValue.x * playerData.movementSpeed, rigidbody.linearVelocity.y, moveValue.y * playerData.movementSpeed);
        } 
    }


    private void Jump()
    {
        if (!playerData.canJump || playerData.inAir) return;

         // Check if jump
        if (jumpAction.WasPressedThisFrame())
        {
            playerData.canJump = false;
            onJumpStart?.Invoke();

            rigidbody.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);
        }
    }
}
