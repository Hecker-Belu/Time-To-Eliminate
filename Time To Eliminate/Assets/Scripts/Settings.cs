using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Toggle vsyncToggle;
    public Slider fovSlider;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;

    public UnityEngine.Audio.AudioMixer audioMixer;

    public float sensitivity = 0.02f;
    public bool vsync = false;
    public float fov = 90;
    public Vector2 resolution = new Vector2(1920, 1080);

    public static bool isSettings = false;
    public GameObject closeButton;
    public GameObject panel;

    // List of supported resolutions
    private readonly Vector2[] availableResolutions =
    {
        new Vector2(1920, 1080),
        new Vector2(1280, 720),
        new Vector2(720, 480)
    };

    void Awake()
    {
        if (!isSettings)
        {
            isSettings = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        // Load resolution
        resolution = new Vector2(PlayerPrefs.GetInt("ResX", 1920), PlayerPrefs.GetInt("ResY", 1080));

        // Set dropdown index
        for (int i = 0; i < availableResolutions.Length; i++)
        {
            if (availableResolutions[i] == resolution)
            {
                resolutionDropdown.value = i;
                break;
            }
        }

        // Load sensitivity
        sensitivity = PlayerPrefs.GetFloat("sens", 0.02f);
        sensitivitySlider.value = sensitivity;

        // Load fov
        fov = PlayerPrefs.GetFloat("fov", 90f);
        fovSlider.value = fov;

        // Load vsync
        vsync = PlayerPrefs.GetInt("vsync", 1) == 1;
        vsyncToggle.isOn = vsync;

        // Apply vsync
        QualitySettings.vSyncCount = vsync ? 1 : 0;

        // Load volume (0–100)
        float savedVolume = PlayerPrefs.GetFloat("volume", 75f);
        volumeSlider.value = savedVolume;

        // Convert to 0–1
        float linear = savedVolume / 100f;

        // Prevent log10(0)
        if (linear <= 0.0001f) linear = 0.0001f;

        // Apply to mixer
        audioMixer.SetFloat("Master", Mathf.Log10(linear) * 20f);

    }

    public void UpdateSettings()
    {
        // Read UI values
        fov = fovSlider.value;
        sensitivity = sensitivitySlider.value;
        vsync = vsyncToggle.isOn;

        // Apply resolution
        Vector2 selectedRes = availableResolutions[resolutionDropdown.value];
        Screen.SetResolution((int)selectedRes.x, (int)selectedRes.y, FullScreenMode.ExclusiveFullScreen);

        // Read slider (0–100)
        float volume = volumeSlider.value;

        // Convert to 0–1
        float linear = volume / 100f;

        // Prevent log10(0)
        if (linear <= 0.0001f) linear = 0.0001f;

        // Apply to mixer
        audioMixer.SetFloat("Master", Mathf.Log10(linear) * 20f);

        // Save
        PlayerPrefs.SetFloat("volume", volume);

        // Save resolution
        PlayerPrefs.SetInt("ResX", (int)selectedRes.x);
        PlayerPrefs.SetInt("ResY", (int)selectedRes.y);

        // Save other settings
        PlayerPrefs.SetFloat("fov", fov);
        PlayerPrefs.SetFloat("sens", sensitivity);
        PlayerPrefs.SetInt("vsync", vsync ? 1 : 0);
        PlayerPrefs.Save();

        // Apply vsync
        QualitySettings.vSyncCount = vsync ? 1 : 0;
    }

    public void QuitGame()
    {
        print("quit");
        Application.Quit();
    }
}
