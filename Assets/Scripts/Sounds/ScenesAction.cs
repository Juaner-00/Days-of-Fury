using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public struct ScenesAction 
{
    [SerializeField] string actionName;
    [SerializeField] bool playRandomClip;
    [SerializeField] List<string> scenesNames;

    public string ActionName { get => actionName; set => actionName = value; }
    public bool PlayRandomClip { get => playRandomClip; set => playRandomClip = value; }
    public List<string> SceneNames { get => scenesNames; set => scenesNames = value; }
}
