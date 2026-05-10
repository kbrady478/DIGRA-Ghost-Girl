using System;
using System.Collections;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

interface IDialogue_Interaction
{
    void Start_Interaction();
    void Progress_Interaction();
}
public class Interaction_Manager : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Dialogue_System dialogue_System;
    
    [Header("Restrictors")] 
    public bool in_Dialogue;
    
    [Header("Fade Animation")]
    [SerializeField] private Fade_Animation fade_Animation;
    
    [Header("Item Management - Do not change manually")] 
    public bool holding_Item;
    public GameObject held_Item;
    
    [Header("Objective Counter")]
    [SerializeField] private TextMeshProUGUI objective_counter;
    private int obj_I = 0;
    
    
    #region --- NPC Variables ---
    
    // True means it has to be played, false is either not played or has played
    [Header("-- NPC Stuff --")] 
    [SerializeField] private float face_Anim_Timer;
    
    
    [Header("- Ghost Girl -")] 
    [SerializeField] private GG_Dialogue_Lines gg_Dialogue_Lines;
    [SerializeField] private Animator gg_Animator;
    public bool gg_First_Interaction = true;
    private bool gg_Final_Interaction = false;
    
    [Space(5)]
    [SerializeField] private GameObject stew_Cheese;
    private bool gg_Stew_Cheese_Interaction = false; // For spawning cheese after interaction
    
    [Space(5)]
    [SerializeField] private Renderer gg_Face_Mat;
    [SerializeField] private Texture gg_Default_Face;
    [SerializeField] private Texture[] gg_Talking_Textures;
    
    
    [Header("- Student 1 -")]
    [SerializeField] private St1_Dialogue_Lines st1_Dialogue_Lines;
    [SerializeField] private Animator st1_Animator;
    public bool st1_Pleased = false;
    public bool st1_First_Interaction = true;
    
    [Space(5)]
    [SerializeField] private Renderer st1_Face_Mat;
    [SerializeField] private Texture st1_Default_Face;
    [SerializeField] private Texture[] st1_Talking_Textures;
    
    
    [Header("- Student 2 -")]
    [SerializeField] private St2_Dialogue_Lines st2_Dialogue_Lines;
    [SerializeField] private Animator st2_Animator;
    public bool st2_Pleased = false;
    public bool st2_First_Interaction = true;
    
    [Space(5)]
    [SerializeField] private Renderer st2_Face_Mat;
    [SerializeField] private Texture st2_Default_Face;
    [SerializeField] private Texture[] st2_Talking_Textures;
    
    [Header("- Student 3 -")]
    [SerializeField] private St3_Dialogue_Lines st3_Dialogue_Lines;
    [SerializeField] private Animator st3_Animator;
    public bool st3_Pleased = false;
    public bool st3_First_Interaction = true;
    
    [Space(5)]
    [SerializeField] private Renderer st3_Face_Mat;
    [SerializeField] private Texture st3_Default_Face;
    [SerializeField] private Texture[] st3_Talking_Textures;
    
    #endregion
    
    
    #region --- Dialogue System Interactions ---

    // Take input from different NPC scripts, pass onto Dialogue System
    
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
    
    // State machines for each character to determine what dialogue to get next and trigger animations
    
    public string Ghost_Girl_State()
    {
        gg_Animator.SetBool("isTalking", true);
        
        if (gg_First_Interaction == true)
        {
            gg_First_Interaction = false;
            return "gg_First_Interaction";
        }

        // Stew cheese interaction
        if (st2_First_Interaction == false && stew_Cheese == true)
        {
            gg_Stew_Cheese_Interaction = true;
            return "gg_Stew_Cheese";
        }
        
        if (st1_Pleased && st2_Pleased && st3_Pleased)
        {
            gg_Animator.SetTrigger("Yippee_Trigger");
            gg_Final_Interaction = true;
            return "gg_End_Dialogue";
        }
            
        if (holding_Item == true)
        {
            return "gg_Given_Item";
        }
        
        else 
            return "gg_Reinteract";
        
    }// end Ghost_Girl_State


    public string St1_State()
    {
        st1_Animator.SetBool("isTalking", true);
        
        // First time meeting
        if (st1_First_Interaction == true)
        {
            st1_First_Interaction = false;
            return "st1_First_Interaction";
        }
        
        
        // Holding item dialogue
        if (holding_Item == true)
        {
            if (st1_Pleased)
            {
                return "st1_Pleased_Item_Held";
            }
            
            if (held_Item.tag == "St1 Object")
            {
                Increase_Counter();
                st1_Animator.SetTrigger("Yippee_Trigger");
                st1_Pleased = true;
                Item_Given();
                return "st1_Correct_Item";
            }

            else
            {
                return "st1_Wrong_Item";
            }
        }
            
        // Reinteract dialogue
        else
        {
            if (st1_Pleased == true)
                return "st1_Pleased_Reinteract";

            else
                return "st1_Normal_Reinteract";
        }
        
    }// end St1_State
    
    
    public string St2_State()
    {
        st2_Animator.SetBool("isTalking", true);
        
        if (st2_First_Interaction == true)
        {
            st2_First_Interaction = false;
            return "st2_First_Interaction";
        }
        
        
        // Holding item dialogue
        if (holding_Item == true)
        {
            if (st2_Pleased)
            {
                return "st2_Pleased_Item_Held";
            }
            
            if (held_Item.tag == "St2 Object")
            {
                Increase_Counter();
                st2_Animator.SetTrigger("Yippee_Trigger");
                st2_Pleased = true;
                Item_Given();
                return "st2_Correct_Item";
            }

            else
            {
                return "st2_Wrong_Item";
            }
        }
            
        // Reinteract dialogue
        else
        {
            if (st2_Pleased == true)
                return "st2_Pleased_Reinteract";

            else
                return "st2_Normal_Reinteract";
        }
        
    }// end St2_State
    
    
    public string St3_State()
    {
        st3_Animator.SetBool("isTalking", true);
        
        if (st3_First_Interaction == true)
        {
            st3_First_Interaction = false;
            return "st3_First_Interaction";
        }
        
        
        // Holding item dialogue
        if (holding_Item == true)
        {
            if (st3_Pleased)
            {
                return "st3_Pleased_Item_Held";
            }
            
            if (held_Item.tag == "St3 Object")
            {
                Increase_Counter();
                st3_Animator.SetTrigger("Yippee_Trigger");
                st3_Pleased = true;
                Item_Given();
                return "st3_Correct_Item";
            }

            else
            {
                return "st3_Wrong_Item";
            }
        }
            
        // Reinteract dialogue
        else
        {
            if (st3_Pleased == true)
                return "st3_Pleased_Reinteract";

            else
                return "st3_Normal_Reinteract";
        }
        
    }// end St3_State
    
    #endregion


    #region --- Facial Animations ---
    
    public IEnumerator GG_Facial_Interaction()
    {
        Material face_Mat = gg_Face_Mat.materials[1];
        int i = 0;
        
        while (in_Dialogue == true)
        {

            face_Mat.SetTexture("_BaseMap", gg_Talking_Textures[i]);
            
            i++;

            if (i > gg_Talking_Textures.Length - 1)
                i = 0;

            yield return new WaitForSeconds(face_Anim_Timer);
        }
        
        GG_Face_Reset();
    }

    private void GG_Face_Reset()
    {
        Material face_Mat = gg_Face_Mat.materials[1];
        face_Mat.SetTexture("_BaseMap", gg_Default_Face);
        gg_Face_Mat.materials[1] = face_Mat;
    }

    
    public IEnumerator St1_Facial_Interaction()
    {
        Material face_Mat = st1_Face_Mat.materials[1];
        int i = 0;
        
        while (in_Dialogue == true)
        {
            

            face_Mat.SetTexture("_BaseMap", st1_Talking_Textures[i]);
            
            i++;

            if (i > st1_Talking_Textures.Length - 1)
                i = 0;

            yield return new WaitForSeconds(face_Anim_Timer);
        }
        
        St1_Face_Reset();
    }

    private void St1_Face_Reset()
    {
        Material face_Mat = st1_Face_Mat.materials[1];
        face_Mat.SetTexture("_BaseMap", st1_Default_Face);
        st1_Face_Mat.materials[1] = face_Mat;
    }
    
    
    public IEnumerator St2_Facial_Interaction()
    {
        Material face_Mat = st2_Face_Mat.materials[2];
        int i = 0;
        
        while (in_Dialogue == true)
        {
            

            face_Mat.SetTexture("_BaseMap", st2_Talking_Textures[i]);
            
            i++;

            if (i > st2_Talking_Textures.Length - 1)
                i = 0;

            yield return new WaitForSeconds(face_Anim_Timer);
        }
        
        St2_Face_Reset();
    }

    private void St2_Face_Reset()
    {
        Material face_Mat = st2_Face_Mat.materials[2];
        face_Mat.SetTexture("_BaseMap", st2_Default_Face);
        st2_Face_Mat.materials[1] = face_Mat;
    }
    
    
    public IEnumerator St3_Facial_Interaction()
    {
        Material face_Mat = st3_Face_Mat.materials[1];
        int i = 0;
        
        while (in_Dialogue == true)
        {
            

            face_Mat.SetTexture("_BaseMap", st3_Talking_Textures[i]);
            
            i++;

            if (i > st3_Talking_Textures.Length - 1)
                i = 0;

            yield return new WaitForSeconds(face_Anim_Timer);
        }
        
        St3_Face_Reset();
    }

    private void St3_Face_Reset()
    {
        Material face_Mat = st3_Face_Mat.materials[1];
        face_Mat.SetTexture("_BaseMap", st3_Default_Face);
        st3_Face_Mat.materials[1] = face_Mat;
    }
    
    #endregion
    
    
    #region --- Other ---
    
    private void Item_Given()
    {
        Destroy(held_Item);
        held_Item = null;
        holding_Item = false;
        
    }

   
    public void Post_Dialogue_Event()
    {
        Stop_Animations();
        
        if (gg_Final_Interaction == true)
            fade_Animation.Fade_Out();

        if (gg_Stew_Cheese_Interaction == true)
        {
            stew_Cheese.SetActive(true);
            gg_Stew_Cheese_Interaction = false;
        }
    }

    private void Stop_Animations()
    {
        gg_Animator.SetBool("isTalking", false);
        st1_Animator.SetBool("isTalking", false);
        st2_Animator.SetBool("isTalking", false);
        st3_Animator.SetBool("isTalking", false);
    }

    private void Increase_Counter()
    {
        obj_I++;
        objective_counter.text = obj_I.ToString();
    }
    
    #endregion
    
}// end Interaction_Manager
