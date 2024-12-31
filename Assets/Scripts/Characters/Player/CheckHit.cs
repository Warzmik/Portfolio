using UnityEngine;
using UnityEngine.Events;

namespace Characters.Player
{
    public class CheckHit : MonoBehaviour, IDamageable
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private Animator animator;

        [Space]

        public UnityEvent onDie;
        public float life { get; private set; }


        private void Awake()
        {
            life = playerData.life;

            animator.SetBool("Die", false);
        }


        public void Hit(float attackForce)
        {
            animator.SetTrigger("Hit");

            life -= attackForce;

            if (life <= 0)
            {
                onDie?.Invoke();
                animator.SetBool("Die", true);

                playerData.AllConstraints(false);
            }
        }
    }
}
