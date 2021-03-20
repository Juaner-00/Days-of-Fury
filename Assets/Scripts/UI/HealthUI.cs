using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifesText;

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
        lifesText.text = $"{lives}";
    }
}
