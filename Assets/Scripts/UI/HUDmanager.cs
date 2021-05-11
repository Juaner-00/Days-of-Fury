using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDmanager : MonoBehaviour
{
    public static HUDmanager Instance { get; private set; }

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

    public void Awake()
    {        
        if (Instance) Destroy(gameObject);
        Instance = this;
       
    }

    public void Start()
    {
        SC = ScoreManager.Instance;
        enemyText.text = $"{a}";
        enemyTextWin.text = $"{a}";
        enemyTextLose.text = $"{a}";

        ScoreTextWin.text = $"{0}";
        ScoreTextLose.text = $"{0}";

    }
    private void OnEnable()
    {
        EnemyController.Kill += Refresh;
    }



    public void Refresh()
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
