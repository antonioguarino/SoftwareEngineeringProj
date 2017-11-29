using Improbable;
using Improbable.Unity.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

namespace Assets.Gamelogic.Player.Guns
{
	
	// Add this MonoBehaviour on UnityWorker (server-side) workers only
	[WorkerType(WorkerPlatform.UnityWorker)]

	public class TakeDamage : MonoBehaviour
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

			if (other != null && (other.gameObject.tag == "Bullet" || other.gameObject.tag == "BulletAlien" || other.gameObject.tag == "Shield") )
			{
				// Reduce health of this entity when hit
				//Debug.LogError("Collision detected with " + gameObject.EntityId());
				// Reduce health of this entity when hit
				int newHealth = HealthWriter.Data.currentHealth - 100;
				HealthWriter.Send(new Health.Update().SetCurrentHealth(newHealth));
				//Destroy (other.gameObject);
				if (newHealth <= 0) {
					Debug.LogError ("---");
					Debug.LogError (other.GetComponent<Guns.DestroyBullet>().firerEntityId.Value.Id);
					AwardPointsForKill (new EntityId (other.GetComponent<Guns.DestroyBullet>().firerEntityId.Value.Id));

				}
			}
		}
		private void AwardPointsForKill(EntityId firerEntityId)
		{
			int pointsToAward = 1;
			// Use Commands API to issue an AwardPoints request to the entity who fired the cannonball
			SpatialOS.Commands.SendCommand(HealthWriter, Score.Commands.AwardPoints.Descriptor, new AwardPoints(pointsToAward), firerEntityId)
				.OnSuccess(OnAwardPointsSuccess)
				.OnFailure(OnAwardPointsFailure);
		}

		private void OnAwardPointsSuccess(AwardResponse response)
		{
			Debug.Log("AwardPoints command succeeded. Points awarded: " + response.amount);
		}

		private void OnAwardPointsFailure(ICommandErrorDetails response)
		{
			Debug.LogError("Failed to send AwardPoints command with error: " + response.ErrorMessage);
		}
	
	}
	
	
}
