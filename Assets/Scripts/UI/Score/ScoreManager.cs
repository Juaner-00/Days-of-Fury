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

    public static Action OnOneMedalObtain;
    public static Action OnTwoMedalObtain;
    public static Action OnThreeMedalObtain;


    Medals medalState;

    public static Action<Medals> OnMedalObtained;


    private void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        medalState = Medals.None;
    }

    // Método para añadir score
    public void Addscore(int score)
    {
        totalScore += score;
        OnGetScore?.Invoke(score);
        HUDmanager.Instance.Refresh();
    }

    public void ActiveOneStarMedal()
    {
        oneStarMedal.SetActive(true);
        oneStar1 = true;
        medalState = Medals.OneMedal;
        oneStarMedal.SetActive(true);
        OnMedalObtained?.Invoke(medalState);
        OnOneMedalObtain?.Invoke();
    }

    public void ActiveTwoStarMedal()
    {
        twoStarMedal.SetActive(true);
        twoStar1 = true;
        medalState = Medals.TwoMedal;
        twoStarMedal.SetActive(true);
        OnMedalObtained?.Invoke(medalState);
        OnTwoMedalObtain?.Invoke();
    }

    public void ActiveThreeStarMedal()
    {
        threeStarMedal.SetActive(true);
        threeStar1 = true;
        medalState = Medals.ThreeMedal;
        threeStarMedal.SetActive(true);
        OnMedalObtained?.Invoke(medalState);
        OnThreeMedalObtain?.Invoke();
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
    public Medals CurrentMedal { get => medalState; }
}

public enum Medals
{
    None,
    OneMedal,
    TwoMedal,
    ThreeMedal
}