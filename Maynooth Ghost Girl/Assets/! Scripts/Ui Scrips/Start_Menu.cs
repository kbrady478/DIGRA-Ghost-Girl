using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Menu : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private GameObject start_Menu;
    [SerializeField] private GameObject controls_Menu;


    public void Start_Game()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit_Game()
    {
        Application.Quit();
    }


    public void Controls_Menu()
    {
        if (controls_Menu.activeInHierarchy == false)
        {
            controls_Menu.SetActive(true);
            start_Menu.SetActive(false);
        }
        else
        {
            controls_Menu.SetActive(false);
            start_Menu.SetActive(true);
        }
    }
    
    
}
