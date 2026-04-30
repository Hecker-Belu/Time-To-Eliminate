using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timer;
    private float time = 10.0f;

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
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

    public void Lose()
    {
        print("BITCH ASS NIGGER");
    }
}
