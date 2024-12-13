using System.Collections.Generic;
using Characters.Enemy;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    public class Aim : MonoBehaviour
    { 
        [SerializeField] private PlayerData playerData;
        [SerializeField] private CinemachineCamera targetCamera;

        public UnityEvent onTargetCamera;
        public UnityEvent onAimCamera;
        public UnityEvent onNormalCamera;

        private InputAction aimAction;
        private InputAction targetAction;
        private List<IEnemy> enemiesInRange = new List<IEnemy>();
        private int targetIndex;


        private void Awake()
        {
            aimAction = InputSystem.actions.FindAction("Aim");
            targetAction = InputSystem.actions.FindAction("Target");
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEnemy enemy))
            {
                enemiesInRange.Add(enemy);
                enemy.InRange(true);
            }
        }


        private void OnTriggerExit(Collider other)
        {
             if (other.TryGetComponent(out IEnemy enemy))
            {
                enemiesInRange.Remove(enemy);
                enemy.InRange(false);
            }
        }


        private void Update()
        {
            if (aimAction.WasPressedThisFrame() && enemiesInRange.Count > 0) // Camera target mode
            {
                playerData.cameraMode = CameraModes.Target;
                onTargetCamera?.Invoke();
            }

            if (aimAction.WasPressedThisFrame() && enemiesInRange.Count == 0) // Camera aim mode
            {
                playerData.cameraMode = CameraModes.Aim;
                onAimCamera?.Invoke();
            }

            if (aimAction.WasReleasedThisFrame()) // Camera normal mode
            {
                playerData.cameraMode = CameraModes.Normal;
                onNormalCamera?.Invoke();
            }

            if (aimAction.IsPressed())
            {
                if (enemiesInRange.Count == 1 && targetCamera.LookAt != enemiesInRange[0].GetTransform())
                {
                    IEnemy enemySelected = enemiesInRange[0];

                    targetCamera.LookAt = enemySelected.GetTransform();
                    SetHowTarget(enemySelected);

                    targetIndex = 0;
                }

                if (enemiesInRange.Count > 1 && targetAction.WasPressedThisFrame())
                {
                    int inputScale = (int) targetAction.ReadValue<float>();

                    targetIndex += inputScale;

                    if (targetIndex < 0)
                    {
                        targetIndex = enemiesInRange.Count - 1;
                    }

                    if (targetIndex > enemiesInRange.Count - 1)
                    {
                        targetIndex = 0;
                    }

                    IEnemy enemySelected = enemiesInRange[targetIndex];
                    
                    targetCamera.LookAt = enemySelected.GetTransform();
                    SetHowTarget(enemySelected);
                }
            } 
        }


        private void FixedUpdate()
        {
            if (playerData.cameraMode == CameraModes.Target || playerData.cameraMode == CameraModes.Aim)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.forward), 10f * Time.deltaTime);
            }
        }


        private void SetHowTarget(IEnemy target)
        {
            foreach (IEnemy enemy in enemiesInRange)
            {
                enemy.SetTarget(false);
            }

            target.SetTarget(true);
        }
    }
}