using Assets.Gamelogic.Core;
using Assets.Gamelogic.Utils;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Gamelogic.Player
{
	[WorkerType(WorkerPlatform.UnityClient)]
	public class SendClientConnection : MonoBehaviour
	{
		[Require]
		private ClientAuthorityCheck.Writer ClientAuthorityCheckWriter;
		private InputField splashInput;
		private Text usernameText;
		private string username;
		private Coroutine heartbeatCoroutine;

		private void OnEnable()
		{	
			splashInput = GameObject.FindObjectOfType<InputField> ();
			username = splashInput.text;
			Debug.LogError ("InputField" + username);

			SceneManager.UnloadSceneAsync(BuildSettings.SplashScreenScene);

			usernameText = GameObject.FindGameObjectWithTag ("UserText").GetComponent<Text> ();
			usernameText.text = username;
			Debug.LogError (usernameText.text);

			heartbeatCoroutine = StartCoroutine(TimerUtils.CallRepeatedly(SimulationSettings.HeartbeatSendingIntervalSecs, SendHeartbeat));
		}

		private void OnDisable()
		{
			StopCoroutine(heartbeatCoroutine);
		}

		private void SendHeartbeat()
		{
			SpatialOS.Commands.SendCommand(ClientAuthorityCheckWriter, ClientConnection.Commands.Heartbeat.Descriptor, new HeartbeatRequest(), gameObject.EntityId());
		}

		private void OnApplicationQuit()
		{
			if (SpatialOS.IsConnected)
			{
				SpatialOS.Commands.SendCommand(ClientAuthorityCheckWriter, ClientConnection.Commands.DisconnectClient.Descriptor, new ClientDisconnectRequest(), gameObject.EntityId());
			}
		}
	}
}
