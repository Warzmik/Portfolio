using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace Characters.Player.Actions
{
    public class JumpAction : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onJumpStart;

        private new Rigidbody rigidbody;
        private InputAction jumpAction;
        private bool canAction = true;


        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            jumpAction = InputSystem.actions.FindAction("Jump");
        }


        private void Update()
        {
            if (!playerData.canJump) return;

            if (jumpAction.WasReleasedThisFrame())
            {
                playerData.isJumping = false;  
            }

            if (jumpAction.WasPressedThisFrame() && playerData.groundType == GroundTypes.Floor && playerData.cameraMode == CameraModes.Normal && canAction)
            {
                playerData.isJumping = true;
                canAction = false;

                onJumpStart?.Invoke();
                rigidbody.AddForce(Vector3.up * playerData.jumpForce, ForceMode.Impulse);

                StartCoroutine(CanAction());
            }
        }


        private IEnumerator CanAction()
        {
            yield return new WaitForSeconds(1f);

            canAction = true;
        }

    }
}
