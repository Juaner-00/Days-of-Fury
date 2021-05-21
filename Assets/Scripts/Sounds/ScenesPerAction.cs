using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ScenesPerAction
{
    [SerializeField] string actionName;
    [SerializeField] bool playRandomClip;
    [SerializeField] List<string> scenesNames;

    public string ActionName { get => actionName; set => actionName = value; }
    public bool PlayRandomClip { get => playRandomClip; set => playRandomClip = value; }
    public List<string> SceneNames { get => scenesNames; set => scenesNames = value; }
}
