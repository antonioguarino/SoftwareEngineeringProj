using Assets.Gamelogic.Core;
using Assets.Gamelogic.Player;
using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Gamelogic.AlienEnemy
{

	[WorkerType(WorkerPlatform.UnityWorker)]
	public class RespawnAlien : MonoBehaviour
	{
		[Require] private Position.Writer PositionWriter;
		[Require] private Health.Writer HealthWriter;
		[Require] private PlayerInput.Writer PlayerInputWriter;
		//private Vector3[] positionArray = new [] { new Vector3(167f,0.02f,-88f),new Vector3(-55f,0.02f,84f),new Vector3(-166f,0.02f,-106f), new Vector3(98f,0.02f,59f)};
		private Rigidbody rigidbody;

		void OnEnable()
		{
			rigidbody = GetComponent<Rigidbody>();

		}
			
		void FixedUpdate()
		{

			int actualHealth = HealthWriter.Data.currentHealth;
			if (actualHealth == 0) {
				rigidbody.position =  new Vector3(0f,0f,0f);
				var pos = rigidbody.position;
				//Debug.LogError ("respawn in position: " + pos.x + "," + pos.y + "," + pos.z);
				var positionUpdate = new Position.Update ()
					.SetCoords (new Coordinates (pos.x, pos.y, pos.z));
				PositionWriter.Send (positionUpdate);
				HealthWriter.Send (new Health.Update ().SetCurrentHealth (1000));
			}

		}


	}

}
