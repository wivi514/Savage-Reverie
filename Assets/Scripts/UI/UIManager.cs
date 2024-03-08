using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public GameObject uiPanelPrefab; // Assign your UI panel prefab
    public Transform contentPanel; // Assign the content panel inside your ScrollRect

    private void Awake()
    {
        // Singleton pattern to ensure only one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateContainerUI(string[] itemNames)
    {
        ClearContainerUI();
        foreach (string itemName in itemNames)
        {
            GameObject itemUI = Instantiate(uiPanelPrefab, contentPanel);
            itemUI.GetComponentInChildren<Text>().text = itemName; // Ensure your prefab has a Text child
        }
    }

    private void ClearContainerUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
