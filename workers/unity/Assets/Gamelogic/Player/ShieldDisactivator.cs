using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Player;
using Improbable.Unity.Visualizer;


namespace Assets.Gamelogic.Player
{
public class ShieldDisactivator : MonoBehaviour {

	[Require] private PlayerInput.Reader PlayerInputReader;
	private MeshRenderer shieldRend;
	private SphereCollider sphereColl;


	private void Start () 

	{
		shieldRend = GetComponent<MeshRenderer> ();
		sphereColl = GetComponent<SphereCollider>();

	}

	private void OnEnable()
	{
		PlayerInputReader.NotShieldTriggered.Add(OffShield);

	}

	private void OnDisable()
	{
		PlayerInputReader.NotShieldTriggered.Remove(OffShield);

	}

		private void OffShield(NotShield not_shield)
	{
		shieldRend.enabled = false;
		sphereColl.enabled = false;
	}
}
}