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
    [SerializeField, Range(0, 3)] int[] medalsLVLs;
    [SerializeField] int[] scoreLVLs;

    SaveData data;


    public SaveData Data
    {
        get
        {
            data = new SaveData(medalsLVLs, scoreLVLs);

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
            scoreLVLs = data.ScoreLVL;

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
        int[] score = new int[levelsCant];

        for (int i = 0; i < levelsCant; i++)
        {
            if (i < medalsLVLs.Length)
            {
                medals[i] = medalsLVLs[i];
                score[i] = scoreLVLs[i];
            }
            else
            {
                medals[i] = 0;
                score[i] = 0;
            }
        }

        medalsLVLs = new int[levelsCant];
        scoreLVLs = new int[levelsCant];

        for (int i = 0; i < levelsCant; i++)
        {
            medalsLVLs[i] = medals[i];
            scoreLVLs[i] = score[i];
        }
    }

    public void AssignMedals(int nivel, int medals)
    {
        medalsLVLs[nivel - 1] = medals > medalsLVLs[nivel - 1] ? medals : medalsLVLs[nivel - 1];
    }

    public void AssignScore(int nivel, int score)
    {
        scoreLVLs[nivel - 1] = score > scoreLVLs[nivel - 1] ? score : scoreLVLs[nivel - 1];
    }

    public void Reset()
    {
        for (int i = 0; i < levelsCant; i++)
        {
            medalsLVLs[i] = 0;
            scoreLVLs[i] = 0;
        }

        SaveAndLoad.Save("LevelData", Data);
    }

    public int[] MedalsLVLs { get => medalsLVLs; set => medalsLVLs = value; }
    public int[] ScoreLVLs { get => scoreLVLs; set => scoreLVLs = value; }
}


[Serializable]
public class SaveData
{
    int[] medalsLVL;
    int[] scoreLVL;

    public SaveData(int[] medalsLVL, int[] scoreLVL)
    {
        int length = medalsLVL.Length;
        this.medalsLVL = new int[length];
        this.scoreLVL = new int[length];

        for (int i = 0; i < length; i++)
        {
            this.medalsLVL[i] = medalsLVL[i];
            this.scoreLVL[i] = scoreLVL[i];
        }
    }

    public int[] MedalsLVL { get => medalsLVL; set => medalsLVL = value; }
    public int[] ScoreLVL { get => scoreLVL; set => scoreLVL = value; }
}