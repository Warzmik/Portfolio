using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Characters.Player.Actions
{
    public class MoveAction : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onMoveStart;
        public UnityEvent onMoveStop;

        private new Rigidbody rigidbody;
        private Camera mainCamera;
        private InputAction moveAction;
        

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();

            moveAction = InputSystem.actions.FindAction("Move");
            mainCamera = Camera.main;

            playerData.isMoving = false;
        }


        private void Update()
        {
            if (!playerData.canMove) return;

            if (moveAction.WasPressedThisFrame())
            {
                onMoveStart?.Invoke();

                playerData.isMoving = true;
            }

            if (moveAction.WasReleasedThisFrame())
            {
                onMoveStop?.Invoke();

                playerData.isMoving = false;
                playerData.moveDirection = Vector3.zero;
            }
        }


        private void FixedUpdate()
        {
            if (!playerData.canMove && playerData.isMoving) return;

            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            Vector3 direction = new Vector3(moveValue.x, 0, moveValue.y);
            Vector3 cameraForward = mainCamera.transform.forward;

            playerData.moveDirection = cameraForward * direction.z + mainCamera.transform.right * direction.x;
            playerData.moveDirection.y = 0f;

            playerData.moveValue = moveValue.magnitude;

            rigidbody.linearVelocity = new Vector3(
                playerData.moveDirection.x * playerData.moveForce,
                rigidbody.linearVelocity.y,
                playerData.moveDirection.z * playerData.moveForce
            );

            if (playerData.moveDirection != Vector3.zero && playerData.cameraMode == CameraModes.Normal)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerData.moveDirection), 10f * Time.deltaTime);
            }

            playerData.currentPosition = transform.position;
        }
    }
}