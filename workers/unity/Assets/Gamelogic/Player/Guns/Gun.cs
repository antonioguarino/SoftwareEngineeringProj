using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Gamelogic.Player.Guns
{
	public class Gun : MonoBehaviour
	{
		[SerializeField]
		private GameObject BulletPrefab;
		private Collider[] firerColliders;


		void Start()
		{
			
			//firerColliders = gameObject.GetComponentsInChildren<Collider>();
		}

		public void Fire(Vector3 dir)
		{

			if (BulletPrefab != null)
			{
				var bullet = Instantiate(BulletPrefab, transform.position+dir, transform.rotation) as GameObject;
				var entityId = gameObject.EntityId();
				bullet.GetComponent<DestroyBullet>().firerEntityId = entityId;
				bullet.GetComponent<BulletController> ().firerEntityId = entityId;

				//EnsureBulletWillNotCollideWithFirer(bullet);
			}
		}
		/*
		private void EnsureBulletWillNotCollideWithFirer(GameObject bullet)
		{
			if (firerColliders == null) return;
			var col = bullet.GetComponent<Collider>();
			if (col == null) return;
			foreach (var c in firerColliders)
			{
				Physics.IgnoreCollision(c, col);
			}
		}*/

	}
}