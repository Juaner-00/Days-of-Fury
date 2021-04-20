using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActiveDestroyTower : MonoBehaviour
{
    [SerializeField]
    GameObject tower,text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tower == null) text.SetActive(true);
    }
    void Deactivate()
    {
        text.SetActive(false);
    }
}
