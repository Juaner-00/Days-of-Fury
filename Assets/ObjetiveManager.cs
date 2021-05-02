using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjetiveManager : MonoBehaviour
{
    //public static ObjetiveManager Instance { get; private set; }
    public static Action<int> OnReachObjetive;

    public int i;

    private void Awake()
    {
        /*if (Instance)
            Destroy(gameObject);
        Instance = this;*/
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.tag);
        if (other.gameObject.tag == "Player")  {
            OnReachObjetive?.Invoke(i);
        }
        
    }
}
