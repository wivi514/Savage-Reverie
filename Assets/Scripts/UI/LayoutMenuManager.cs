using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayoutMenuManager : MonoBehaviour
{
    public GameObject statMenuContent;
    public GameObject inventoryMenuContent;
    public GameObject journalMenuContent;
    public GameObject mapMenuContent;
    public GameObject questMenuContent;

    private void DesactivateAllMenu()
    {
        statMenuContent.SetActive(false);
        inventoryMenuContent.SetActive(false);
        journalMenuContent.SetActive(false);
        mapMenuContent.SetActive(false);
        questMenuContent.SetActive(false);
    }

    public void ShowStatMenu()
    {
        DesactivateAllMenu();
        statMenuContent.SetActive(true);
    }

    public void ShowInventoryMenu()
    {
        DesactivateAllMenu();
        inventoryMenuContent.SetActive(true);
    }

    public void ShowJournalMenu()
    {
        DesactivateAllMenu();
        journalMenuContent.SetActive(true);
    }

    public void ShowMapMenu()
    {
        DesactivateAllMenu();
        mapMenuContent.SetActive(true);
    }

    public void ShowQuestMenu()
    {
        DesactivateAllMenu();
        questMenuContent.SetActive(true);
    }
}
