using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

[WorkerType(WorkerPlatform.UnityClient)]
public class PlayerInputSender : MonoBehaviour
{

    [Require] private PlayerInput.Writer PlayerInputWriter;
    bool readyShield=true;
  	bool usingShield=false;
    private float SecondsUntilDisableShield = 8f;
    private float spawnTime;

    void Update()
    {
        var xAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");

        var update = new PlayerInput.Update();
        update.SetJoystick(new Joystick(xAxis, yAxis));
        PlayerInputWriter.Send(update);

        if (xAxis != 0 || yAxis != 0)
        {
            PlayerInputWriter.Send(new PlayerInput.Update().AddMove(new Move()));
        }
        else
        {
            PlayerInputWriter.Send(new PlayerInput.Update().AddStop(new Stop()));
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
			PlayerInputWriter.Send (new PlayerInput.Update ().AddFire (new Fire ()));
		}


    if ((Input.GetKeyDown (KeyCode.P) || usingShield) && readyShield) {
      //Debug.LogError ("using shield: " + usingShield + "  readyshield: " + readyShield);
      if (usingShield == false) {
        spawnTime = Time.time;

      }
      if ((Time.time - spawnTime) < SecondsUntilDisableShield) {
       // Debug.LogError ("Secondi:  " + (Time.time - spawnTime));
        usingShield = true;
        PlayerInputWriter.Send (new PlayerInput.Update ().AddShield (new Shield ()));

      } else {
        //Debug.LogError ("ho disabilitato");
        PlayerInputWriter.Send (new PlayerInput.Update ().AddNotShield (new NotShield ()));
        usingShield = false;

    }
    }
    }
}
