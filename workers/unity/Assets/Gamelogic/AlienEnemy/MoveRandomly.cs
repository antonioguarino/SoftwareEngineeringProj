using Assets.Gamelogic.Core;
using Assets.Gamelogic.Player;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

namespace Assets.Gamelogic.AlienEnemy
{
	// Add this MonoBehaviour on UnityWorker (server-side) workers only
	[WorkerType(WorkerPlatform.UnityWorker)]
	public class MoveRandomly : MonoBehaviour
	{
		
		[Require] private PlayerInput.Writer PlayerInputWriter;

		private void OnEnable()
		{
			// Change steering decisions every five seconds
			InvokeRepeating("RandomizeSteering", 0, 5.0f);
		}

		private void OnDisable()
		{
			CancelInvoke("RandomizeSteering");
		}
			
		private void RandomizeSteering()
		{

			var xAxis = Random.Range (-0.9f, 0.9f);
			var yAxis = Random.Range (-0.9f, 0.9f);

			var update = new PlayerInput.Update();
			update.SetJoystick(new Joystick(xAxis, yAxis));
			PlayerInputWriter.Send(update);


		}

	}
}