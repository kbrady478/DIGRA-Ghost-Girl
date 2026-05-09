using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuController : MonoBehaviour
{
    [Header("FadeScreen Settings:")]

    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float fadeTime = 1.0f;

    [Header("Pause Settings:")]
    //[SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Slider volumeSlider;
    public bool isGamePaused;
    public string Url;

    [SerializeField] private string mainScene = "Beta_Scene";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Update is called once per frame
  
  
    public void ContinueGame()
    {
        settingsMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void EnterSettingsGame()
    {
        settingsMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;

    }
    
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void MainSceneButton()
    {
        if (blackScreen != null)
        {
            StartFadeBlack();
        }
        else
        {

            SceneManager.LoadScene(mainScene);
        }
    }
    public void OpenActionaidLink()
    {
        Application.OpenURL(Url);
    }

    private void StartFadeBlack()
    {
        StartCoroutine(FadeToBlack());
    }
    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            float newAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);

            blackScreen.alpha = newAlpha;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blackScreen.alpha = 1f;

        SceneManager.LoadScene(mainScene);
    }
}
