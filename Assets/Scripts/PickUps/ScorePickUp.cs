using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickUp : PickUpBase
{
    [SerializeField] int score;

    protected override void Pick()
    {
        ScoreManager.Instance.Addscore(score);
        base.Pick();
    }
}
