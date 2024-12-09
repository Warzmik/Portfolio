using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Characters.Player 
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onMovementStart;
        public UnityEvent onMovementStop;

        private new Rigidbody rigidbody;
        private Camera mainCamera;
        private InputAction moveAction;
          private bool isMovingInvoked;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            mainCamera = Camera.main;
            moveAction = InputSystem.actions.FindAction("Move");
        }


        private void Update()
        {
            if (!playerData.canBeControlled || !playerData.canMove) return;

            // Check if start
            if (moveAction.WasPressedThisFrame())
            {
                onMovementStart?.Invoke();
            }

            // Check if stop
            if (moveAction.WasReleasedThisFrame())
            {
                isMovingInvoked = false;
                onMovementStop?.Invoke();
            }

            // Check if is pressed
            if (moveAction.IsPressed())
            {
                isMovingInvoked = true;
            }
        }


        private void FixedUpdate()
        {
            if (!playerData.canBeControlled || !playerData.canMove || !isMovingInvoked) return;

            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            Vector3 direction = new Vector3(moveValue.x, 0, moveValue.y);
            Vector3 moveDirection = mainCamera.transform.forward * direction.z + mainCamera.transform.right * direction.x;

            rigidbody.linearVelocity = new Vector3(
                moveDirection.x * playerData.movementSpeed, 
                rigidbody.linearVelocity.y, 
                moveDirection.z * playerData.movementSpeed
            );

            moveDirection.y = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), 10f * Time.deltaTime);

            playerData.currentPosition = transform.position;
        }
    }
}
