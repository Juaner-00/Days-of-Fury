using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pool : MonoBehaviour
{
    [Header("Pool Properties")]
    [SerializeField] GameObject item;
    [SerializeField] int length = 30;

    [Header("Debug (only for enemies)")]
    [SerializeField] int cantEnemiesThatStay;

    List<IPool> items;
    List<GameObject> objects;

    int index = 0;

    int cantNotStay = 0;

    private void Awake()
    {
        items = new List<IPool>();
        objects = new List<GameObject>();

        // for (int i = 0; i < length; i++)
        while (cantNotStay < length)
            InstantiateItem();

        cantEnemiesThatStay = items.Count(c => c.StayOnScene);
    }

    void InstantiateItem()
    {
        GameObject objTemp = Instantiate(item, transform.position, Quaternion.identity);
        IPool ipoolTemp = objTemp.GetComponent<IPool>();

        ipoolTemp.Instantiate();
        objTemp.transform.parent = transform;

        objects.Add(objTemp);
        items.Add(ipoolTemp);

        if (!ipoolTemp.StayOnScene)
            cantNotStay++;
    }

    // Devuelve un objeto del pool en la posición requerida
    public GameObject GetItem(Vector3 position, Vector3 pos, string tag = "")
    {
        IPool ipoolTemp = items[index];

        ipoolTemp.Begin(position, tag, pos);
        GameObject tmp = objects[index];

        if (ipoolTemp.StayOnScene)
        {
            objects.RemoveAt(index);
            items.RemoveAt(index);
        }

        index++;

        if (index >= items.Count)
            index = 0;

        return tmp;
    }
}
