using System;
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

    Medals starState;

    public static Action<Medals> OnStarObtained;


    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        starState = Medals.None;
    }

    // Método para añadir score
    public void Addscore(int score)
    {
        totalScore += score;
        Checkscore();
    }

    void Checkscore()
    {
        if (totalScore >= threeStar)
        {
            starState = Medals.ThreeMedal;
            threeStarMedal.SetActive(true);
            OnStarObtained?.Invoke(starState);
        }
        else if (totalScore >= twoStar)
        {
            starState = Medals.TwoMedal;
            twoStarMedal.SetActive(true);
            OnStarObtained?.Invoke(starState);
        }
        else if (totalScore >= oneStar)
        {
            starState = Medals.OneMedal;
            oneStarMedal.SetActive(true);
            OnStarObtained?.Invoke(starState);
        }
    }

    public int GetValue()
    {
        return totalScore;
    }

    public int TotalScore => totalScore;
    public static ScoreManager Instance { get; private set; }
}

public enum Medals
{
    None,
    OneMedal,
    TwoMedal,
    ThreeMedal
}