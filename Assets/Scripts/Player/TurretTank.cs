using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTank : MonoBehaviour
{
    [Header("Shoot Properties")]
    [SerializeField] float force = 10, attackSpeed = 0.5f;
    [SerializeField] Transform reference;
    [SerializeField] ParticleSystem smokeFire;

    bool available = true;
    Pool cartrigde;


    private void Start()
    {
        cartrigde = GameObject.Find("Cartrigde (Pool)").GetComponent<Pool>();
    }

    public void Shot()
    {
        if (available)
        {
            if (smokeFire)
                smokeFire.Play();
            available = false;
            Invoke("Reload", 1f / attackSpeed);

            Vector3 force = -transform.up * this.force;

            GameObject clone = cartrigde.GetItem(reference.position, tag);
            //clone.transform.LookAt(clone.transform.position + transform.forward);
            clone.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

    void Reload()
    {
        available = true;
    }
}
