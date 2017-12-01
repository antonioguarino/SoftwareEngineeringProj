﻿using UnityEngine;

namespace Assets.Gamelogic.Core
{
    public static class SimulationSettings
	{			//positionArray = new [] { new Vector3(-50f,0f,-50f),new Vector3(-50f,0f,50f),new Vector3(50f,0f,50f), new Vector3(50f,0f,-50f) };
		
        public static readonly float PlayerSpawnHeight = 0;
        public static readonly float PlayerAcceleration = 15;
		public static readonly float[] PlayerXPos = {-50f,-50f,50f,50f };
        public static readonly string PlayerPrefabName = "Player";
        public static readonly string PlayerCreatorPrefabName = "PlayerCreator";
		public static readonly string AlienPrefabName = "Alien";
        //public static readonly string CubePrefabName = "Cube";
		public static readonly float ClientConnectionTimeoutSecs = 7;
		public static readonly int TotalAliens = 50;


        //Camera
        public static readonly Quaternion InitialThirdPersonCameraRotation = Quaternion.Euler(30, 0, 0);
        public static readonly float InitialThirdPersonCameraDistance = 15;
        public static readonly string MouseScrollWheel = "Mouse ScrollWheel";
        public static readonly float ThirdPersonZoomSensitivity = 3f;
        public static readonly float ThirdPersonCameraMinDistance = 4f;
        public static readonly float ThirdPersonCameraMaxDistance = 20f;
        public static readonly int RotateCameraMouseButton = 1;
        public static readonly float ThirdPersonCameraSensitivity = 2f;
        public static readonly float ThirdPersonCameraMinPitch = 5f;
        public static readonly float ThirdPersonCameraMaxPitch = 70f;


        public static readonly float HeartbeatCheckIntervalSecs = 3;
        public static readonly uint TotalHeartbeatsBeforeTimeout = 3;
        public static readonly float HeartbeatSendingIntervalSecs = 3;

        public static readonly int TargetClientFramerate = 60;
        public static readonly int TargetServerFramerate = 60;
        public static readonly int FixedFramerate = 20;

        public static readonly float PlayerCreatorQueryRetrySecs = 4;
        public static readonly float PlayerEntityCreationRetrySecs = 4;

        public static readonly string DefaultSnapshotPath = Application.dataPath + "/../../../snapshots/default.snapshot";
    }
}
