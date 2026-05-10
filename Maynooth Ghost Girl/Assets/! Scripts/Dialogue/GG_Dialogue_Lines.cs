using System;
using UnityEngine;

public class GG_Dialogue_Lines : MonoBehaviour, IInteractable, IDialogue_Interaction
{
    [Header("References")] 
    [SerializeField] private Interaction_Manager interaction_Manager;
    [SerializeField] private Dialogue_System dialogue_System;
    
    
    [Header("Dialogue Text - Elements are different lines")] // Different types of dialogue, arrays contain individual lines
    [SerializeField] private string[] first_Dialogue;
    [SerializeField] private string[] reinteract_Dialogue;
    [SerializeField] private string[] end_Dialogue;
    [SerializeField] private string[] given_Item;
    [SerializeField] private string[] stew_Cheese_Interaction;

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
    // Check state in Interaction Manager, return here to match ID to string, send to Interaction Manager
    public void Start_Interaction()
    {
        dialogue_To_Get = interaction_Manager.Ghost_Girl_State();
        Get_Dialogue(dialogue_To_Get);
        interaction_Manager.Start_Interaction(dialogue_To_Send);
        interaction_Manager.in_Dialogue = true;
        interaction_Manager.StartCoroutine("GG_Facial_Interaction");
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

            case "gg_First_Interaction":
                dialogue_To_Send = first_Dialogue;
                break;
            
            case "gg_Given_Item":
                dialogue_To_Send = given_Item;
                break;
            
            case "gg_End_Dialogue":
                dialogue_To_Send = end_Dialogue;
                break;
            
            case "gg_Reinteract":
                dialogue_To_Send = reinteract_Dialogue;
                break;
            
            case "gg_Stew_Cheese":
                dialogue_To_Send = stew_Cheese_Interaction;
                break;

            default:
                dialogue_To_Send = null;
                print("ERROR: Dialogue ID has no match");
                break;
            
        }// end switch()
        
        dialogue_System.current_Dialogue = dialogue_To_Send;
        
    }// end Get_Dialogue()
    
}  // end script