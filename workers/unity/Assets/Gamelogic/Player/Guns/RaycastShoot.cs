using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Gamelogic.Player.Guns{
public class RaycastShoot: MonoBehaviour {
		
	[SerializeField]
	private GameObject BulletPrefab;


	public int gunDamage = 1;                                           
	public float fireRate = .100f;                                       
	public float weaponRange = 80f;                                   
	//public float hitForce = 100f;
	public Transform gunEnd;
	//public GameObject player;
	private WaitForSeconds shotDuration = new WaitForSeconds(.07f);

	private TakeDamage tD;
	//private AudioSource gunAudio;

	private LineRenderer laserLine;
	private float nextFire; 
	private Vector3 dir;
	private bool is_firing=false;
	
	public void setDir(Vector3 dircetion){
		dir = dircetion;
	}
	public void setFire(bool ifFire){
		is_firing=ifFire;
	}

	void Start () 

	{
		laserLine = GetComponent<LineRenderer> ();
		
		
		//gunAudio = GetComponent<AudioSource> ();
	}
	
	void Update(){
			
			if(is_firing && Time.time > nextFire){
				nextFire = Time.time + fireRate;
				StartCoroutine (ShotEffect());

				RaycastHit hit;
				//Vector3 altezza = new Vector3 (0.6f, gunEnd.position.y, 0) ;
				laserLine.SetPosition (0,gunEnd.position+dir);

				if (Physics.Raycast (gunEnd.position+dir, transform.forward, out hit, weaponRange)) {
					laserLine.SetPosition (1, hit.point);
						if (BulletPrefab != null) {
						
						var bullet = Instantiate (BulletPrefab, hit.transform.position, hit.transform.rotation) as GameObject;
							var entityId = gameObject.EntityId ();
							bullet.GetComponent<DestroyBullet>().firerEntityId = entityId;
							//bullet.GetComponent<BulletController> ().firerEntityId = entityId;
						}


				}else {
					laserLine.SetPosition (1, gunEnd.position+dir + (transform.forward * weaponRange));
				}
				is_firing = false;
				}
			}


			

	private IEnumerator ShotEffect()
		{
			//gunAudio.Play ();
			laserLine.enabled = true;

			yield return shotDuration;

			laserLine.enabled = false;
		}

}
}