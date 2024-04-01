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
        ClearContainerUI(); // Clear previous UI elements
        foreach (string itemName in itemNames)
        {
            GameObject itemUI = Instantiate(uiPanelPrefab, contentPanel);
            // Ensure the prefab instantiated is the item entry prefab, not the inventory panel itself
            itemUI.GetComponentInChildren<Text>().text = itemName;
        }
    }

    public void ClearContainerUI()
    {
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
    }
}
