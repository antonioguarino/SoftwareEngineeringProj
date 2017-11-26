using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gamelogic.AlienEnemy
{
	public class Gun : MonoBehaviour
	{
		[SerializeField]
		private GameObject BulletPrefab;
		private Collider[] firerColliders;
		[SerializeField]
		private GameObject gunEnd;


		public void Fire(Vector3 dir)
		{

			if (BulletPrefab != null)
			{
				
				Vector3 gunEndV = gunEnd.transform.position;
				//gunEnd.y = 4f;
				var bullet = Instantiate(BulletPrefab, gunEndV+dir, transform.rotation) as GameObject;
				var entityId = gameObject.EntityId();
				firerColliders = BulletPrefab.GetComponentsInChildren<Collider>();
				bullet.GetComponent<DestroyAlienBullet>().firerEntityId = entityId;
				bullet.GetComponent<BulletController> ().firerEntityId = entityId;

				EnsureBulletWillNotCollideWithFirer(bullet);
			}
		}

		private void EnsureBulletWillNotCollideWithFirer(GameObject bullet)
		{
			if (firerColliders == null) return;
			var col = bullet.GetComponent<Collider>();
			if (col == null) return;
			foreach (var c in firerColliders)
			{
				Physics.IgnoreCollision(c, col);
			}
		}

	}
}