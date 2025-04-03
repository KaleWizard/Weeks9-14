using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<GameObject> destructibleObjects;

    CinemachineVirtualCamera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyNextObject();
        }
    }

    void DestroyNextObject()
    {
        GameObject target = destructibleObjects[Random.Range(0, destructibleObjects.Count)];
        destructibleObjects.Remove(target);
        camera.Follow = target.transform;
        target.GetComponent<DestructionScript>().Activate();
    }
}
