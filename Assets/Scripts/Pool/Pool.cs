using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [Header("Pool Properties")]
    [SerializeField] GameObject item;
    [SerializeField] int length = 30;

    Queue<IPool> items;
    Queue<GameObject> objects;


    protected virtual void Awake()
    {
        items = new Queue<IPool>(length);
        objects = new Queue<GameObject>(length);

        for (int i = 0; i < length; i++)
            InstantiateItem();
    }

    protected virtual void InstantiateItem()
    {
        GameObject objTemp = Instantiate(item, transform.position, Quaternion.identity);
        IPool ipoolTemp = objTemp.GetComponent<IPool>();

        ipoolTemp.Instantiate(this);
        objTemp.transform.parent = transform;

        objects.Enqueue(objTemp);
        items.Enqueue(ipoolTemp);
    }

    // Devuelve un objeto del pool en la posición requerida
    public virtual GameObject GetItem(Vector3 position, Vector3 pos, string tag = "")
    {
        if (items.Count == 0)
            InstantiateItem();

        IPool ipoolTemp = items.Dequeue();
        ipoolTemp.Begin(position, tag, pos);

        GameObject tmp = objects.Dequeue();

        return tmp;
    }

    // Añade el objeto al pool
    public void PushItem(GameObject newObject)
    {
        objects.Enqueue(newObject);
        items.Enqueue(newObject.GetComponent<IPool>());
    }
}
