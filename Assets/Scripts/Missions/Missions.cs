using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mision", menuName = "Mision")]
public class Missions : ScriptableObject
{
    [SerializeField] public string names;
    [SerializeField] public string description;

    [SerializeField] public int objetive;

    public enum Opcion
    {
        Enemys, Pickups, Walls, Score, Move, Objetive,Tower,
    }
    public Opcion opcion = Opcion.Score;
}
