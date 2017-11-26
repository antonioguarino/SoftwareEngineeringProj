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
			var positionArray = new [] { new Vector3(167f,0.02f,-88f),new Vector3(-55f,0.02f,84f),new Vector3(-166f,0.02f,-106f), new Vector3(98f,0.02f,59f)};

			for (var i = 0; i < SimulationSettings.TotalAliens; i++)
			{
				// Choose a starting position for this pirate entity
				var alienCoordinates = positionArray[Random.Range(0,4)];
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
