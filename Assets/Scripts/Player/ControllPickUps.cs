using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllPickUps : MonoBehaviour
{
    [SerializeField]
    ParticleSystem lifeUp, fastShot, pointUp;
    [SerializeField]
    GameObject life, atackSpeed, scoreUp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other == life)
        {
            lifeUp.Play();
        }
        if (other == atackSpeed)
        {
            fastShot.Play();
        }
        if (other == scoreUp)
        {
            pointUp.Play();
        }
    }


}
