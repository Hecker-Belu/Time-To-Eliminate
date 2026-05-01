using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public string NextScene;

    private float time = 10.0f;
    private bool started = true;

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
            timer.text = time.ToString() + "s";
            if (time <= 0.0f)
            {
                Lose();
            }
        }
    }

    public void Win()
    {
        print(started);
        if (time <= 0.0f)
        {
            Lose();
        }
        if (started)
        {
            started = false;
            SceneManager.LoadScene(NextScene);
        }
    }

    public void Lose()
    {
        print("You lose");
    }
}
