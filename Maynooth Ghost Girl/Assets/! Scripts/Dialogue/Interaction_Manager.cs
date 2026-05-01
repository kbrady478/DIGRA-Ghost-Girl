using UnityEngine;

interface IDialogue_Interaction
{
    void Start_Interaction();
    void Progress_Interaction();
}
public class Interaction_Manager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Dialogue_System dialogue_System;
    // Can likely remove these two
    [SerializeField] private Player_Movement player_Movement;
    [SerializeField] private Player_Camera player_Camera;

    [Header("Restrictor")] 
    public bool in_Dialogue;

    [Header("Functionality")] 
    public string[] dialogue_To_Send;
    
    [Header("Ghost Girl")] 
    [SerializeField] private Dialogue_Lines gg_Dialogue_Lines;
    public bool gg_Reinteract;
    public bool gg_First_Interaction = false;
    
    
    #region --- Dialogue System Interactions ---

    public void Start_Interaction(string[] recieved_Dialogue)
    {
        dialogue_System.Start_Dialogue(dialogue_To_Send);
    }
    
    public void Progress_Interaction()
    {
        dialogue_System.Dialogue_Interacted();
    }
    
    #endregion
    
    
}
