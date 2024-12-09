using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player 
{
    public class CheckGround : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onAir;
        public UnityEvent onTouchGround;

        private LayerMask masks;

        private float coyoteTimer;
        private bool isTouchGroundInvoked;


        private void Awake()
        {
            masks = LayerMask.GetMask("Ground", "Wall");
        }


        private void Update()
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.2f, masks))
            {
                playerData.inAir = false; // Is in ground
                playerData.canJump = true;

                if (!isTouchGroundInvoked) // Invoke touch ground event once
                {
                    isTouchGroundInvoked = true;
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
    }
}


