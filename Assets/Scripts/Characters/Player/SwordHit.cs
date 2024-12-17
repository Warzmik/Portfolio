using UnityEngine;
using UnityEngine.Events;
using Characters.Enemy;

namespace Characters.Player
{
    public class SwordHit : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onSwordHit;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEnemy enemy) && playerData.makingDamage)
            {
                enemy.Hit(playerData.attackForce);
                onSwordHit?.Invoke();
            }
        }
    }
}