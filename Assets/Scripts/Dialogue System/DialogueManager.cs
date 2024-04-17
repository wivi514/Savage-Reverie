using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public Transform responseContainer;
    public GameObject responsePrefab;

    public Color defaultResponseColor = Color.white;
    public Color selectedResponseColor = new Color(1f, 0.51f, 0f);

    private Dialogue currentDialogue;
    private int selectedResponseIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(DialogueScriptableObject dialogueScriptableObject)
    {
        if (dialogueScriptableObject.dialogues.Count > 0)
        {
            currentDialogue = dialogueScriptableObject.dialogues[0];
            dialogueText.text = currentDialogue.dialogueLines[0]; // Show the first line.
            PopulateResponses(currentDialogue.responses); // Show the responses.
            dialoguePanel.SetActive(true);
        }
    }

    void PopulateResponses(List<DialogueOption> responses)
    {
        foreach (Transform child in responseContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < responses.Count; i++)
        {
            GameObject responseObj = Instantiate(responsePrefab, responseContainer);
            TMP_Text responseText = responseObj.GetComponentInChildren<TMP_Text>();
            responseText.text = responses[i].text;
            responseText.color = (i == selectedResponseIndex) ? selectedResponseColor : defaultResponseColor;
        }
    }

    public void ChangeResponseSelection(int direction)
    {
        selectedResponseIndex += direction;
        selectedResponseIndex = Mathf.Clamp(selectedResponseIndex, 0, currentDialogue.responses.Count - 1); // Use 'Count'
        UpdateResponseColors();
    }

    void UpdateResponseColors()
    {
        for (int i = 0; i < responseContainer.childCount; i++)
        {
            TMP_Text responseText = responseContainer.GetChild(i).GetComponentInChildren<TMP_Text>();
            responseText.color = (i == selectedResponseIndex) ? selectedResponseColor : defaultResponseColor;
        }
    }

    public void ConfirmResponse()
    {
        DialogueOption selectedResponse = currentDialogue.responses[selectedResponseIndex];
        selectedResponse.onSelect.Invoke();
    }

    // ... Other methods as needed ...
}
