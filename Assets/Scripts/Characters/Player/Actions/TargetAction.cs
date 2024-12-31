using System.Collections.Generic;
using Characters.Enemy;
using NUnit.Framework;
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
        private List<EnemyController> enemiesInRange = new List<EnemyController>();
        private int targetIndex;


        private void Awake()
        {
            aimAction = InputSystem.actions.FindAction("Aim");
            targetAction = InputSystem.actions.FindAction("Target"); 
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyController enemy))
            {
                enemiesInRange.Add(enemy);
                enemy.InRange(true);
                onEnemyInRange?.Invoke();
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyController enemy))
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
            if (enemiesInRange.Count > 0)
            {
                CheckForActiveTargets();
            }


            if (aimAction.WasPressedThisFrame() && enemiesInRange.Count > 0) // Camera target mode
            {
                playerData.isTargeting = true;
                playerData.cameraMode = CameraModes.Target;
                onTargetCamera?.Invoke();
            }

            if (aimAction.WasReleasedThisFrame() || enemiesInRange.Count == 0) // Camera normal mode
            {
                playerData.isTargeting = false;
                playerData.cameraMode = CameraModes.Normal;
                onNormalCamera?.Invoke();

                playerData.targetPosition = Vector3.zero;
            }

            if (aimAction.IsPressed())
            {
                if (enemiesInRange.Count == 1 && targetCamera.LookAt != enemiesInRange[0].transform)
                {
                    EnemyController enemySelected = enemiesInRange[0];
                    Transform enemyTransform = enemySelected.transform;

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

                    EnemyController enemySelected = enemiesInRange[targetIndex];
                    Transform enemyTransform = enemySelected.transform;

                    targetCamera.LookAt = enemyTransform;
                    SetHowTarget(enemySelected);                  
                }
            }
        }


        private void FixedUpdate()
        {
            if (playerData.cameraMode == CameraModes.Target && enemiesInRange.Count > 0)
            {
                Vector3 enemyPosition = enemiesInRange[targetIndex].transform.position; //BUGGGGGGGG

                playerData.targetPosition = enemyPosition;
                enemyPosition.y = 0;

                transform.LookAt(enemyPosition);
            }
        }


        private void SetHowTarget(EnemyController target)
        {
            foreach (EnemyController enemy in enemiesInRange)
            {
                enemy.SetTarget(false);
            }

            target.SetTarget(true);
        }


        private void CheckForActiveTargets()
        {
            for (int i = enemiesInRange.Count - 1; i >= 0; i--)
            {
                if (!enemiesInRange[i].gameObject.activeInHierarchy)
                {
                    enemiesInRange.RemoveAt(i);
                }
            }
        }
    }
}

