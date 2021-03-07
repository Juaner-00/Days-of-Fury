using System;
using System.Collections.Generic;
using UnityEngine;

public class ReticleController : MonoBehaviour
{
    [Header("Turret Properties")]
    [SerializeField] Transform turretTransform;

    [SerializeField] Transform reticleTransform;
    [SerializeField] TurretTank turretTank;
    


    Quaternion target;
    Vector3 reticlePosition;

    public Action OnShooting;

    private void Update()
    {


        HandleInputs();

        if (Input.GetButtonDown("Fire1"))
        {
            
            if (turretTank)
            {
                turretTank.Shot();
                OnShooting?.Invoke();
            }
               

        }
    }

    private void FixedUpdate()
    {
        HandleTurret();
        HandleReticle();
    }

    void HandleTurret()
    {
        if (turretTransform)
        {
            Vector3 turretLookDir = reticlePosition - turretTransform.position;
            Vector3 newDir = Vector3.RotateTowards(turretTransform.forward, turretLookDir, 1, 0.0F);
            target = Quaternion.LookRotation(newDir);
            turretTransform.rotation = Quaternion.Euler(-90, target.eulerAngles.y, 0);
        }
    }

    void HandleReticle()
    {
        if (reticleTransform)
        {
            reticleTransform.position = reticlePosition;
        }
    }

    void HandleInputs()
    {
        Ray screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit))
        {
            reticlePosition = hit.point;
        }
    }
}
