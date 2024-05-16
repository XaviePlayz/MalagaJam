using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class AudioVolumeController : MonoBehaviour
{
    #region Singleton

    private static AudioVolumeController _instance;
    public static AudioVolumeController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioVolumeController>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(AudioVolumeController).Name;
                    _instance = obj.AddComponent<AudioVolumeController>();
                }
            }
            return _instance;
        }
    }

    #endregion

    [Header("Volume")]
    public Slider volumeSlider;
    private const string VolumeKey = "Volume";

    [Header("Scenes")]
    public GameObject pauseMenu;
    public GameObject mainMenu, options;
    float gameVolume;

    [Header("Buttons")]
    public GameObject startButton;
    public GameObject tutorialButton;
    public GameObject optionsButton;
    public GameObject volumeSliderButton;
    public GameObject saveOptionsButton;

    private bool controllerIsAlreadyConnected = false;


    private void Start()
    {
        // Load Game
        options.SetActive(false);

        // Load the saved volume value
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 0.05f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Attach a listener to the slider's OnValueChanged event
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void Update()
    {
        if (ControllerCursor.Instance.isControllerConnected && !controllerIsAlreadyConnected && !options.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(startButton);
            controllerIsAlreadyConnected = true;
        }
        if (!ControllerCursor.Instance.isControllerConnected && !controllerIsAlreadyConnected && !options.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            controllerIsAlreadyConnected = false;
        }

        if (ControllerCursor.Instance.isControllerConnected && !controllerIsAlreadyConnected && options.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(saveOptionsButton);
            controllerIsAlreadyConnected = true;
        }
        if (!ControllerCursor.Instance.isControllerConnected && options.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(null);
            controllerIsAlreadyConnected = false;
        }
    }

    public void OpenSettings()
    {
        if (ControllerCursor.Instance.isControllerConnected)
        {
            EventSystem.current.SetSelectedGameObject(saveOptionsButton);
        }
        pauseMenu.SetActive(false);
        options.SetActive(true);
        SetVolume(gameVolume);

        // Load the saved volume value
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void ConfirmSettings()
    {
        if (ControllerCursor.Instance.isControllerConnected)
        {
            EventSystem.current.SetSelectedGameObject(optionsButton);
        }
        pauseMenu.SetActive(true);
        options.SetActive(false);
        SetVolume(gameVolume);
    }

    public void OpenSettingsInMainMenu()
    {
        if (ControllerCursor.Instance.isControllerConnected)
        {
            EventSystem.current.SetSelectedGameObject(saveOptionsButton);
        }
        mainMenu.SetActive(false);
        options.SetActive(true);
        SetVolume(gameVolume);

        // Load the saved volume value
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void ConfirmSettingsInMainMenu()
    {
        if (ControllerCursor.Instance.isControllerConnected)
        {
            EventSystem.current.SetSelectedGameObject(optionsButton);
        }
        mainMenu.SetActive(true);
        options.SetActive(false);
        SetVolume(gameVolume);
    }

    public void OnVolumeChanged(float volume)
    {
        // Set the volume for all audio sources
        SetVolume(volume);
        gameVolume = volume;

        // Save the volume value
        PlayerPrefs.SetFloat(VolumeKey, volume);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        // Find all instances of AudioSource in the scene
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // Set the volume for all audio sources
        foreach (var audioSource in audioSources)
        {
            audioSource.volume = volume;
        }
    }

    public void GoToMainMenu()
    {
        // Load the MainMenu
        SceneManager.LoadScene(0);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(1);
    }
}