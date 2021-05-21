using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TextsTutorial : MonoBehaviour
{
    [SerializeField]
    GameObject text,tower;
    [SerializeField]
    AudioClip narratorClip;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

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
            source.clip = narratorClip;
            source.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            text.SetActive(false);
            source.Stop();
        }
    }
    void Deactivate()
    {
        text.SetActive(false);
    }
}
