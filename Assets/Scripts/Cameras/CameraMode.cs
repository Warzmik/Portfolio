using Unity.Cinemachine;
using UnityEngine;

namespace Cameras
{
    public class CameraMode : MonoBehaviour
    {
        [SerializeField] private CameraModes cameraMode;
        [SerializeField] private PlayerData playerData;

        private CinemachineCamera cinemachineCamera;


        private void Awake()
        {
            cinemachineCamera = GetComponent<CinemachineCamera>();
        }


        private void Update()
        {
            if (playerData.cameraMode != cameraMode || cinemachineCamera.IsLive) return;

            cinemachineCamera.Prioritize();
        }
    }
}
