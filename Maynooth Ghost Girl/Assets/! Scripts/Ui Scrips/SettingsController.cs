using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SettingsController : MonoBehaviour
{
    [Header("Assign Main & UI audio sources:")]

    [SerializeField] private AudioSource MusicAudio;
    [SerializeField] private AudioSource SFXAudio;
    [Space(10)]
    [Header("Sliders:")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Pause Menu:")]
    [SerializeField] private GameObject pauseMenuParent;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    public bool isGamePaused;

    [Header("Load Settings:")]
    [SerializeField] private string mainMenu = "MainMenu";
    [SerializeField] private string mainScene = "Beta_Scene";
    public string Url;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Adjust music volume
        musicVolumeSlider.value = MusicAudio.volume;
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        sfxVolumeSlider.value = SFXAudio.volume;
        sfxVolumeSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isGamePaused)
            {
                ContinueGame();
                Debug.Log("Game should NOT be paused rn");
            }
            else
            {
                PauseGame();
                Debug.Log("Game should be paused rn");
            }
        }

    }
    public void PauseGame()
    {
        pauseMenuParent.SetActive(true);
        settingsMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;

    }
    public void ContinueGame()
    {
        pauseMenuParent.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }
    public void EnterSettingsGame()
    {
        pauseMenu.gameObject.SetActive(false);
        settingsMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;

    }

    public void SetMusicVolume(float value)
    {
        MusicAudio.volume = value;
    }

    public void SetSfxVolume(float value)
    {
        SFXAudio.volume = value;
    }
    public void MainMenuButton()
    {
        ContinueGame();
        SceneManager.LoadScene(mainMenu);
    }
    public void MainSceneButton()
    {
        SceneManager.LoadScene(mainScene);
    }
    public void OpenActionaidLink()
    {
        Application.OpenURL(Url);
    }
    public void doExitGame()
    {
        Application.Quit();
    }
}
