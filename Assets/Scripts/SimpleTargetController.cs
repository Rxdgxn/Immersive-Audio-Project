using UnityEngine;
using System;
using Unity.VisualScripting;

public class SimpleTargetController : MonoBehaviour
{
    private CameraController cameraController = null;

    private Vector3 minBounds = new Vector3(-65f, 10f, -50f);
    private Vector3 maxBounds = new Vector3(38f, 30f, 20f);

    private float maxDistance = 60f; // from camera

    private void Start()
    {
        transform.position = GetRandomPosition();
    }

    private void OnEnable()
    {
        cameraController = FindAnyObjectByType<CameraController>();
        cameraController.OnTargetHit += HandleTargetHit;
    }

    private void OnDisable()
    {
        cameraController.OnTargetHit -= HandleTargetHit;
    }

    private void HandleTargetHit()
    {
        Vector3 pos = GetRandomPosition();

        while (Vector3.Distance(pos, cameraController.transform.position) >= maxDistance)
        {
            pos = GetRandomPosition();
        }

        transform.position = pos;
    }

    private Vector3 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
        float y = UnityEngine.Random.Range(minBounds.y, maxBounds.y);
        float z = UnityEngine.Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(x, y, z);
    }
}
