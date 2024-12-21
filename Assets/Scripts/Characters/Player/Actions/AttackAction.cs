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
            playerData.attackCount = 0;
        }


        private void Update()
        {
            if (!playerData.canAttack || playerData.groundType != GroundTypes.Floor || playerData.cameraMode != CameraModes.Target) return;

            if (attackAction.WasReleasedThisFrame())
            {
                playerData.isAttacking = false;

                StopCoroutine("ResetAttackCount");
                StartCoroutine("ResetAttackCount");
            }

            if (attackAction.WasPressedThisFrame())
            {
                playerData.isAttacking = true;
                playerData.attackCount++;

                if (playerData.attackCount > 4)
                {
                    playerData.attackCount = 1;
                }

                StopCoroutine("ResetAttackCount");
            }
        }


        private IEnumerator ResetAttackCount()
        {
            yield return new WaitForSeconds(0.5f);
            playerData.attackCount = 0;
        }
    }
}
