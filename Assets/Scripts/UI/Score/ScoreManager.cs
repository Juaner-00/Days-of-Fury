using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour, ICounterValueContainer
{
    [SerializeField] int oneStar;
    [SerializeField] int twoStar;
    [SerializeField] int threeStar;


    [SerializeField] GameObject oneStarMedal;
    [SerializeField] GameObject twoStarMedal;
    [SerializeField] GameObject threeStarMedal;

    [SerializeField] private int totalScore;
    int state = 0;

    //0= Sin estrellas
    //1= 1 Estrella
    //2= 2 Estrellas
    //3= 3 Estrellas

    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    private void Update()
    {
        //scoreText.text = "" + totalScore;
    }


    public void Addscore(int score)
    {
        totalScore += score;
        Checkscore();
    }

    void Checkscore()
    {
        if (totalScore >= oneStar)
        {
            state = 1;
            oneStarMedal.SetActive(true);
        }
        if (totalScore >= twoStar)
        {
            state = 2;
            twoStarMedal.SetActive(true);
        }
        if (totalScore >= threeStar)
        {
            state = 3;
            threeStarMedal.SetActive(true);
        }
    }

    public int GetValue()
    {
        return totalScore;
    }

    public int TotalScore => totalScore;
    public static ScoreManager Instance { get; private set; }
}
