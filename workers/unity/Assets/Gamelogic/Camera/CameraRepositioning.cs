//using Assets.Gamelogic.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Improbable.Core;
//using Improbable.Unity.Visualizer;

public class CameraRepositioning : MonoBehaviour {
    
    private float smoothing = 5f;
    private Transform camera;
    Vector3 offset;
    
    // Use this for initialization
    void OnEnable()
    {
        camera = Camera.main.transform;
        //target = GameObject.FindGameObjectWithTag("Player").transform;

        offset = camera.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetCamPos = transform.position + offset;
        camera.position = Vector3.Lerp(camera.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
