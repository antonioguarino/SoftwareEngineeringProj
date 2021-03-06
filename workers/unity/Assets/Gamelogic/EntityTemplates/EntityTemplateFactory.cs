﻿using Assets.Gamelogic.Core;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Unity.Core.Acls;
using Improbable.Worker;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine;
using Improbable.Unity.Entity;

namespace Assets.Gamelogic.EntityTemplates
{
	public class EntityTemplateFactory : MonoBehaviour
	{
		private Vector3[] positionArray;

		public static Entity CreatePlayerCreatorTemplate()
		{
			var playerCreatorEntityTemplate = EntityBuilder.Begin()
				//.AddPositionComponent(new Improbable.Coordinates( SimulationSettings.PlayerXPos[Random.Range(0,4)], SimulationSettings.PlayerSpawnHeight,  SimulationSettings.PlayerXPos[Random.Range(0,4)]).ToUnityVector(), CommonRequirementSets.PhysicsOnly)
				.AddPositionComponent(Improbable.Coordinates.ZERO.ToUnityVector(), CommonRequirementSets.PhysicsOnly)
				.AddMetadataComponent(entityType: SimulationSettings.PlayerCreatorPrefabName)
				.SetPersistence(true)
				.SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
				.AddComponent(new Rotation.Data(Quaternion.identity.ToNativeQuaternion()), CommonRequirementSets.PhysicsOnly)
				.AddComponent(new PlayerCreation.Data(), CommonRequirementSets.PhysicsOnly)
				.Build();

			return playerCreatorEntityTemplate;
		}

		public static Entity CreatePlayerTemplate(string clientId)
		{
			var playerTemplate = EntityBuilder.Begin()
				.AddPositionComponent(new Improbable.Coordinates( SimulationSettings.PlayerXPos[Random.Range(0,4)], SimulationSettings.PlayerSpawnHeight,  SimulationSettings.PlayerXPos[Random.Range(0,4)]).ToUnityVector(), CommonRequirementSets.PhysicsOnly)
				.AddMetadataComponent(entityType: SimulationSettings.PlayerPrefabName)
				.SetPersistence(false)
				.SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
				.AddComponent(new Rotation.Data(Quaternion.identity.ToNativeQuaternion()), CommonRequirementSets.PhysicsOnly)
				.AddComponent(new ClientAuthorityCheck.Data(), CommonRequirementSets.SpecificClientOnly(clientId))
				.AddComponent(new ClientConnection.Data(SimulationSettings.TotalHeartbeatsBeforeTimeout), CommonRequirementSets.PhysicsOnly)
				.AddComponent(new PlayerInput.Data(new Joystick(xAxis: 0, yAxis: 0)), CommonRequirementSets.SpecificClientOnly(clientId))
				.AddComponent(new PlayerRotation.Data(0), CommonRequirementSets.SpecificClientOnly(clientId))
				.AddComponent(new Health.Data(1000), CommonRequirementSets.PhysicsOnly)
				.AddComponent(new Score.Data(0), CommonRequirementSets.PhysicsOnly)
				.Build();
			
			return playerTemplate;
		}

		public static Entity CreateAlienTemplate(Vector3 initialPosition,float rotation)
		{
			var alienTemplate = EntityBuilder.Begin()
				.AddPositionComponent(new Improbable.Coordinates(initialPosition.x, SimulationSettings.PlayerSpawnHeight, initialPosition.z).ToUnityVector(), CommonRequirementSets.PhysicsOnly)
				.AddMetadataComponent(entityType: SimulationSettings.AlienPrefabName)
				.SetPersistence(true)
				.SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
				.AddComponent(new Rotation.Data(Quaternion.identity.ToNativeQuaternion()), CommonRequirementSets.PhysicsOnly)
				.AddComponent(new PlayerInput.Data(new Joystick(xAxis: 0, yAxis: 0)), CommonRequirementSets.PhysicsOnly)
				.AddComponent(new PlayerRotation.Data(rotation), CommonRequirementSets.PhysicsOnly)
				.AddComponent(new Health.Data(1000), CommonRequirementSets.PhysicsOnly)
				.Build();

			return alienTemplate;
		}

		/*public static Entity CreateCubeTemplate()
        {
            var cubeTemplate = EntityBuilder.Begin()
                .AddPositionComponent(Improbable.Coordinates.ZERO.ToUnityVector(), CommonRequirementSets.PhysicsOnly)
                .AddMetadataComponent(entityType: SimulationSettings.CubePrefabName)
                .SetPersistence(true)
                .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
                .AddComponent(new Rotation.Data(Quaternion.identity.ToNativeQuaternion()), CommonRequirementSets.PhysicsOnly)
                .Build();

            return cubeTemplate;
        }*/
	}
}
