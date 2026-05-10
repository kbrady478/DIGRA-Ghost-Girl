using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;


interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask interactable_Layers;
    [SerializeField] private Transform interactor_Source;
    
    [Header("Settings")]
    [SerializeField] private float interact_Range;
    
    [Header("Crosshair")]
    [SerializeField] private GameObject default_Crosshair;
    [SerializeField] private GameObject npc_Crosshair;
    [SerializeField] private GameObject item_Crosshair;
  
    
    
    private void Update()
    { 
       Ray ray = new Ray(interactor_Source.position, interactor_Source.forward);
       if (Physics.Raycast(ray, out RaycastHit hit, interact_Range, interactable_Layers))
       {
           
          if (hit.collider.gameObject.TryGetComponent(out IInteractable interact_Obj))
          {
              // Check NPC
              if (hit.collider.gameObject.layer == 7)
              {
                  if (npc_Crosshair.activeInHierarchy == false)
                  {
                      npc_Crosshair.SetActive(true);
                      default_Crosshair.SetActive(false);
                      item_Crosshair.SetActive(false);
                  }

                  if (Input.GetKeyDown(KeyCode.E))
                  {
                     interact_Obj.Interact();
                     print("NPC interact attempt");
                  }
               
              }

              // Check Item
              else if (hit.collider.gameObject.layer == 6)
              {

                  if (item_Crosshair.activeInHierarchy == false)
                  {
                      item_Crosshair.SetActive(true);
                      npc_Crosshair.SetActive(false);
                      default_Crosshair.SetActive(false);
                  }
                  
                  if (Input.GetKeyDown(KeyCode.E))
                  {
                      interact_Obj.Interact();
                      print("NPC interact attempt");
                  }
              }
              
          }// end Hit
           
       }// end Raycast 

       else
       {
           if (default_Crosshair.activeInHierarchy == false)
           {
               npc_Crosshair.SetActive(false);
               default_Crosshair.SetActive(true);
               item_Crosshair.SetActive(false);
           }
       }// end default
       
    }// end Update()
    
}// end script