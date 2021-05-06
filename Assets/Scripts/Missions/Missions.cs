using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mision", menuName = "Mision")]
public class Missions : ScriptableObject
{
    [SerializeField] public string names;
    [SerializeField] public string description;

    [SerializeField] public int objetive;
    [SerializeField] public int numObjetive;

    public enum Opcion
    {
        Enemys, Pickups, Walls, Score, Move, Objetive,Tower,PickupAll, PickUpEsp, Normal, Strong
    }
    public Opcion opcion = Opcion.Score;
    [SerializeField] public PickUpType TypePickup;
}
