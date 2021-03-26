using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPickUps : MonoBehaviour
{
    [SerializeField]
    ParticleSystem plifeUp, pfastShoot, ppointUp, pfastMove;

    private void OnEnable()
    {
        PickUpBase.OnPick += ActivateVfx;
    }

    private void OnDisable()
    {
        PickUpBase.OnPick -= ActivateVfx;

    }
    
    void ActivateVfx(Vector3 _,PickUpType pType)
    {
        switch (pType)
        {
            case PickUpType.Life:
                plifeUp.Play();
                break;
            case PickUpType.Shoot:
                pfastShoot.Play();
                break;
            case PickUpType.Speed:
                pfastMove.Play();
                break;
            case PickUpType.Score:
                ppointUp.Play();
                break;   
        }
    }
    


}
