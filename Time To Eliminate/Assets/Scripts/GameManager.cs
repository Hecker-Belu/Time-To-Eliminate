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
    public float time = 10.0f;
    public bool paused = false;

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Update()
    {
        UpdateGame();
    }

    public void UpdateGame()
    {
        if (timer != null && !paused)
        {
            time -= Time.deltaTime;
            timer.SetText(time.ToShortString(3) + "s");
        }

        if (time <= 0.0f)
        {
            Lose();
        }
    }

    public void Win()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(nextScene);
    }

    public void Lose()
    {
        print("You lose");
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
