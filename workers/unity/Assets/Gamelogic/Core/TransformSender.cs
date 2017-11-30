using Assets.Gamelogic.Utils;
using Improbable;
using Improbable.Core;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

[WorkerType(WorkerPlatform.UnityWorker)]
public class TransformSender : MonoBehaviour
{

    [Require] private Position.Writer PositionWriter;
    [Require] private Rotation.Writer RotationWriter;

    void Update()
    {
        var pos = transform.position;

		if (PositionNeedsUpdate(new Coordinates(pos.x,pos.y,pos.z))){
        var positionUpdate = new Position.Update().SetCoords(new Coordinates(pos.x, pos.y, pos.z));
        PositionWriter.Send(positionUpdate);
		}
		if(RotationNeedsUpdate(transform.rotation)){
        var rotationUpdate = new Rotation.Update().SetRotation(MathUtils.ToNativeQuaternion(transform.rotation));
        RotationWriter.Send(rotationUpdate);
		}
	}
	private bool PositionNeedsUpdate (Coordinates newCoords){
		return !MathUtils.ApproximatelyEqual (newCoords, PositionWriter.Data.coords);
	}
			private bool RotationNeedsUpdate (UnityEngine.Quaternion newRotation){
				return !MathUtils.ApproximatelyEqual (newRotation, RotationWriter.Data.rotation.ToUnityQuaternion());
	}
}
