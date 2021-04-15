using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifesText;
    [SerializeField] Image damaged;
    PlayerHealth pHealth;

    void Start()
    {

        pHealth = GameManager.Player.GetComponentInParent<PlayerHealth>(); 
    }
    void Update()
    {
        
        if (pHealth.MaxHealthPoints > pHealth.HealthPoints)
        {
            damaged.color = new Color(damaged.color.r, damaged.color.g, damaged.color.b,  0.8f);

        }
        else
        {
            damaged.color = new Color(damaged.color.r, damaged.color.g, damaged.color.b, 0f);
        }
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
        lifesText.text = $"{lives}";
        
    }
}
