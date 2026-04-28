using System;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    
    [Header("Camera Settings")]
    public float sensitivity = 5F;
    private float x_Rotation = 0f;
    
    [Header("Restrictions")]
    public bool lock_Camera = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    private void Update()
    {
        if (lock_Camera == true)
            return;
        
        // Get directions
        float mouse_X = Input.GetAxis("Mouse X") * sensitivity;
        float mouse_Y = Input.GetAxis("Mouse Y") * sensitivity;
        
        // Limit rotation to stop flipping
        x_Rotation -= mouse_Y;
        x_Rotation = Mathf.Clamp(x_Rotation, -90f, 90f);
        
        // Calculate and apply directions
        transform.localRotation = Quaternion.Euler(x_Rotation, 0f, 0f);
        transform.parent.Rotate(Vector3.up * mouse_X);
    }
}
