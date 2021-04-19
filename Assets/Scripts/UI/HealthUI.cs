using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifesText;
    [SerializeField] Image damaged, glass;
    [SerializeField] AnimationCurve curve;
    [SerializeField] Color color1, color2;


    PlayerHealth pHealth;
    float t = 0;
    public float tileDuration;
    public float divisor;

    void Start()
    {
        pHealth = GameManager.Player.GetComponent<PlayerHealth>();
        if (damaged)
            damaged.color = new Color(damaged.color.r, damaged.color.g, damaged.color.b, 0);
        if (glass)
            glass.color = new Color(glass.color.r, glass.color.g, glass.color.b, 0);

    }

    void Update()
    {
        t += Time.deltaTime;
        float c = (curve.Evaluate(t / tileDuration));
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

        divisor = (float)pHealth.HealthPoints;
        if (glass)
            glass.color = new Color(glass.color.r, glass.color.g, glass.color.b, 0.4f - (divisor / pHealth.MaxHealthPoints));
        if (damaged)
            damaged.color = new Color(damaged.color.r, damaged.color.g, damaged.color.b, 0.7f - (divisor / pHealth.MaxHealthPoints));
        lifesText.text = $"{lives}";
    }
}
