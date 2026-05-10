using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Menu : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject pause_Menu;
    [SerializeField] private GameObject cursor_Objects;
    [SerializeField] private GameObject counter_Object;
    
    
    public bool is_Paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (is_Paused == false)
                Pause_Game();
            else
                Unpause_Game();
            
        }
    }

    
    private void Pause_Game()
    {
        is_Paused = true;
        Time.timeScale = 0;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        
        pause_Menu.SetActive(true);
        cursor_Objects.SetActive(false);
        counter_Object.SetActive(false);
    }

    public void Unpause_Game()
    {
        is_Paused = false;
        Time.timeScale = 1;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
          pause_Menu.SetActive(false);
          cursor_Objects.SetActive(true);
          counter_Object.SetActive(true);
    }
    
    
    public void Quit_Game()
    {
        SceneManager.LoadScene(0);
    }
    
}
