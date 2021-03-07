using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Player)
            transform.position = Vector3.MoveTowards(transform.position, GameManager.Player.transform.position, 1);
    }
}
