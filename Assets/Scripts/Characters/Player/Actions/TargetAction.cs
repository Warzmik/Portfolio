using System.Collections.Generic;
using Characters.Enemy;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace Characters.Player.Actions
{
    public class TargetAction : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;
        [SerializeField] private CinemachineCamera targetCamera;

        [Space]

        public UnityEvent onTargetCamera;
        public UnityEvent onNormalCamera;
        public UnityEvent onEnemyInRange;
        public UnityEvent onEnemyOutRange;

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
                onEnemyInRange?.Invoke();
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IEnemy enemy))
            {
                enemiesInRange.Remove(enemy);
                enemy.InRange(false);
                onEnemyOutRange?.Invoke();

                if (enemiesInRange.Count == 0)
                {
                    playerData.cameraMode = CameraModes.Normal;
                }
            }
        }


        private void Update()
        {
            if (aimAction.WasPressedThisFrame() && enemiesInRange.Count > 0) // Camera target mode
            {
                playerData.isTargeting = true;
                playerData.cameraMode = CameraModes.Target;
                onTargetCamera?.Invoke();
            }

            if (aimAction.WasReleasedThisFrame()) // Camera normal mode
            {
                playerData.isTargeting = false;
                playerData.cameraMode = CameraModes.Normal;
                onNormalCamera?.Invoke();

                playerData.targetPosition = Vector3.zero;
            }

            if (aimAction.IsPressed())
            {
                if (enemiesInRange.Count == 1 && targetCamera.LookAt != enemiesInRange[0].GetTransform())
                {
                    IEnemy enemySelected = enemiesInRange[0];
                    Transform enemyTransform = enemySelected.GetTransform();

                    targetCamera.LookAt = enemyTransform;
                    SetHowTarget(enemySelected);

                    targetIndex = 0;
                }

                if (enemiesInRange.Count > 1 && targetAction.WasPressedThisFrame())
                {
                    int inputScale = (int)targetAction.ReadValue<float>();

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
                    Transform enemyTransform = enemySelected.GetTransform();

                    targetCamera.LookAt = enemyTransform;
                    SetHowTarget(enemySelected);                  
                }
            }
        }


        private void FixedUpdate()
        {
            if (playerData.cameraMode == CameraModes.Target)
            {
                Vector3 enemyPosition = enemiesInRange[targetIndex].GetTransform().position;

                playerData.targetPosition = enemyPosition;
                enemyPosition.y = 0;

                transform.LookAt(enemyPosition);
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

