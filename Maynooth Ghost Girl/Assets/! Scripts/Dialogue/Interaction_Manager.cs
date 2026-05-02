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
    
    
    #region --- NPC Variables ---
    
    // True means it has to be played, false is either not played or has played
    
    [Header("Ghost Girl")] 
    [SerializeField] private GG_Dialogue_Lines gg_Dialogue_Lines;
    public bool gg_First_Interaction = true;
    
    
    [Header("Student 1")]
    [SerializeField] private St1_Dialogue_Lines st1_Dialogue_Lines;
    public bool st1_Pleased = false;
    public bool st1_First_Interaction = true;
    
    
    [Header("Student 2")]
    [SerializeField] private St2_Dialogue_Lines st2_Dialogue_Lines;
    public bool st2_Pleased = false;
    public bool st2_First_Interaction = true;
    
    
    [Header("Student 3")]
    [SerializeField] private St3_Dialogue_Lines st3_Dialogue_Lines;
    public bool st3_Pleased = false;
    public bool st3_First_Interaction = true;
    
    #endregion
    
    
    #region --- Dialogue System Interactions ---

    public void Start_Interaction(string[] recieved_Dialogue)
    {
        
        dialogue_System.Start_Dialogue(recieved_Dialogue);
    }
    
    public void Progress_Interaction()
    {
        dialogue_System.Dialogue_Interacted();
    }
    
    #endregion


    #region --- NPC States ---
    
    // State machine to determine what dialogue to get next
    
    public string Ghost_Girl_State()
    {
        if (gg_First_Interaction == true)
        {
            gg_First_Interaction = false;
            return "gg_First_Interaction";
        }
        
        else 
            return "gg_Reinteract";
        
    }// end Ghost_Girl_State


    public string St1_State()
    {
        if (st1_First_Interaction == true)
        {
            st1_First_Interaction = false;
            return "st1_First_Interaction";
        }
        
        else 
            return "st1_Reinteract";
        
    }// end St1_State
    
    
    public string St2_State()
    {
        if (st2_First_Interaction == true)
        {
            st2_First_Interaction = false;
            return "st2_First_Interaction";
        }
        
        else 
            return "st2_Reinteract";
        
    }// end St2_State
    
    
    public string St3_State()
    {
        if (st3_First_Interaction == true)
        {
            st3_First_Interaction = false;
            return "st3_First_Interaction";
        }
        
        else 
            return "st3_Reinteract";
        
    }// end St3_State
    
    #endregion
    
}
