using UnityEngine;
using Characters.Enemy;

namespace Characters.Player
{
    public class SwordHit : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IEnemy enemy) && playerData.isAttacking)
            {
                enemy.Hit(playerData.attackForce);
            }
        }
    }
}