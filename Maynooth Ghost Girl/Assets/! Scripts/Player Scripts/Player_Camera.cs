using System;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    private Interaction_Manager interaction_Manager;
    [SerializeField] private Pause_Menu pause_Menu;
    
    
    [Header("Camera Settings")]
    public float sensitivity = 5F;
    private float x_Rotation = 0f;

    
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interaction_Manager = GameObject.FindWithTag("Interaction Manager").GetComponent<Interaction_Manager>();
    }

    
    private void Update()
    {
        if (interaction_Manager.in_Dialogue == true || pause_Menu.is_Paused == true)
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
