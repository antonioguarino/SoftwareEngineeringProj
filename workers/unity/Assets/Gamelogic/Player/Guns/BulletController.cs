using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable;
using Improbable.Collections;

public class BulletController : MonoBehaviour {
	public float speed;

	public Option<EntityId> firerEntityId = new Option<EntityId>();
	// Use this for initialization
	void Start () {
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * speed* Time.deltaTime);
	}
}
