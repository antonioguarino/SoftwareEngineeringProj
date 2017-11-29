using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;
using UnityEngine.UI;
using Improbable.Util;


namespace Assets.Gamelogic.Player
{
	public class HealthBar : MonoBehaviour
	{
		
		// Inject access to the entity's Health component
		[Require] private Health.Reader HealthReader;
		/*
		private void OnEnable()
		{
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

			if(currentHealth >= 0 ){
				float cur = currentHealth;
				float tot = 1000;
				float tmp =cur/tot;
				transform.localScale = new Vector3 (tmp, 1, 1);
			}

		}*/
		void Update()
		{
			

			float cur = HealthReader.Data.currentHealth;
			if (cur >= 0) {
				float tot = 1000;
				float tmp =cur/tot;
				transform.localScale = new Vector3 (tmp, 1, 1);
			}

		}

	}
}