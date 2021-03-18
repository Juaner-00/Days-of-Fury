using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolVfxs : MonoBehaviour
{
    [Header("Pool Properties")]
    [SerializeField] ParticleSystem item;
    [SerializeField] int length = 30;

    IPool[] items;
    ParticleSystem[] objects;
    int index = 0;


    private void Awake()
    {
        items = new IPool[length];
        objects = new ParticleSystem[length];

        for (int i = 0; i < length; i++)
        {
            objects[i] = Instantiate(item, transform.position, Quaternion.identity);
            objects[i].transform.parent = transform;
            items[i] = objects[i].GetComponent<IPool>();
            items[i].Instantiate();
        }
    }

    // Devuelve un objeto del pool
    public ParticleSystem GetItem(Vector3 position, string tag = "")
    {
        items[index].Begin(position, tag, Vector3.zero);
        ParticleSystem tmp = objects[index];
        index++;

        if (index >= items.Length)
            index = 0;

        return tmp;
    }
}
