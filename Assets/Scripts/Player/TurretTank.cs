using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretTank : MonoBehaviour
{
    [Header("Shoot Properties")]
    [SerializeField] float force , attackSpeedBase;
    [SerializeField] Transform reference;
    [SerializeField] ParticleSystem smokeFire;
    [SerializeField] Slider reload;
    [SerializeField] Image fill;


    [SerializeField] int shootsMax;
    [SerializeField] float reloadTime;


    int numShoots;
    bool available = true;
    Pool cartrigde;

    public bool Available { get => available; }


    bool counterStart = false,justShoot = false;

    [SerializeField, Header("Debug")]
    float attackSpeed;

    float counterSlider,counterShoot;

    private void Start()
    {
        cartrigde = GameObject.Find("Cartrigde (Pool)").GetComponent<Pool>();

        attackSpeed = attackSpeedBase;
    }
    void Update()
    {

           


        //Contador luego del disparo
        if (counterStart == true)
        {
            counterShoot += Time.deltaTime;
            
        }

        //recarga la bala despues de un tiempo sin disparar
        if (3 - counterShoot <= 0 && shootsMax <= 0)
        {
            counterShoot = 0;
            available = false;            
            Invoke("ReloadFull", (5f / 3f) * 3 / attackSpeed);
            counterStart = false;
            
        }
        else if (2 - counterShoot <= 0 && shootsMax <= 1 && shootsMax > 0)
        {
            counterShoot = 0;
            available = false;
            Invoke("ReloadTwo", (5f / 3f) * 2 / attackSpeed);
            counterStart = false;

        }else if(1 - counterShoot <= 0 && shootsMax <= 2 && shootsMax > 1)
        {
            counterShoot = 0;
            available = false;
            Invoke("ReloadOne", (5f / 3f) / attackSpeed);
            counterStart = false;

        }
    }


    // Método para disparar una bala
    public void Shot()
    {
        counterStart = true;
        if (available)
        {
            shootsMax = shootsMax - 1;

            //vfxs smokefire
            if (smokeFire)
                smokeFire.Play();
            available = false;

            Vector3 force = -transform.up * this.force;
            GameObject clone = cartrigde.GetItem(reference.position, Vector3.zero, tag);
            //clone.transform.LookAt(clone.transform.position + transform.forward);
            clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

            Invoke("ReloadingAmmo", 1f / attackSpeed);
            ChangeSlider();

        }
    }
    //Recarga el cargador entero
    void ReloadFull()
    {
        shootsMax = 3;
        available = true;
        reload.value = 1;

    }
    void ReloadOne()
    {
        shootsMax += 1;
        available = true;
        reload.value += 0.33f;

    }
    void ReloadTwo()
    {
        shootsMax += 2;
        available = true;
        reload.value += 0.66f;

    }

    //tiempo por disparo
    void ReloadingAmmo()
    {
        available = true;

    }
    // Método para aumentar la cadencia de disparo
    public void GainShootSpeed(float porcent)
    {
        attackSpeed = attackSpeed + attackSpeed * porcent / 100;
    }

    void ChangeSlider()
    {
        if (fill)
        {
            //fill.color = new Color(fill.color.r, fill.color.g, fill.color.b, 1);     
            if (shootsMax == 0)
            {

                reload.value = 0f;
            }
            if (shootsMax == 1)
            {
                reload.value = 0.33f;
               
            }
            if (shootsMax == 2)
            {
                reload.value = 0.66f;
            }
            if (shootsMax == 3)
            {
                reload.value = 1f;
            }
        }
        
    }
}
