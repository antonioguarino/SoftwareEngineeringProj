using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gamelogic.AlienEnemy
{
	public class Gun : MonoBehaviour
	{
		[SerializeField]
		private GameObject BulletAlien;

		private Collider[] firerColliders;


		void Start()
		{
			
			firerColliders = gameObject.GetComponentsInChildren<Collider>();
		}

		public void Fire()
		{
			

			if (BulletAlien != null)
			{
				var bullet = Instantiate(BulletAlien, transform.position, transform.rotation) as GameObject;
				var entityId = bullet.EntityId();
				bullet.GetComponent<DestroyBullet>().firerEntityId = entityId;
				bullet.GetComponent<BulletController> ().firerEntityId = entityId;
				EnsureCannonBallWillNotCollideWithFirer(bullet);

				//cannonFireAudioSource.PlayOneShot(cannonFireAudioClips[Random.Range(0, cannonFireAudioClips.Length)]);
			}
		}


		private void EnsureCannonBallWillNotCollideWithFirer(GameObject cannonball)
		{
			if (firerColliders == null) return;
			var col = cannonball.GetComponent<Collider>();
			if (col == null) return;
			foreach (var c in firerColliders)
			{
				Physics.IgnoreCollision(c, col);
			}
		}
	}
}