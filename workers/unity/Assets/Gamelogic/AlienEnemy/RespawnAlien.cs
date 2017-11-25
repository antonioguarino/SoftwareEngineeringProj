using Assets.Gamelogic.Core;
using Assets.Gamelogic.Player;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Gamelogic.AlienEnemy
{

	[WorkerType(WorkerPlatform.UnityWorker)]
	public class RespawnAlien : MonoBehaviour
	{
		[Require] private Position.Writer PositionWriter;
		[Require] private Health.Writer HealthWriter;
		[Require] private PlayerInput.Writer PlayerInputWriter;
		private Vector3[] positionArray = new [] { new Vector3(-221f,1f,199f),new Vector3(-198f,1f,-209f),new Vector3(199f,1f,221f), new Vector3(209f,1f,-190f), new Vector3(-23f,1f,-296f), new Vector3(55f,1f,298f), new Vector3(-331f,1f,29f), new Vector3(275f,1f,-45f) };
		private Rigidbody rigidbody;

		void OnEnable()
		{
			rigidbody = GetComponent<Rigidbody>();

		}

		void FixedUpdate()
		{

			int actualHealth = HealthWriter.Data.currentHealth;
			if (actualHealth <= 0) {
					HealthWriter.Send (new Health.Update ().SetCurrentHealth (1000));
					rigidbody.position = positionArray [Random.Range (0, 8)];
					var pos = rigidbody.position;
					Debug.LogError ("respawn in position: " + pos.x + "," + pos.y + "," + pos.z);
					var positionUpdate = new Position.Update ()
					.SetCoords (new Coordinates (pos.x, pos.y, pos.z));
					PositionWriter.Send (positionUpdate);

			}


		
		}





	}

}
