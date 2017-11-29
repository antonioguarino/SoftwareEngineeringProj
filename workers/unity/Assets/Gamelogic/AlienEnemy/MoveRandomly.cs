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
		[Require] private Health.Writer HealthWriter;
		[Require] private PlayerRotation.Writer PlayerRotationWriter;
		private void OnEnable()
		{
			// Change steering decisions every five seconds

			InvokeRepeating("RandomizeSteering", 0, 5.0f);
			InvokeRepeating("RandomizeShooting", 0, 10.0f);

		}

		private void OnDisable()
		{
			CancelInvoke("RandomizeSteering");
			CancelInvoke("RandomizeShooting");
		}

		private void RandomizeSteering()
		{
			var xAxis = Random.Range (-0.9f, 0.9f);
			var yAxis = Random.Range (-0.9f, 0.9f);

			var update = new PlayerInput.Update();
			update.SetJoystick(new Joystick(xAxis, yAxis));
			PlayerInputWriter.Send(update);

			PlayerRotationWriter.Send(new PlayerRotation.Update().SetBearing( Random.Range(0f,360f)));

		}
		private void RandomizeShooting()
		{
			var update = new PlayerInput.Update();
			PlayerInputWriter.Send (new PlayerInput.Update ().AddFire (new Fire ()));
		}

	}
}