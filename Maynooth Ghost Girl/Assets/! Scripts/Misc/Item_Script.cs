using UnityEngine;

public class Item_Script : MonoBehaviour, IInteractable
{
    private Interaction_Manager interaction_Manager;
    private GameObject target_Position;
    private Rigidbody rb;
    private Collider collider;
    
    public bool being_Held;
    private bool can_Interact = true;
    private float interact_Cooldown = 0.1f;
    //private AudioSource audio_Source;
    
    void Start()
    {
        interaction_Manager = GameObject.FindWithTag("Interaction Manager").GetComponent<Interaction_Manager>();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        target_Position = GameObject.FindGameObjectWithTag("Hold Point");
        being_Held = false;
        //audio_Source = GameObject.FindGameObjectWithTag("Pick_Up_Audio").GetComponent<AudioSource>();
    }// end Start()

    void Update()
    {
        if (being_Held == false)
            return;

        // Cant be in Interact() since we turn off collider
        if (Input.GetKeyDown(KeyCode.E) && interaction_Manager.in_Dialogue == false)
        {
            // Stops problem of picking item back up as soon as it is dropped
            can_Interact = false;
            Invoke("Reset_Interact", interact_Cooldown);
            
            being_Held = false;
            rb.isKinematic = false;
            collider.enabled = true;
        }
        
        
        gameObject.transform.position = Vector3.Lerp(transform.position, target_Position.transform.position, Time.deltaTime * 50);
     
    }// end Update()
    
    public void Interact()
    {
        if (can_Interact == false || interaction_Manager.in_Dialogue == true)
            return;
        
        // Pick Up
        if (being_Held == false)
        {
            //audio_Source.Play();
            being_Held = true;
            rb.isKinematic = true;
            collider.enabled = false; // Turn off collider to avoid weird physics with player
        }
        
    }// end Interact()
    
    private void Reset_Interact()
    {
        can_Interact = true;
    }// end Reset_Interact()
    
}// end script