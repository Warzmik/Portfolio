using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Characters.Player.Actions
{
    public class PowerAction : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        [Space]

        public UnityEvent onPowerStart;
        public UnityEvent onPowerEnd;

        private InputAction powerAction;
        private float initialAttackForce;
        private bool isActive;


        private void Awake()
        {
            powerAction = InputSystem.actions.FindAction("Power");
            playerData.isUsingPower = false;
            isActive = false;
            initialAttackForce = playerData.attackForce;
        }


        private void Update()
        {
            if (!playerData.canUsePower) return;

            if (powerAction.WasReleasedThisFrame())
            {
                playerData.isUsingPower = false;
            }

            if (powerAction.WasPressedThisFrame() && !isActive)
            {
                playerData.isUsingPower = true;
                StartCoroutine("PowerEffect");
            }
        }


        private IEnumerator PowerEffect()
        {
            isActive = true;
            playerData.attackForce *= playerData.powerForce;
            onPowerStart?.Invoke();

            yield return new WaitForSeconds(playerData.powerDuration);

            onPowerEnd?.Invoke();
            playerData.attackForce = initialAttackForce;
            isActive = false;
        }
    }
}
