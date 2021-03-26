using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDmanager : MonoBehaviour
{

    [Header("EnemyTexts")]
    [SerializeField] TextMeshProUGUI enemyText; //Texto
    [SerializeField] TextMeshProUGUI enemyTextWin; //Texto
    [SerializeField] TextMeshProUGUI enemyTextLose; //Texto


    [Header("ScoreTexts")]
    [SerializeField] TextMeshProUGUI ScoreTextWin; //Texto
    [SerializeField] TextMeshProUGUI ScoreTextLose; //Texto




    int a = 0;
    int b = 0;
    ScoreManager SC;

    public void Start()
    {
        SC = ScoreManager.Instance;
        enemyText.text = $"{a}";
        enemyTextWin.text = $"{a}";
        enemyTextLose.text = $"{a}";
    }
    private void OnEnable()
    {
        EnemyController.Kill += Refresh;
    }

    void Refresh()
    {        
        a++;
        b = SC.TotalScore;

        enemyText.text = $"{a}";
        enemyTextWin.text = $"{a}";
        enemyTextLose.text = $"{a}";

        ScoreTextWin.text = $"{b}";
        ScoreTextLose.text = $"{b}";    
    }




}
