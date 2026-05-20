using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Settings settings;
    public Button mainMenuCloseButton;
    public AudioSource uiSelect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        GameObject.Find("Settings").TryGetComponent<Settings>(out settings);
        mainMenuCloseButton.onClick.AddListener(settings.QuitGame);
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LevelSelect(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
}
