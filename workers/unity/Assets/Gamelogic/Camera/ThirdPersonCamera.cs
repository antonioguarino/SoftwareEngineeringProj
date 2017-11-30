﻿using Assets.Gamelogic.Core;
using Improbable.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Player
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [Require]
        private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;
        private Transform camera;
        private UnityEngine.Quaternion cameraRotation;
        private float cameraDistance;
        private float smoothing = 5f;
		private float[] camera_position;
		Vector3 offset;

        private void OnEnable()
        {
            // Grab the camera from the Unity scene
            camera = Camera.main.transform;
            // Set the camera as a child of the Player to easily ensure the camera follows the Player
            //camera.parent = transform;

			offset = new Vector3(0,11,-8);
			camera.position = transform.position + offset;
            // Set the camera rotation and zoom distance to some initial values
            cameraRotation = SimulationSettings.InitialThirdPersonCameraRotation;
            cameraDistance = SimulationSettings.InitialThirdPersonCameraDistance;

        }

        private void LateUpdate()
        {
            //SetCameraTransform();
            CameraFollow();
        }

        private void CameraFollow()
        {
            Vector3 targetCamPos = transform.position + offset;
            camera.position = Vector3.Lerp(camera.position, targetCamPos, smoothing * Time.deltaTime);
        }

        // Update the position and orientation of the camera to match the cameraRotation and cameraDistance
        private void SetCameraTransform()
        {
            // Set the position of the camera based on the desired rotation towards and distance from the Player model
            camera.localPosition = cameraRotation * Vector3.back * cameraDistance;
            // Set the camera to look towards the Player model
            camera.LookAt(transform.position);
        }

        private void Update()
        {
            SelectNextCameraDistance();
            //SelectNextCameraRotation();
        }

        // If the user scrolls up on their mousewheel then zoom in, if they scroll down then zoom out
        private void SelectNextCameraDistance()
        {
            var mouseScroll = Input.GetAxis(SimulationSettings.MouseScrollWheel);
            if (!mouseScroll.Equals(0f))
            {
                var distanceChange = cameraDistance - mouseScroll * SimulationSettings.ThirdPersonZoomSensitivity;
                cameraDistance = Mathf.Clamp(distanceChange, SimulationSettings.ThirdPersonCameraMinDistance,
                    SimulationSettings.ThirdPersonCameraMaxDistance);
            }
        }

        // If the user holds right mouse button and moves their mouse about, the camera rotates around the player
        private void SelectNextCameraRotation()
        {
            if (Input.GetMouseButton(SimulationSettings.RotateCameraMouseButton))
            {
                var yaw = (cameraRotation.eulerAngles.y + Input.GetAxis("Mouse X") * SimulationSettings.ThirdPersonCameraSensitivity) % 360f;
                var pitch = Mathf.Clamp(cameraRotation.eulerAngles.x - Input.GetAxis("Mouse Y") * SimulationSettings.ThirdPersonCameraSensitivity,
                        SimulationSettings.ThirdPersonCameraMinPitch,
                        SimulationSettings.ThirdPersonCameraMaxPitch);
                cameraRotation = UnityEngine.Quaternion.Euler(new Vector3(pitch, yaw, 0));
            }
        }
    }
}