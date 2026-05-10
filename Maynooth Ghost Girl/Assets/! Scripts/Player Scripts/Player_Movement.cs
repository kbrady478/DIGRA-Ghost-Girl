using System;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    [Header("Object Refs")]
    [SerializeField] Camera player_Camera;
    [SerializeField] private CharacterController controller;
    private Interaction_Manager interaction_Manager;
    
    [Header("Settigns")] 
    [SerializeField] private float move_Speed = 5f;
    [SerializeField] private float jump_Force = 5f;
    [SerializeField] private float gravity = -10f;
    
    private Vector3 velocity = Vector3.zero;
    


    private void Start()
    {
        interaction_Manager = GameObject.FindWithTag("Interaction Manager").GetComponent<Interaction_Manager>();
    }

    private void Update()
    {
        if (interaction_Manager.in_Dialogue == true)
            return;
        
        // Calculate directions
        Vector3 movement = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        controller.Move(movement * move_Speed * Time.deltaTime);

        // Check if grounded
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // If space pressed, jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jump_Force * -2f * gravity);
        }
        
        velocity.y += gravity * Time.deltaTime;
        
        // Apply movement
        controller.Move(velocity * Time.deltaTime);
    }
}