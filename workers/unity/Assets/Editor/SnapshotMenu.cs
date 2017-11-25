using Assets.Gamelogic.Core;
using Assets.Gamelogic.EntityTemplates;
using Improbable;
using Improbable.Worker;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor 
{
	public class SnapshotMenu : MonoBehaviour
	{
		[MenuItem("Improbable/Snapshots/Generate Default Snapshot")]
		private static void GenerateDefaultSnapshot()
		{
			var snapshotEntities = new Dictionary<EntityId, Entity>();
			var currentEntityId = 1;

			snapshotEntities.Add(new EntityId(currentEntityId++), EntityTemplateFactory.CreatePlayerCreatorTemplate());

			PopulateSnapshotWithAlienEntities(ref snapshotEntities, ref currentEntityId);


			//snapshotEntities.Add(new EntityId(currentEntityId++), EntityTemplateFactory.CreateCubeTemplate());

			SaveSnapshot(snapshotEntities);
		}

		public static void PopulateSnapshotWithAlienEntities(ref Dictionary<EntityId, Entity> snapshotEntities, ref int nextAvailableId)
		{
			var positionArray = new [] { new Vector3(-221f,0.3f,199f),new Vector3(-198f,0.3f,-209f),new Vector3(199f,0.3f,221f), new Vector3(209f,0.3f,-190f), new Vector3(-23f,0f,-296f), new Vector3(55f,0f,298f), new Vector3(-331f,0f,29f), new Vector3(275f,0f,-45f) };

			for (var i = 0; i < SimulationSettings.TotalAliens; i++)
			{
				// Choose a starting position for this pirate entity
				var alienCoordinates = positionArray[Random.Range(0,8)];
				float alienRotation = Random.Range(0f,360f);

				snapshotEntities.Add(new EntityId(nextAvailableId++), EntityTemplateFactory.CreateAlienTemplate(alienCoordinates, alienRotation));
			}
		}

		private static void SaveSnapshot(IDictionary<EntityId, Entity> snapshotEntities)
		{
			File.Delete(SimulationSettings.DefaultSnapshotPath);
			var maybeError = Snapshot.Save(SimulationSettings.DefaultSnapshotPath, snapshotEntities);

			if (maybeError.HasValue)
			{
				Debug.LogErrorFormat("Failed to generate initial world snapshot: {0}", maybeError.Value);
			}
			else
			{
				Debug.LogFormat("Successfully generated initial world snapshot at {0}", SimulationSettings.DefaultSnapshotPath);
			}
		}
	}
}
