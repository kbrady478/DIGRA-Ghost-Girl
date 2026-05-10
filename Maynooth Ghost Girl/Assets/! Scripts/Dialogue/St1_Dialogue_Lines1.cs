using System;
using UnityEngine;

public class St1_Dialogue_Lines : MonoBehaviour, IInteractable, IDialogue_Interaction
{
    [Header("References")] 
    [SerializeField] private Interaction_Manager interaction_Manager;
    [SerializeField] private Dialogue_System dialogue_System;
    [SerializeField] private AudioSource st1_Audio;
    
    [Header("Dialogue Text - Elements are different lines")] // Different types of dialogue, arrays contain individual lines
    [SerializeField] private string[] first_Dialogue;
    [SerializeField] private string[] correct_Item;
    [SerializeField] private string[] wrong_Item;
    [SerializeField] private string[] normal_Reinteract_Dialogue;
    [SerializeField] private string[] pleased_Reinteract_Dialogue;
    [SerializeField] private string[] pleased_Item_Held;

    private string dialogue_To_Get;
    private string[] dialogue_To_Send;
    
    
    #region --- Interactors ---
    
    // Triggered by interact key within range of NPC collider
    public void Interact()
    {
        if (interaction_Manager.in_Dialogue == false)
        {
            Start_Interaction();
        }
        else if (interaction_Manager.in_Dialogue == true)
        {
            Progress_Interaction();
        }
    }

    // Used to start interaction in manager
    // Check state in Interaction Manager, return here to match ID to string, send string to Interaction Manager
    public void Start_Interaction()
    {
        dialogue_To_Get = interaction_Manager.St1_State();
        Get_Dialogue(dialogue_To_Get);
        interaction_Manager.Start_Interaction(dialogue_To_Send, st1_Audio);
        interaction_Manager.in_Dialogue = true;
        interaction_Manager.StartCoroutine("St1_Facial_Interaction");
    }

    // Used to continue interaction through manager on repeat interact presses
    public void Progress_Interaction()
    {
        interaction_Manager.Progress_Interaction();
    }
    
    #endregion
    
    // List of all dialogue options with ID, specifics determined in Interaction Manager
    // Lines for each string are written in the inspector
    private void Get_Dialogue(string dialogue_ID)
    {

        switch (dialogue_ID)
        {

            case "st1_First_Interaction":
                dialogue_To_Send = first_Dialogue;
                break;
            
            case "st1_Correct_Item":
                dialogue_To_Send = correct_Item;
                break;
            
            case "st1_Wrong_Item":
                dialogue_To_Send = wrong_Item;
                break;
            
            case "st1_Normal_Reinteract":
                dialogue_To_Send = normal_Reinteract_Dialogue;
                break;

            case "st1_Pleased_Reinteract":
                dialogue_To_Send = pleased_Reinteract_Dialogue;
                break;
            
            case "st1_Pleased_Item_Held":
                dialogue_To_Send = pleased_Item_Held;
                break;
            
            default:
                dialogue_To_Send = null;
                print("ERROR: Dialogue ID has no match");
                break;
            
        }// end switch()
        
        dialogue_System.current_Dialogue = dialogue_To_Send;
        
    }// end Get_Dialogue()
    
}  // end script