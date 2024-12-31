using UnityEngine;

namespace Characters.Player
{
    public class SwordHit : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable) && playerData.isAttacking)
            {
                damageable.Hit(playerData.attackForce);
                playerData.isAttacking = false;
            }
        }
    }
}