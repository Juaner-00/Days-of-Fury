using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextsTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject text,tower;

    bool isDestroy = false;
    private void Update()
    {
        if(gameObject.name == "ColliderText6" )
        {
            
            if (tower == null && isDestroy == false)
            {
                text.SetActive(true);
                Invoke("Deactivate", 5f);
                isDestroy = true;
            }

        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            text.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            text.SetActive(false);
        }
    }
    void Deactivate()
    {
        text.SetActive(false);
    }


}
