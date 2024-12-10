using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    public class Aim : MonoBehaviour
    { 
        [SerializeField] private PlayerData playerData;
        [SerializeField] private GameObject aimCanvas;

        [Space]

        private InputAction aimAction;


        private void Awake()
        {
            aimAction = InputSystem.actions.FindAction("Aim");
            aimCanvas.SetActive(false);
        }


        private void Update()
        {
            if (aimAction.WasPressedThisFrame())
            {
                playerData.cameraMode = CameraModes.Aim;
                aimCanvas.SetActive(true);
            }

            if (aimAction.WasReleasedThisFrame())
            {
                playerData.cameraMode = CameraModes.Normal;
                aimCanvas.SetActive(false);
            }
        }


        private void FixedUpdate()
        {
            if (playerData.cameraMode != CameraModes.Aim) return;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Camera.main.transform.forward), 10f * Time.deltaTime);
        }
    }
}