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


        private void Awake()
        {
            masks = LayerMask.GetMask("Floor");
        }


        private void OnCollisionEnter(Collision collision)
        {
            if ((masks.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                playerData.groundType = GroundTypes.Floor;
                onTouchGround?.Invoke();
            }
        }


        private void OnCollisionExit(Collision collision)
        {
            if ((masks.value & (1 << collision.transform.gameObject.layer)) > 0)
            {
                playerData.groundType = GroundTypes.Air;
                onAir?.Invoke();
            }
        }
    }
}


