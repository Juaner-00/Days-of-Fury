using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Counter : MonoBehaviour
{
    // public GameObject scoreManager;
    // private ICounterValueContainer valueManager;
    [SerializeField] float increment;

    private TextMeshProUGUI scoreIndicatorTMPro;
    private float currentValue;



    void Start()
    {
        // valueManager = scoreManager.GetComponent<ICounterValueContainer>();        
        scoreIndicatorTMPro = GetComponent<TextMeshProUGUI>();
    }

    private void FixedUpdate()
    {
        RefreshCounter();
    }

    private void RefreshCounter()
    {
        // int targetValue = valueManager.GetValue();
        int targetValue = ScoreManager.Instance.GetValue();


        if (currentValue != targetValue)
        {

            increment += 5 * Time.deltaTime;
            //PONGAN AQUI LOS SONIDITOS O SUS MIERDITAS :3

            if (currentValue < targetValue)
            {
                currentValue += increment;
                if (currentValue > targetValue)
                {
                    currentValue = targetValue;
                }
            }
            else
            {
                currentValue -= increment;
                if (currentValue < targetValue)
                {
                    currentValue = targetValue;
                }
            }
        }
        scoreIndicatorTMPro.text = ((int)currentValue).ToString();

    }
}
