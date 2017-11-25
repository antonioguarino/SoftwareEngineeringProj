using System.Collections;
using System.Collections.Generic;
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.AlienEnemy
{
	// Add this MonoBehaviour on client workers only
	[WorkerType(WorkerPlatform.UnityClient)]
	public class DeadBehaviourAlien : MonoBehaviour
	{
		// Inject access to the entity's Health component
		[Require] private Health.Reader HealthReader;
		private Animator _animator;

		public bool alreadyDead = false;

		private void OnEnable()
		{
			
			_animator = GetComponent<Animator> ();
			alreadyDead = false;
			// Register callback for when components change
			HealthReader.CurrentHealthUpdated.Add(OnCurrentHealthUpdated);
		}

		private void OnDisable()
		{
			// Deregister callback for when components change
			HealthReader.CurrentHealthUpdated.Remove(OnCurrentHealthUpdated);
		}



		// Callback for whenever the CurrentHealth property of the Health component is updated
		private void OnCurrentHealthUpdated(int currentHealth)
		{
			if (!alreadyDead && currentHealth <= 0)
			{
				_animator.Play("dead");
				alreadyDead = true;

			}
		}
	}
}