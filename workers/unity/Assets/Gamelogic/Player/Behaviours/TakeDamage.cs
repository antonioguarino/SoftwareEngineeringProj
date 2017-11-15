using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

namespace Assets.Gamelogic.Player.Behaviours
{
	
	// Add this MonoBehaviour on UnityWorker (server-side) workers only
	[WorkerType(WorkerPlatform.UnityWorker)]
	public class TakeDamage : MonoBehaviour
	{
		// Enable this MonoBehaviour only on the worker with write access for the entity's Health component
		[Require] private Health.Writer HealthWriter;
		public int damageBullet;

		private void OnTriggerEnter(Collider other)
		{
			/*
             * Unity's OnTriggerEnter runs even if the MonoBehaviour is disabled, so non-authoritative UnityWorkers
             * must be protected against null writers
             */
			if (HealthWriter == null)
				return;

			// Ignore collision if this player is already dead
			if (HealthWriter.Data.currentHealth <= 0)
				return;

			if (other != null && other.gameObject.tag == "Bullet")
			{
				// Reduce health of this entity when hit
				//Debug.LogError("Collision detected with " + gameObject.EntityId());
				// Reduce health of this entity when hit
				int newHealth = HealthWriter.Data.currentHealth - damageBullet;
				HealthWriter.Send(new Health.Update().SetCurrentHealth(newHealth));
			}
		}
	}
}
