using UnityEngine;
using System;
using FMODUnity;
using System.Collections;

public class SimpleTargetController : MonoBehaviour
{
    private CameraController cameraController = null;
    [SerializeField] private StudioEventEmitter acquireEmitter;

    private SpawnSound spawnSound;
    private Sonification sonification;

    private Vector3 minBounds = new Vector3(-65f, 10f, -50f);
    private Vector3 maxBounds = new Vector3(38f, 30f, 20f);

    private float maxDistance = 40f; // from camera

    private void Start()
    {
        transform.position = GetRandomPosition();
        spawnSound.PlaySound();
        sonification.Play();
    }

    private void OnEnable()
    {
        cameraController = FindAnyObjectByType<CameraController>();
        cameraController.OnTargetHit += HandleTargetHit;
        cameraController.OnTargetAcquired += HandleTargetAcquired;

        spawnSound = FindAnyObjectByType<SpawnSound>();
        sonification = FindAnyObjectByType<Sonification>();
    }

    private void OnDisable()
    {
        cameraController.OnTargetHit -= HandleTargetHit;
        cameraController.OnTargetAcquired -= HandleTargetAcquired;
    }

    private void HandleTargetHit()
    {
        sonification.Stop();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        transform.position = new Vector3(-99f, -99f, -99f);

        Vector3 pos = GetRandomPosition();
        while (Vector3.Distance(pos, cameraController.transform.position) >= maxDistance)
        {
            pos = GetRandomPosition();
        }

        yield return new WaitForSeconds(3f);

        transform.position = pos;
        spawnSound.PlaySound();
        sonification.Play();
    }

    private void HandleTargetAcquired()
    {
        acquireEmitter.Play();
    }

    private Vector3 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
        float y = UnityEngine.Random.Range(minBounds.y, maxBounds.y);
        float z = UnityEngine.Random.Range(minBounds.z, maxBounds.z);

        return new Vector3(x, y, z);
    }
}
