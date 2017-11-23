using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using Improbable.Player;

namespace Assets.Gamelogic.Player
{
    public class SoldierAnimation : MonoBehaviour
    {

        [Require] private PlayerInput.Reader PlayerInputReader;
        public Animator animator;


        // Use this for initialization
        private void OnEnable()
        {
            PlayerInputReader.MoveTriggered.Add(OnMove);
            PlayerInputReader.StopTriggered.Add(OnStop);
        }

        private void OnDisable()
        {
            PlayerInputReader.MoveTriggered.Remove(OnMove);
            PlayerInputReader.StopTriggered.Remove(OnStop);
        }

        // Update is called once per frame
        private void OnMove(Move move)
        {
            // Respond to FireLeft event
            animator.SetBool("Moving", true);
            animator.SetBool("Running", true);
        }

        private void OnStop(Stop stop)
        {
            // Respond to FireLeft event
            animator.SetBool("Moving", false);
            animator.SetBool("Running", false);
        }
    }
}
