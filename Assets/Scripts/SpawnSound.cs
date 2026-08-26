using UnityEngine;
using FMODUnity;

public class SpawnSound : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter emitter;

    private CameraController cameraController = null;

    private void OnEnable()
    {
        cameraController = FindAnyObjectByType<CameraController>();
        cameraController.OnTargetAcquired += HandleTargetAcquired;
    }

    private void OnDisable()
    {
        cameraController.OnTargetAcquired -= HandleTargetAcquired;
    }

    private void HandleTargetAcquired()
    {
        emitter.Play();
    }
}
