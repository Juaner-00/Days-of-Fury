using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject Text1;
    public GameObject Text2;

    public void OnEnable()
    {
        ScoreManager.OnOneMedalObtain += Texto1;
        ScoreManager.OnTwoMedalObtain += Texto2;
    }

    void Texto1()
    {
        Text1.gameObject.SetActive(true);
    }
    void Texto2()
    {
        Text2.gameObject.SetActive(true);
    }
}
