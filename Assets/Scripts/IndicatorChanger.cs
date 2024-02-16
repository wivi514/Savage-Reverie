using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorChanger : MonoBehaviour
{
    // Ces couleurs peuvent �tre d�finies dans l'�diteur Unity
    [SerializeField] private Color activeTabColor;
    [SerializeField] private Color inactiveTabColor;
    [SerializeField] private GameObject[] panels; // Assignez tous vos panneaux ici dans l'ordre des onglets

    // Appel�e par les boutons pour changer l'onglet
    public void SelectTab(int tabIndex)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == tabIndex)
            {
                panels[i].GetComponent<Image>().color = activeTabColor; // Changer la couleur pour l'onglet actif
            }
            else
            {
                panels[i].GetComponent<Image>().color = inactiveTabColor; // R�initialiser la couleur pour les autres onglets
            }
        }
    }
}
