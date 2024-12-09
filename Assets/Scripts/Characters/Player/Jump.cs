using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Characters.Player 
{
    public class Jump : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onJumpStart;

        private new Rigidbody rigidbody;
        private InputAction jumpAction;

        private bool isJumpingInvoked;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            jumpAction = InputSystem.actions.FindAction("Jump");
        }


        private void Update()
        {
            if (!playerData.canBeControlled) return;

            if (jumpAction.WasReleasedThisFrame())
            {
                isJumpingInvoked = false;
            }

            if (jumpAction.WasPressedThisFrame())
            {
                isJumpingInvoked = true;
            }
        }


        private void FixedUpdate()
        {
            if (!playerData.canBeControlled || !playerData.canJump || playerData.inAir || !isJumpingInvoked) return;

            playerData.canJump = false;
            onJumpStart?.Invoke();

            rigidbody.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);
        }
    }
}
