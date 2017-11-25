using System.Collections;
using System.Collections.Generic;
using Improbable;
using Improbable.Collections;
using UnityEngine;

namespace Assets.Gamelogic.AlienEnemy
{
public class DestroyAlienBullet : MonoBehaviour {

		public Option<EntityId> firerEntityId = new Option<EntityId>();

		[SerializeField]
		public float SecondsUntilDestroy = 5f;
		private float spawnTime;

		void Start()
		{
			spawnTime = Time.time;
		}

		void Update()
		{
			var lifeTime = Time.time - spawnTime;
			if (lifeTime > SecondsUntilDestroy){
				Destroy(gameObject);
			}

		}
	}
}
