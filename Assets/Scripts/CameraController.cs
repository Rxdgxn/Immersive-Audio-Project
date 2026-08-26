using UnityEngine;
using System;
using Unity.VisualScripting;

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
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleAim();

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButton((int)MouseButton.Left) && targetAcquired)
        {
            OnTargetHit?.Invoke();
        }

        lastTargetState = targetAcquired;
    }
}
