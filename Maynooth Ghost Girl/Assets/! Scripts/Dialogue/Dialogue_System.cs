using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Dialogue_System : MonoBehaviour
{
    private Interaction_Manager interaction_Manager;
    
    [Header("Dialogue Functionality")]
    public string[] current_Dialogue;
    private int current_Line_I = 0; // Individual line within Dialogue ID
    private bool currently_Typing = false; // For checking if dialogue is actively being written
    
    [Header("UI Functionality")]
    [SerializeField] private GameObject dialogue_Box; // To hide/unhide whole dialogue box
    [SerializeField] private TextMeshProUGUI text_Component;
    [SerializeField] private float text_Speed;
    [SerializeField] private AudioSource current_Audio;
    
    
    private void Start()
    {
        interaction_Manager = GameObject.FindWithTag("Interaction Manager").GetComponent<Interaction_Manager>();
    }


    public void Start_Dialogue(string[] recieved_Dialogue, AudioSource talking_Clip)
    {
        current_Audio = talking_Clip;
        current_Dialogue = recieved_Dialogue;
        
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
        
        current_Audio.Play();
        currently_Typing = true;
        // Add each character in the line incrementally
        foreach (char c in current_Dialogue[current_Line_I].ToCharArray())
        {
            text_Component.text += c;
            yield return new WaitForSeconds(text_Speed);
        }
        currently_Typing = false;
        current_Line_I++;
        current_Audio.Pause();
        
    }

    
    // When interact is pressed mid dialogue
    public void Dialogue_Interacted()
    {
        // If typing, finish line
        if (currently_Typing == true)
        {
            StopAllCoroutines();
            current_Audio.Pause();
            text_Component.text = current_Dialogue[current_Line_I];
            currently_Typing = false;
            current_Line_I++;
        }

        // If we are past the amount of total lines in the dialogue, end it
        else if (current_Line_I >= current_Dialogue.Length)
        {
            interaction_Manager.in_Dialogue = false;
            interaction_Manager.Post_Dialogue_Event();
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
        current_Line_I = 0;
    }
    
}// end script