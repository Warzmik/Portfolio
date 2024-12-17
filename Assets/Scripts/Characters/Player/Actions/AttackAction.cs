using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    public class AttackAction : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onAttack;

        private InputAction attackAction;


        private void Awake()
        {
            attackAction = InputSystem.actions.FindAction("Attack");
            playerData.makingDamage = false;
        }


        private void Update()
        {
            if (!playerData.canAttack) return;

            if (attackAction.WasReleasedThisFrame())
            {
                playerData.isAttacking = false;
                StartCoroutine(AttackTime());
            }

            if (attackAction.WasPressedThisFrame() && playerData.groundType == GroundTypes.Floor && playerData.cameraMode == CameraModes.Target)
            {
                playerData.isAttacking = true;
                playerData.makingDamage = true;

                StopAllCoroutines();
            }
        }


        private IEnumerator AttackTime()
        {
            yield return new WaitForSeconds(0.25f);

            playerData.makingDamage = false;
        }
    }
}
