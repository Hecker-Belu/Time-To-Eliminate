using TMPro;
using UnityEngine;

public class End : MonoBehaviour
{
    public TextMeshProUGUI creditsText;

    public void Update()
    {
        if (creditsText != null) {
            creditsText.rectTransform.anchoredPosition = new Vector2(-400f, creditsText.rectTransform.anchoredPosition.y + 100f * Time.deltaTime);
        }
    }
}
