using Assets.Gamelogic.Core;
using Improbable.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Improbable.Player;

namespace Assets.Gamelogic.Player
{
    public class PlayerOrientation : MonoBehaviour
    {
        [Require]
        private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;
        [Require] private PlayerRotation.Writer PlayerRotationWriter;

        int floorMask;
        float camRayLength = 100f;
        private Transform camera;
        private UnityEngine.Quaternion cameraRotation;
        private float cameraDistance;
        private Rigidbody playerRigidBody;
        private void OnEnable()
        {
            floorMask = LayerMask.GetMask("Floor");
            playerRigidBody = GetComponent<Rigidbody>();
            // Grab the camera from the Unity scene
            camera = Camera.main.transform;
			camera.transform.position = playerRigidBody.transform.position - new Vector3 (0,11,-8);
        }
        private void Update()
        {

            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f;

                UnityEngine.Quaternion newRotation = UnityEngine.Quaternion.LookRotation(playerToMouse);
                transform.rotation = newRotation;
                PlayerRotationWriter.Send(new PlayerRotation.Update().SetBearing(transform.eulerAngles.y));

            }
        }
    }

}