using Assets.Gamelogic.Core;
using Assets.Gamelogic.Player;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class PlayerMover : MonoBehaviour
{

    [Require] private Position.Writer PositionWriter;
    [Require] private Rotation.Writer RotationWriter;
    [Require] private PlayerInput.Reader PlayerInputReader;
    //[Require] private PlayerRotation.Writer PlayerRotationWriter;
	public float speed;
    private Rigidbody rigidbody;

    void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var joystick = PlayerInputReader.Data.joystick;
        Vector3 direction = new Vector3(joystick.xAxis, 0, joystick.yAxis);
        direction = direction.normalized * speed * Time.deltaTime;
        rigidbody.MovePosition(transform.position + direction);

        var pos = rigidbody.position;
        var positionUpdate = new Position.Update()
            .SetCoords(new Coordinates(pos.x, pos.y, pos.z));
        PositionWriter.Send(positionUpdate);


    //    transform.rotation = Quaternion.Euler(new Vector3(0, yaw, 0));
        var rotationUpdate = new Rotation.Update()
            .SetRotation(rigidbody.rotation.ToNativeQuaternion());
        RotationWriter.Send(rotationUpdate);
        //PlayerRotationWriter.Send(new PlayerRotation.Update().SetBearing(transform.eulerAngles.y));
    }
}
//[WorkerType(WorkerPlatform.UnityWorker)]
//public class PlayerMover : MonoBehaviour
//{

//    [Require] private Position.Writer PositionWriter;
//    [Require] private Rotation.Writer RotationWriter;
//    [Require] private PlayerInput.Reader PlayerInputReader;
//    [Require] private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;

//    private new Rigidbody rigidbody;
//    private float speed = 6f;
//    float camRayLength = 100f;
//    int floorMask;
//    private Camera camera;

//    void OnEnable()
//    {
//        floorMask = LayerMask.GetMask("Floor"); //Acquisisco il layer Floor
//        rigidbody = GetComponent<Rigidbody>(); //Acquisisco rigidbody
//        camera = Camera.main; //Acquisisco camera
//        camera.transform.parent = transform; //Sposto la camera come oggetto figlio del playerPrefab
//    }

//    //Aggiorna e Restituisce la posizione corrente del rigidbody associato al playerPrefab
//    public Vector3 Move()
//    {
//        var joystick = PlayerInputReader.Data.joystick;
//        Vector3 direction = new Vector3(joystick.xAxis, 0, joystick.yAxis);
//        direction = direction * speed * Time.deltaTime;
//        rigidbody.MovePosition(transform.position + direction);

//        return rigidbody.position;

//    }

//    // Aggiorna e Restituisce l'orientamento corrente del rigidbody associato al playerPrefab
//    public Improbable.Core.Quaternion Turn()
//    {
//        Ray camRay = camera.ScreenPointToRay(Input.mousePosition);
//        RaycastHit floorHit;
//        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
//        {
//            Vector3 playerToMouse = floorHit.point - transform.position;
//            playerToMouse.y = 0f;

//            return UnityEngine.Quaternion.LookRotation(playerToMouse).ToNativeQuaternion();
//        }
//        return rigidbody.rotation.ToNativeQuaternion();
//    }
//}