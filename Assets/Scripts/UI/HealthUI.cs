using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifesText;
    [SerializeField] Image damaged, glass;
    [SerializeField] Color color1, color2;

    [SerializeField] PlayerHealth health;

    float divisor;

    void Start()
    {
        if (damaged)
            damaged.color = new Color(damaged.color.r, damaged.color.g, damaged.color.b, 0);
        if (glass)
            glass.color = new Color(glass.color.r, glass.color.g, glass.color.b, 0);

    }

    private void OnEnable()
    {
        PlayerHealth.OnChangeLife += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerHealth.OnChangeLife -= UpdateUI;
    }

    void UpdateUI(int lives)
    {
        divisor = (float)health.HealthPoints;

        if (glass)
            glass.color = new Color(glass.color.r, glass.color.g, glass.color.b, 0.4f - (divisor / health.MaxHealthPoints));
        if (damaged)
            damaged.color = new Color(damaged.color.r, damaged.color.g, damaged.color.b, 0.7f - (divisor / health.MaxHealthPoints));
        lifesText.text = $"{lives}";
    }
}
