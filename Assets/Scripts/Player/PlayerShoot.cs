using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerShoot : TurretTank
{
    [Header("PlayerShoot")]
    [SerializeField]
    int numShoots;
    [SerializeField]
    Slider fill;
    [SerializeField]
    float coldDown, cdLastShoot;
    [SerializeField]
    Image bgSlider;
    [SerializeField]
    AnimationCurve curveAlpha;
    float counter, coldDownTimer;


    protected override void Start()
    {
        coldDownTimer = 0;
        base.Start();
        fill.maxValue = numShoots;
        counter = numShoots;
    }

    void Update()
    {
        available = counter >= 1;
        if (coldDownTimer == 0)
        {
            counter += Time.deltaTime * AttackSpeed;
            counter = Mathf.Clamp(counter, 0f, numShoots);

        }
        else
        {
            bgSlider.color = new Color(bgSlider.color.r, bgSlider.color.g, bgSlider.color.b, curveAlpha.Evaluate(coldDown - coldDownTimer / coldDown));
            coldDownTimer -= Time.deltaTime;
            coldDownTimer = Mathf.Clamp(coldDownTimer, 0f, cdLastShoot);
        }

        fill.value = counter;
        /*if (counter < 3) multiplier = 1f;
        if (counter < 2) multiplier = 0.66f;
        if (counter < 1) multiplier = 0.33f ;*/
    }

    public override bool Shot()
    {
        if (counter < 1)
            return false;

        if (smokeFire)
            smokeFire.Play();

        counter--;
        coldDownTimer = coldDown;


        Vector3 force = -transform.up * this.force;
        GameObject clone = cartrigde.GetItem(reference.position, Vector3.zero, tag);
        clone.transform.LookAt(clone.transform.position - transform.up);
        clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        return true;
    }
}
