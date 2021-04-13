using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Player)
        {
            Vector3 newPos = new Vector3(GameManager.Player.transform.position.x, transform.position.y, GameManager.Player.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, newPos, 1);
        }
    }
}
