using UnityEngine;


namespace Characters.Player
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Animator animator;


        private void Update()
        {
            animator.SetBool("air", playerData.inAir);
            animator.SetBool("jump", playerData.isJumping);
            animator.SetBool("move", playerData.isMoving);

            switch (playerData.cameraMode)
            {
                case CameraModes.Normal:
                    animator.SetInteger("camera", 0);
                    break;

                case CameraModes.Target:
                case CameraModes.Aim:
                    animator.SetInteger("camera", 1);
                    break;
            }
        }
    }
}

