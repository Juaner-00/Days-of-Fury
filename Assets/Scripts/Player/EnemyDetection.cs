using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField]
    float zoneDetection;
    [SerializeField]
    int numeroTanks;
    public LayerMask targetMask;
    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
 
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        visibleTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, zoneDetection, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float dstToTarget = Vector3.Distance(transform.position, target.position);


            visibleTargets.Add(target);
                  
               
                

            
        }

        if (targetsInViewRadius.Length >= numeroTanks)
        {
            CamaraManager.Instance.ZoomCam();

        }
        else if(targetsInViewRadius.Length<numeroTanks)
        {
            CamaraManager.Instance.EndZoom();
        }

    }

    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, zoneDetection);
       
    }
}
