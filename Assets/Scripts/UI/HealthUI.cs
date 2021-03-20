using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifesText;

    PlayerHealth health;

    private void Start()
    {
        if (GameManager.Player)
            health = GameManager.Player.GetComponentInParent<PlayerHealth>();
        UpdateUI();
    }

    private void OnEnable()
    {
        PlayerHealth.OnChangeLife += UpdateUI;
    }

    private void OnDisable()
    {
        PlayerHealth.OnChangeLife -= UpdateUI;
    }

    void UpdateUI()
    {
        if (health)
            lifesText.text = $"x {health.HealthPoints}";
    }
}
