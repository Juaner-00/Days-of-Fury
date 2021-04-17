using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] GameObject [] coreRays, texts;
    [SerializeField] GameObject lastRay, text;

    [SerializeField] float distance, time = 0f, enemyTotalTime;
    [SerializeField] float[] totalTime;

    RaycastHit [] ray;
    RaycastHit enemyRay;

    [SerializeField] bool firstClick = false;

    private void Start()
    {
        ray = new RaycastHit[coreRays.Length];
    }

    private void Update()
    {
        //if(firstClick == false)
        //{
        //    if(Input.anyKey)
        //    {
        //        firstClick = true;
        //    }
        //}

        //if (firstClick == true)
        //{
        //    time += Time.deltaTime;
        //}

        //for (int i = 0; i < coreRays.Length; i++)
        //{
        //    if (time <= totalTime[i])
        //    {
        //        texts[i].SetActive(false);
        //    }
        //}

        //if(time <= enemyTotalTime)
        //{
        //    text.SetActive(false);
        //}
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < coreRays.Length; i++)
        {
            bool isHit = Physics.Raycast(coreRays[i].transform.position, coreRays[i].transform.forward, out ray[i], distance);

            if (isHit && ray[i].transform.CompareTag("Player"))
            {
                texts[i].SetActive(true);
                Debug.DrawRay(coreRays[i].transform.position, coreRays[i].transform.TransformDirection(Vector3.forward) * ray[i].distance, Color.green);
            }
            else
                Debug.DrawRay(coreRays[i].transform.position, coreRays[i].transform.TransformDirection(Vector3.forward) * ray[i].distance, Color.red);
        }

        bool isHitEnemy = Physics.Raycast(lastRay.transform.position, lastRay.transform.forward, out enemyRay, distance);

        if (isHitEnemy && enemyRay.transform.CompareTag("Enemy"))
        {
            text.SetActive(false);
            Debug.DrawRay(lastRay.transform.position, lastRay.transform.TransformDirection(Vector3.forward) * enemyRay.distance, Color.green);
        }
        else
        {
            text.SetActive(true);
            Debug.DrawRay(lastRay.transform.position, lastRay.transform.TransformDirection(Vector3.forward) * enemyRay.distance, Color.red);
        }

    }
}
