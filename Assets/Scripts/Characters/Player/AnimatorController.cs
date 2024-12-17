using UnityEngine;


namespace Characters.Player
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Animator animator;


        private void LateUpdate()
        {
            animator.SetInteger("groundType", (int) playerData.groundType);
            animator.SetInteger("cameraType", (int) playerData.cameraMode);
            
            animator.SetFloat("moveValue", playerData.moveValue);

            animator.SetBool("isMoving", playerData.isMoving);
            animator.SetBool("isJumping", playerData.isJumping);
            animator.SetBool("isAttacking", playerData.isAttacking);
        }
    }
}

