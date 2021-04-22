using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTank : MonoBehaviour
{
    [Header("Shoot Properties")]
    [SerializeField] protected float force = 10, attackSpeedBase = 0.5f;
    [SerializeField] protected Transform reference;
    [SerializeField] protected ParticleSystem smokeFire;

    protected bool available = true;
    protected Pool cartrigde;



    [SerializeField, Header("Debug")]
    float attackSpeed;


    protected virtual void Start()
    {
        cartrigde = GameObject.Find("Cartrigde (Pool)").GetComponent<Pool>();

        attackSpeed = attackSpeedBase;
    }

    // Método para disparar una bala
    public virtual bool Shot()
    {
        if (available)
        {
            if (smokeFire)
                smokeFire.Play();
            available = false;
            Invoke("Reload", 1f / attackSpeed);

            Vector3 force = -transform.up * this.force;

            GameObject clone = cartrigde.GetItem(reference.position, Vector3.zero, tag);
            clone.transform.LookAt(clone.transform.position - transform.up);
            clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            return true;
        }
        return false;
    }

    void Reload()
    {
        available = true;
    }

    // Método para aumentar la cadencia de disparo
    public void GainShootSpeed(float porcent)
    {
        attackSpeed = attackSpeed + attackSpeed * porcent / 100;
    }

    public bool Available { get => available; }
    public float AttackSpeed { get => attackSpeed;}
}
