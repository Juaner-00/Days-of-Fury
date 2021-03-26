using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    ParticleSystem vida;
    ParticleSystem.ShapeModule shapeLife;
    
    // Start is called before the first frame update
    void Start()
    {
        vida = GetComponent<ParticleSystem>();
        shapeLife = vida.shape;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
