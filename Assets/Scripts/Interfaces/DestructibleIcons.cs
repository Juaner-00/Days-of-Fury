using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DestructibleIcons : MonoBehaviour
{
    [SerializeField] DestructibleWall wall;

    private void OnEnable()
    {
        wall.OnWallDestroyed += DesactivateGM;
    }

    private void OnDisable()
    {
        wall.OnWallDestroyed -= DesactivateGM;
    }

    void DesactivateGM()
    {
        gameObject.SetActive(false);
    }

}


