using Improbable;
using Improbable.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

namespace Assets.Gamelogic.AlienEnemy
{

	// Add this MonoBehaviour on UnityWorker (server-side) workers only
	[WorkerType(WorkerPlatform.UnityWorker)]

	public class TakeDamageAlien : MonoBehaviour
	{

		// Enable this MonoBehaviour only on the worker with write access for the entity's Health component
		[Require] private Health.Writer HealthWriter;


		private void OnTriggerEnter(Collider other)
		{

			if (HealthWriter == null)
				return;

			// Ignore collision if this player is already dead
			if (HealthWriter.Data.currentHealth <= 0)
				return;

			if (other != null && (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Shield") )
			{
				int newHealth = HealthWriter.Data.currentHealth - 250;

					HealthWriter.Send (new Health.Update ().SetCurrentHealth (newHealth));

			}
		}


	}


}
