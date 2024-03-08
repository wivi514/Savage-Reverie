using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorManager : MonoBehaviour
{
    [SerializeField] public GameObject StatIndicator;
    [SerializeField] public GameObject InventoryIndicator;
    [SerializeField] public GameObject JournalIndicator;
    [SerializeField] public GameObject MapIndicator;

    // D�sactiver tous les indicateurs
    private void DeactivateAllIndicators()
    {
        StatIndicator.SetActive(false);
        InventoryIndicator.SetActive(false);
        JournalIndicator.SetActive(false);
        MapIndicator.SetActive(false);
    }

    // Activer l'indicateur pour le menu "Weapons"
    public void ActivateStatIndicator()
    {
        DeactivateAllIndicators();
        StatIndicator.SetActive(true);
    }

    // Activer l'indicateur pour le menu "Gears"
    public void ActivateInventoryIndicator()
    {
        DeactivateAllIndicators();
        InventoryIndicator.SetActive(true);
    }

    // Activer l'indicateur pour le menu "Consumables"
    public void ActivateJournalIndicator()
    {
        DeactivateAllIndicators();
        JournalIndicator.SetActive(true);
    }

    // Activer l'indicateur pour le menu "Misc"
    public void ActivateMapIndicator()
    {
        DeactivateAllIndicators();
        MapIndicator.SetActive(true);
    }
}
