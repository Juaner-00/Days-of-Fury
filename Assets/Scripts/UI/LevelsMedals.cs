using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelsMedals : MonoBehaviour
{
    [SerializeField, Tooltip("El tamaño es la cantidad de nivels - 1 porque cada número es referencia el nivel anterior")]
    int[] medalsToUnlock;

    [SerializeField]
    Button[] levelsButtons;

    [SerializeField]
    Transform[] levelsMedalsContainer;

    [SerializeField]
    TextMeshProUGUI[] levelsScore;

    GameObject[] medalsLevels;

    private void Awake()
    {
        // Obtener el objeto que guarda la cantidad de medallas y score en cada nivel
        DataObject levelsData = GameManager.Instance.DataObject;

        // Desactivar el botón y el score de los niveles, excepto el primero
        for (int i = 1; i < levelsButtons.Length; i++)
        {
            levelsButtons[i].interactable = false;
            levelsButtons[i].transform.GetChild(2).gameObject.SetActive(false);
        }

        // Activar el botón y el score del nivel dependiendo de la cantidad de medallas
        for (int i = 1; i < levelsButtons.Length; i++)
            if (levelsData.MedalsLVLs[i - 1] >= medalsToUnlock[i - 1])
            {
                levelsButtons[i].interactable = true;
                levelsButtons[i].transform.GetChild(2).gameObject.SetActive(true);
            }

        // Activar la cantidad de medallas que tenga en cada nivel
        for (int i = 0; i < levelsMedalsContainer.Length; i++)
            if (i < levelsData.MedalsLVLs.Length)
                for (int j = 0; j < levelsData.MedalsLVLs[i]; j++)
                    levelsMedalsContainer[i].transform.GetChild(j).gameObject.SetActive(true);

        // Poner el score de cada nivel
        for (int i = 0; i < levelsScore.Length; i++)
            if (i < levelsData.ScoreLVLs.Length)
                levelsScore[i].text = levelsData.ScoreLVLs[i].ToString();
    }
}
