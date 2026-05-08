using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public string nextScene;
    public GameObject nextText;
    private float time = 60.0f;
    private bool started = false;
    private static bool gameManagerCreated = false;
    public string[] levelName = { "Level 1", "Level 2", "Level 3", };
    public int levelIndex = 0;




    private void Awake()
    {
        if (!gameManagerCreated)
        {
            gameManagerCreated = true;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
        started = true;
    }

    public void Update()
    {
        if (started)
        {
            UpdateGame();
        }
    }

    public void UpdateGame()
    {
        if (timer != null)
        {
            time -= Time.deltaTime;
            timer.SetText(time.ToShortString(3) + "s");
            print(time.ToString());
        }
        else
        {
            timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        }

        if (time <= 0.0f)
        {
            Lose();
        }
    }

    public void Win()
    {
        print(started);
        if (started)
        {
            started = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(nextScene);
        }
    }

    public void Lose()
    {
        print("You lose");
        started = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void next()
    {
        print("next level");
        SceneManager.LoadScene(nextScene);
    }
}
