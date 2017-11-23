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

namespace Assets.Gamelogic.Player
{

[WorkerType(WorkerPlatform.UnityWorker)]
public class Respawn : MonoBehaviour
{
	[Require] private Position.Writer PositionWriter;
	[Require] private Health.Writer HealthWriter;
	private Vector3[] positionArray = new [] { new Vector3(-221f,0.3f,199f),new Vector3(-198f,0.3f,-209f),new Vector3(199f,0.3f,221f), new Vector3(209f,0.3f,-190f), new Vector3(-23f,0f,-296f), new Vector3(55f,0f,298f), new Vector3(-331f,0f,29f), new Vector3(275f,0f,-45f) };
	private Rigidbody rigidbody;

		void OnEnable()
		{
			rigidbody = GetComponent<Rigidbody>();
		}

		void FixedUpdate()
		{
			
			int actualHealth = HealthWriter.Data.currentHealth;
			if (actualHealth <= 0) {
				HealthWriter.Send(new Health.Update().SetCurrentHealth(1000));
				//Qui si deve modificare il valori di pos come cordinate di respawn e mandarle al server!
				//quindi cambiare prima il rigidbody.position e pou usare queste cordinate per mandarle al server
				rigidbody.position=positionArray[Random.Range(0,8)];
				var pos = rigidbody.position;
				Debug.LogError ("respawn in position: " + pos.x + "," + pos.y + "," + pos.z);
				var positionUpdate = new Position.Update ()
				.SetCoords (new Coordinates (pos.x, pos.y, pos.z));
				PositionWriter.Send (positionUpdate);
			}

		}

	}

}
