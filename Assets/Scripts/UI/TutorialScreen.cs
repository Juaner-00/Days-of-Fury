using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] GameObject [] coreRays, texts;
    [SerializeField] GameObject lastRay, text;

    [SerializeField] float distance;

    bool[] confirm;
    bool confirmEnemy;

    RaycastHit [] ray;
    RaycastHit enemyRay;

    [SerializeField] bool firstClick = false;

    private void Start()
    {
        ray = new RaycastHit[coreRays.Length];
        confirm = new bool[coreRays.Length];
        confirmEnemy = false;

        for (int i = 0; i < confirm.Length; i++)
        {
            confirm[i] = false;
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < coreRays.Length; i++)
        {
            bool isHit = Physics.Raycast(coreRays[i].transform.position, coreRays[i].transform.forward, out ray[i], distance);

            if (isHit && ray[i].transform.CompareTag("Player") && confirm[i] == false)
            {
                texts[i].SetActive(true);
                Debug.DrawRay(coreRays[i].transform.position, coreRays[i].transform.TransformDirection(Vector3.forward) * ray[i].distance, Color.green);
                confirm[i] = true;

                DeactivateText(texts[i]);
            }
            else
                Debug.DrawRay(coreRays[i].transform.position, coreRays[i].transform.TransformDirection(Vector3.forward) * ray[i].distance, Color.red);
        }

        bool isHitEnemy = Physics.Raycast(lastRay.transform.position, lastRay.transform.forward, out enemyRay, distance);

        if (isHitEnemy && enemyRay.transform.CompareTag("Enemy") && confirmEnemy == false)
        {
            text.SetActive(false);
            Debug.DrawRay(lastRay.transform.position, lastRay.transform.TransformDirection(Vector3.forward) * enemyRay.distance, Color.green);
        }
        else
        {
            text.SetActive(true);
            Debug.DrawRay(lastRay.transform.position, lastRay.transform.TransformDirection(Vector3.forward) * enemyRay.distance, Color.red);
            confirmEnemy = true;
            DeactivateText(text);
        }
    }

    public void DeactivateText(GameObject activatedText)
    {
        float time = 2f; 
        while(time > 0)
        {
            time -= Time.deltaTime;
        }
        activatedText.SetActive(false);
    }
}
