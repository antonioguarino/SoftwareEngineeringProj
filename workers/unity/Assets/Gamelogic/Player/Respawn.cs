﻿using Assets.Gamelogic.Core;
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
	private Vector3[] positionArray = new [] { new Vector3(-72.3f,0f,64.3f),new Vector3(73.7f,0f,82.9f),new Vector3(61.3f,0f,-91.1f), new Vector3(-41.7f,0f,-78.7f) };
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
				rigidbody.position=positionArray[Random.Range(0,4)];
				var pos = rigidbody.position;
				Debug.LogError ("respawn in position: " + pos.x + "," + pos.y + "," + pos.z);
				var positionUpdate = new Position.Update ()
				.SetCoords (new Coordinates (pos.x, pos.y, pos.z));
				PositionWriter.Send (positionUpdate);
			}

		}

	}

}
