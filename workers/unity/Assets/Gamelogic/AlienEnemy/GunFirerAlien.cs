using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Player;
using Improbable.Unity.Visualizer;

namespace Assets.Gamelogic.AlienEnemy
{
public class GunFirerAlien : MonoBehaviour {
		// This MonoBehaviour will be enabled on both client and server-side workers
		[Require] private PlayerInput.Reader PlayerInputReader;
			
		private Gun gun;

		private void Start()
			{

			gun = gameObject.GetComponent<Gun>();
				
			}

		private void OnEnable()
		{
			PlayerInputReader.FireTriggered.Add(OnFire);
		}

		private void OnDisable()
		{
			PlayerInputReader.FireTriggered.Remove(OnFire);

		}

		private void OnFire(Fire fire)
		{
			// Respond to Fire event
			// Respond to Fire event
			if (gun != null){
				gun.Fire();
				}

		}
	}
}