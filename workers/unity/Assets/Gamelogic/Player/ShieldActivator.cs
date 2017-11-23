using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Player;
using Improbable.Unity.Visualizer;


namespace Assets.Gamelogic.Player
{
public class ShieldActivator : MonoBehaviour {

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
		PlayerInputReader.ShieldTriggered.Add(OnShield);

	}

	private void OnDisable()
	{
		PlayerInputReader.ShieldTriggered.Remove(OnShield);

	}

	private void OnShield(Shield shield)
	{
			shieldRend.enabled = true;
			sphereColl.enabled = true;
	}
	

}
}