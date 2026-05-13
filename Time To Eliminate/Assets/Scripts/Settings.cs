using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Slider sensitivitySlider;
    public Toggle vsyncToggle;

    public float sensitivity = 0.02f;
    public bool vsync = false;
    public static bool settingsCreated = false;

    public GameObject closeButton;
    public GameObject panel;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // Load sensitivity
        sensitivity = PlayerPrefs.GetFloat("sens", 0.02f);
        sensitivitySlider.value = sensitivity;

        // Load vsync
        vsync = PlayerPrefs.GetInt("vsync", 1) == 1;
        vsyncToggle.isOn = vsync;

        // Apply vsync
        QualitySettings.vSyncCount = vsync ? 1 : 0;
    }

    public void UpdateSettings()
    {
        // Read UI values
        sensitivity = sensitivitySlider.value;
        vsync = vsyncToggle.isOn;

        // Save
        PlayerPrefs.SetFloat("sens", sensitivity);
        PlayerPrefs.SetInt("vsync", vsync ? 1 : 0);
        PlayerPrefs.Save();

        // Apply vsync
        QualitySettings.vSyncCount = vsync ? 1 : 0;
    }
}
