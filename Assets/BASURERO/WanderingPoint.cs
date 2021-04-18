using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingPoint : MonoBehaviour
{
    [SerializeField] int randomXmin, randomXmax, randomZmin, randomZmax;
    [SerializeField]
    float radius;
    public LayerMask ColliderMask;

    /*[HideInInspector]
    public GameObject enemy;
    public bool taken;

    void Awake()
    {
        taken = false;
    }
    */
    private void Start()
    {
        NewPosition();
    }

    void Update()
    {
        /*if (enemy)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= 0.5f)
            {
                NewPosition();
            } 
        }*/
        
        Collider[] wallNoWalking = Physics.OverlapSphere(transform.position, radius, ColliderMask);
        if (wallNoWalking.Length >= 1)
        {
            NewPosition();
        }
    }

    void NewPosition()
    {
        float randomX = Random.Range(randomXmin, randomXmax);
        float randomZ = Random.Range(randomZmin, randomZmax);
        gameObject.transform.position = new Vector3(randomX, transform.position.y, randomZ);
    }
}
