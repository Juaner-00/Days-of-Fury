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
    [SerializeField] bool playedOnce;

    SaveData data;

    public SaveData Data
    {
        get
        {
            data = new SaveData(medalsLVLs, scoreLVLs, playedOnce);

            // Para ver en consola
            ///---------------------------------------------
            string[] test = new string[data.MedalsLVL.Length];
            for (int i = 0; i < test.Length; i++)
                test[i] = data.MedalsLVL[i].ToString();

            Debug.Log("Get Medals " + string.Join(", ", test));

            for (int i = 0; i < test.Length; i++)
                test[i] = data.ScoreLVL[i].ToString();

            Debug.Log("Get Score " + string.Join(", ", test));
            ///---------------------------------------------

            return data;
        }
        set
        {
            data = value;
            medalsLVLs = data.MedalsLVL;
            scoreLVLs = data.ScoreLVL;
            playedOnce = data.PlayedOnce;

            // Para ver en consola
            ///---------------------------------------------
            string[] test = new string[data.MedalsLVL.Length];
            for (int i = 0; i < test.Length; i++)
                test[i] = data.MedalsLVL[i].ToString();

            Debug.Log("Set Medals " + string.Join(", ", test));

            for (int i = 0; i < test.Length; i++)
                test[i] = data.ScoreLVL[i].ToString();

            Debug.Log("Set Score " + string.Join(", ", test));
            ///---------------------------------------------

        }
    }

    private void OnValidate()
    {
        levelsCant = levelsCant < 0 ? 0 : levelsCant;

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

    public void SetPlayed()
    {
        playedOnce = true;
        Save();
    }

    public void Reset()
    {
        for (int i = 0; i < levelsCant; i++)
        {
            medalsLVLs[i] = 0;
            scoreLVLs[i] = 0;
        }

        playedOnce = false;
        Save();
    }

    public void Save()
    {
        SaveAndLoad.Save("LevelData", Data);
    }


    public int[] MedalsLVLs { get => medalsLVLs; set => medalsLVLs = value; }
    public int[] ScoreLVLs { get => scoreLVLs; set => scoreLVLs = value; }
    public bool PlayedOnce { get => playedOnce; }
}


[Serializable]
public class SaveData
{
    public int[] medalsLVL;
    public int[] scoreLVL;
    public bool playedOnce;

    public SaveData()
    {

    }

    public SaveData(int[] medalsLVL, int[] scoreLVL, bool playedOnce)
    {
        int length = medalsLVL.Length;
        this.medalsLVL = new int[length];
        this.scoreLVL = new int[length];
        this.playedOnce = playedOnce;

        for (int i = 0; i < length; i++)
        {
            this.medalsLVL[i] = medalsLVL[i];
            this.scoreLVL[i] = scoreLVL[i];
        }
    }

    public int[] MedalsLVL { get => medalsLVL; set => medalsLVL = value; }
    public int[] ScoreLVL { get => scoreLVL; set => scoreLVL = value; }
    public bool PlayedOnce { get => playedOnce; }
}
