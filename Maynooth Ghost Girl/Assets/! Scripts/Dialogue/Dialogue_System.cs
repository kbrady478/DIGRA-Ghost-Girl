using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue_System : MonoBehaviour, IInteractable
{
    [Header("Dialogue Functionality")]
    [SerializeField] private Dialogue_Lines character_Script;
    [SerializeField] private string[] current_Dialogue;
    public int current_Dialogue_I = 0; // Dialogue ID
    private int current_Line_I = 0; // Individual line within Dialogue ID
    private bool currently_Typing = false; // For checking if dialogue is actively being written
    private bool in_Dialogue = false;
    
    [Header("UI Functionality")]
    [SerializeField] private GameObject dialogue_Box; // To hide/unhide whole dialogue box
    [SerializeField] private TextMeshProUGUI text_Component;
    [SerializeField] private float text_Speed;
    //[SerializeField] private AudioSource talking_Clip;

    [Header("Restrict Player Controls")]
    [SerializeField] private Player_Movement player_Controls;
    [SerializeField] private Player_Camera player_Camera;
    
    

    public void Start_Dialogue()
    {
        // Retrieve dialogue
        current_Dialogue = character_Script.Get_Dialogue(current_Dialogue_I);
        if (current_Dialogue == null)
        {
            print("ERROR: Dialogue not found");
            return;
        }
        
        
        // Start writing dialogue
        Toggle_Dialogue();
        StartCoroutine(Type_Line());
    }
    
    
    IEnumerator Type_Line()
    {
        text_Component.text = "";
        
        //talking_Clip.Play();
        currently_Typing = true;
        // Add each character in the line incrementally
        foreach (char c in current_Dialogue[current_Line_I].ToCharArray())
        {
            text_Component.text += c;
            yield return new WaitForSeconds(text_Speed);
        }
        currently_Typing = false;
        current_Line_I++;
        //talking_Clip.Pause();
        
    }

    
    // When interact is pressed mid dialogue
    public void Dialogue_Interacted()
    {
        // If typing, finish line
        if (currently_Typing == true)
        {
            StopAllCoroutines();
            //talking_Clip.Pause();
            text_Component.text = current_Dialogue[current_Line_I];
            currently_Typing = false;
            current_Line_I++;
        }

        // If we are past the amount of total lines in the dialogue, end it
        else if (current_Line_I >= current_Dialogue.Length)
        {
            // Set to reinteract line, even if there has been other dialogue
            if (current_Dialogue_I != 1)
                current_Dialogue_I = 1;
            
            in_Dialogue = false;
            Toggle_Dialogue();
        }
            
        // Else move onto next line
        else
        {
            StartCoroutine(Type_Line());
        }
    }
    
    // For toggling UI, bools, indexes
    private void Toggle_Dialogue()
    {
        dialogue_Box.SetActive(!dialogue_Box.activeSelf);
        text_Component.text = "";
        player_Controls.lock_Movement = in_Dialogue;
        player_Camera.lock_Camera = in_Dialogue;
        current_Line_I = 0;
    }


    public void Interact()
    {
        if (in_Dialogue == false)
        {
            in_Dialogue = true;
            Start_Dialogue();
        }
        else if (in_Dialogue == true)
        {
            Dialogue_Interacted();
        }
    }
    
}// end script