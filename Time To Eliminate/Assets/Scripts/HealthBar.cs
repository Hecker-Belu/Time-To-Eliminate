using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float MaxHealth = 100f;
    public GameObject healthBarValue; // The "Value" child panel
    private float Health;
    private float maxWidth;


    private RectTransform valueRect;

    void Awake()
    {
        valueRect = healthBarValue.GetComponent<RectTransform>();
        Health = MaxHealth;
        maxWidth = valueRect.rect.width;
        UpdateBar();
    }

    public void SetHealth(float newHealth)
    {
        print(newHealth);
        Health = Mathf.Clamp(newHealth, 0, MaxHealth);
        UpdateBar();
    }

    private void UpdateBar()
    {
        float percent = Health / MaxHealth;
        valueRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, percent * maxWidth);
    }
}
