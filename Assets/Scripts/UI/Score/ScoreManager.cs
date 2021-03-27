using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour, ICounterValueContainer
{
    bool oneStar1;
    bool twoStar1;
    bool threeStar1;

    [SerializeField] GameObject oneStarMedal;
    [SerializeField] GameObject twoStarMedal;
    [SerializeField] GameObject threeStarMedal;

    [SerializeField] private int totalScore;

    public static event Action<int> OnGetScore = delegate { };


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
        OnGetScore?.Invoke(score);
    }
    public void ActiveOneStarMedal()
    {
        oneStarMedal.SetActive(true);
        oneStar1 = true;
        starState = Medals.OneMedal;
        oneStarMedal.SetActive(true);
        OnStarObtained?.Invoke(starState);
    }
    public void ActiveTwoStarMedal()
    {
        twoStarMedal.SetActive(true);
        twoStar1 = true;
        starState = Medals.TwoMedal;
        twoStarMedal.SetActive(true);
        OnStarObtained?.Invoke(starState);
    }
    public void ActiveThreeStarMedal()
    {
        threeStarMedal.SetActive(true);
        threeStar1 = true;
        starState = Medals.ThreeMedal;
        threeStarMedal.SetActive(true);
        OnStarObtained?.Invoke(starState);
    }

    public int GetValue()
    {
        return totalScore;
    }

    public int TotalScore => totalScore;
    public static ScoreManager Instance { get; private set; }
    public bool OneStar1 { get; private set; }
    public bool TwoStar1 { get; private set; }
    public bool ThreeStar1 { get; private set; }
}

public enum Medals
{
    None,
    OneMedal,
    TwoMedal,
    ThreeMedal
}