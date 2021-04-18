using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataObject))]
public class LevelsEditor : Editor
{
    DataObject lvlObj;


    private void OnEnable()
    {
        lvlObj = target as DataObject;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset Levels"))
            lvlObj.ResetLevels();
    }

}