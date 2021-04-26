using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelsMedals : MonoBehaviour
{
    [SerializeField]
    Transform[] levelsMedalsContainer;

    [SerializeField]
    TextMeshProUGUI[] levelsScore;

    GameObject[] medalsLevels;

    private void Start()
    {
        // Obtener el objeto que guarda la cantidad de medallas y score en cada nivel
        DataObject levelsData = GameManager.Instance.DataObject;

        // Activar la cantidad de medallas que tenga en cada nivel
        for (int i = 0; i < levelsMedalsContainer.Length; i++)
            for (int j = 0; j < levelsData.MedalsLVLs[i]; j++)
                levelsMedalsContainer[i].transform.GetChild(j).gameObject.SetActive(true);

        // Poner el score de cada nivel
        for (int i = 0; i < levelsScore.Length; i++)
            levelsScore[i].text = levelsData.ScoreLVLs[i].ToString();
    }
}
