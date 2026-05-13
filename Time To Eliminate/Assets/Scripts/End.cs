using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public TextMeshProUGUI creditsText;
    private float time = 30f;

    public void Update()
    {
        if (creditsText != null) {
            creditsText.rectTransform.anchoredPosition = new Vector2(creditsText.rectTransform.anchoredPosition.x, creditsText.rectTransform.anchoredPosition.y + 100f * Time.deltaTime);
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
