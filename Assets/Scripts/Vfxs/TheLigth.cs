using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLigth : MonoBehaviour
{
    [SerializeField]
    AnimationCurve curve;
    [SerializeField]
    Light luz;

    public float intensidad;

    float t = 0;

    public float duracion;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        t += Time.deltaTime;
        luz.intensity = intensidad * (curve.Evaluate(t / duracion));

    }
}
