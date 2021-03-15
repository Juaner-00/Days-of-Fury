using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [Header("Pool Properties")]
    [SerializeField] GameObject item;
    [SerializeField] int length = 30;

    IPool[] items;
    GameObject[] objects;
    int index = 0;


    private void Awake()
    {
        items = new IPool[length];
        objects = new GameObject[length];

        for (int i = 0; i < length; i++)
        {
            objects[i] = Instantiate(item, transform.position, Quaternion.identity);
            objects[i].transform.parent = transform;
            items[i] = objects[i].GetComponent<IPool>();
            items[i].Instantiate();
        }
    }

    // Devuelve un objeto del pool en la posición requerida
    public GameObject GetItem(Vector3 position, string tag = "")
    {
        items[index].Begin(position, tag);
        GameObject tmp = objects[index];
        index++;

        if (index >= items.Length)
            index = 0;

        return tmp;
    }
}
