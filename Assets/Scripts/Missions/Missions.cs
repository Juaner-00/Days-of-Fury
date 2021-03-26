using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mision", menuName = "Mision")]
public class Missions : ScriptableObject
{
    public string names;
    public string description;

    public int objetive;
    public int actual;

    public bool itsActive = false;

    public enum Opcion
    {
        Enemys, Pickups, Walls, Score,
    }
    public Opcion opcion = Opcion.Score;   

}
