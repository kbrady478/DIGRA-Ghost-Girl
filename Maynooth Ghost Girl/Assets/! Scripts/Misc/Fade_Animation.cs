using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade_Animation : MonoBehaviour
{
    [SerializeField] private Animator fade_Animator;

    public void Fade_Out()
    {
        fade_Animator.SetTrigger("Fade Out");
    }

    public void Load_Menu()
    {
        SceneManager.LoadScene(0);
    }
    

}
