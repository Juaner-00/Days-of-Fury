using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ActionClips
{
    [SerializeField] string actionName;
    [SerializeField] [Range(0f,1f)] float actionVolume;
    [SerializeField] bool isOverlappable;
    [SerializeField] AudioClip[] clips;

    public string ActionName { get => actionName; }
    public float ActionVolume { get => actionVolume; }
    public bool IsOverlappable { get => isOverlappable; }
    public AudioClip[] Clips { get => clips; }
}
