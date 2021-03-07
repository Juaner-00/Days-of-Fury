using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingPoint : MonoBehaviour
{
    GameObject enemy;
    [SerializeField] int randomXmin, randomXmax, randomZmin, randomZmax;

    void Update()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        float randomX = Random.Range(randomXmin, randomXmax);
        float randomZ = Random.Range(randomZmin, randomZmax);
        if(gameObject.transform.position == enemy.transform.position)
        {
            gameObject.transform.position = new Vector3(randomX, transform.position.y, randomZ);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        float randomX = Random.Range(randomXmin, randomXmax);
        float randomZ = Random.Range(randomZmin, randomZmax);
        gameObject.transform.position = new Vector3(randomX, transform.position.y, randomZ);
    }
}
