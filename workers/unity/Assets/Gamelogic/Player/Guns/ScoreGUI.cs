
using Improbable.Player;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic.Player.Guns
{
	// Add this MonoBehaviour on client workers only
	[WorkerType(WorkerPlatform.UnityClient)]
	public class ScoreGUI : MonoBehaviour
	{
		/*
         * Client will only have write access for their own designated PlayerShip entity's ShipControls component,
         * so this MonoBehaviour will be enabled on the client's designated PlayerShip GameObject only and not on
         * the GameObject of other players' ships.
         */
		[Require] private PlayerInput.Writer PlayerInputWriter;
		[Require] private Score.Reader ScoreReader;

		private GameObject scoreCanvasUI;
		private Text totalPointsGUI;

		private void Awake()
		{
			scoreCanvasUI= GameObject.Find("ScoreCanvas");
			if (scoreCanvasUI != null) {
				totalPointsGUI = scoreCanvasUI.GetComponentInChildren<Text>();
				scoreCanvasUI.SetActive(false);
				updateGUI(0);
			}
		}

		private void OnEnable()
		{
			// Register callback for when components change
			//ScoreReader.NumberOfPointsUpdated.Add(OnNumberOfPointsUpdated);
			ScoreReader.ScoreUpdated.Add(OnNumberOfPointsUpdated);
		}

		private void OnDisable()
		{
			// Deregister callback for when components change
			//ScoreReader.NumberOfPointsUpdated.Remove(OnNumberOfPointsUpdated);
			ScoreReader.ScoreUpdated.Remove(OnNumberOfPointsUpdated);
		}

		// Callback for whenever one or more property of the Score component is updated
		private void OnNumberOfPointsUpdated(int numberOfPoints)
		{
			updateGUI(numberOfPoints);
		}

		void updateGUI(int score)
		{
			if (scoreCanvasUI != null) {
				if (score >= 0)
				{
					scoreCanvasUI.SetActive(true);
					totalPointsGUI.text = "Score: " + score.ToString();
					Debug.LogError (score.ToString());
				}
				else
				{
					scoreCanvasUI.SetActive(false);
				}
			}
		}
	}
}
