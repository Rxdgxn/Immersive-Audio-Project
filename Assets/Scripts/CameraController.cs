using UnityEngine;
using System;
using FMODUnity;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private float sensitivity = 2f;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    [SerializeField] private LayerMask layer;
    private float rayMaxDistance = 60f;

    private bool targetAcquired = false;
    private bool lastTargetState = false;

    public event Action OnTargetHit;
    public event Action OnTargetAcquired;

    [SerializeField] private StudioEventEmitter fireEmitter;

    private Sonification sonification;
    
    private void HandleMouseLook()
    {        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        horizontalRotation += mouseX;

        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }

    private void HandleAim()
    {
        targetAcquired = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit _hit, rayMaxDistance, layer);

        if (targetAcquired && !lastTargetState)
        {
            OnTargetAcquired?.Invoke();
        }
    }

    private void HandleSonification()
    {
        Vector3 local = transform.InverseTransformPoint(sonification.transform.position);

        float azimuthError = Mathf.Atan2(local.x, local.z) * Mathf.Rad2Deg;         // -180..180
        float horizontalDist = Mathf.Sqrt(local.x * local.x + local.z * local.z);
        float elevationError = Mathf.Atan2(local.y, horizontalDist) * Mathf.Rad2Deg; // -90..90

        sonification.UpdateParams(azimuthError, elevationError);
    }
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        sonification = FindAnyObjectByType<Sonification>();
    }

    void Update()
    {
        HandleMouseLook();
        HandleAim();
        HandleSonification();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            fireEmitter.Play();

            if (targetAcquired)
            {
                OnTargetHit?.Invoke();
            }
        }

        lastTargetState = targetAcquired;
    }
}
