using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Player;
using Improbable.Unity.Visualizer;

namespace Assets.Gamelogic.Player.Guns
{

	// This MonoBehaviour will be enabled on both client and server-side workers
	public class GunFirer : MonoBehaviour
	{
		[Require] private PlayerInput.Reader PlayerInputReader;
		private RaycastShoot gun;

		private void Start()
		{
			// Cache entity's Gun gameobject
			gun = gameObject.GetComponent<RaycastShoot>();

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
			var joystick = PlayerInputReader.Data.joystick;
			Vector3 direction = new Vector3(joystick.xAxis,0 , joystick.yAxis);
			if (gun != null)
			{
				gun.setDir (direction);
				gun.setFire (true);

				//gun.Fire(direction);
			}

		}


	}
}
