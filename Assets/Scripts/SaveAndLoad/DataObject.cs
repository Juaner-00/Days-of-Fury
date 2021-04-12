using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData")]
public class DataObject : ScriptableObject
{
    [SerializeField] int levelsCant;
    [SerializeField, Range(0, 3)] public int[] medalsLVLs;

    SaveData data;


    public SaveData Data
    {
        get
        {
            data = new SaveData(medalsLVLs);

            string[] test = new string[data.MedalsLVL.Length];
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = data.MedalsLVL[i].ToString();
            }

            Debug.Log(string.Join(", ", test));
            return data;
        }
        set
        {
            data = value;
            medalsLVLs = data.MedalsLVL;

            string[] test = new string[data.MedalsLVL.Length];
            for (int i = 0; i < test.Length; i++)
            {
                test[i] = data.MedalsLVL[i].ToString();
            }

            Debug.Log(string.Join(", ", test));
        }
    }

    private void OnValidate()
    {
        int[] medals = new int[levelsCant];

        for (int i = 0; i < levelsCant; i++)
        {
            if (i < medalsLVLs.Length)
                medals[i] = medalsLVLs[i];
            else
                medals[i] = 0;
        }

        medalsLVLs = new int[levelsCant];

        for (int i = 0; i < levelsCant; i++)
            medalsLVLs[i] = medals[i];
    }

    public void AssignMedals(int nivel, int medals)
    {
        if (medals > medalsLVLs[nivel - 1])
            medalsLVLs[nivel - 1] = medals;
    }

    public void ResetLevels()
    {
        for (int i = 0; i < levelsCant; i++)
            medalsLVLs[i] = 0;

        SaveAndLoad.Save("LevelData", Data);
    }

    public int[] MedalsLVLs { get => medalsLVLs; set => medalsLVLs = value; }
}


[Serializable]
public class SaveData
{
    int[] medalsLVL;

    public SaveData(int[] medalsLVL)
    {
        int length = medalsLVL.Length;
        this.medalsLVL = new int[length];

        for (int i = 0; i < length; i++)
            this.medalsLVL[i] = medalsLVL[i];
    }

    public int[] MedalsLVL { get => medalsLVL; set => medalsLVL = value; }
}