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
    public GameObject dialogueBackgroundPanel;

    private List<DialogueOption> responses;

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
            dialogueBackgroundPanel.SetActive(true);
            currentDialogue = dialogueScriptableObject.dialogues[0];
            responses = currentDialogue.responses;
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
            Text responseText = responseObj.GetComponentInChildren<Text>(); // Note the change here from TMP_Text to Text
            if (responseText != null)
            {
                responseText.text = responses[i].text;
                responseText.color = (i == selectedResponseIndex) ? selectedResponseColor : defaultResponseColor;
            }
            else
            {
                Debug.LogError("Text component not found on instantiated responseObj.");
            }
        }
    }


    public void ChangeResponseSelection(float direction)
    {
        if (direction < 0) selectedResponseIndex = Mathf.Min(selectedResponseIndex + 1, currentDialogue.responses.Count - 1);
        else if (direction > 0) selectedResponseIndex = Mathf.Max(selectedResponseIndex - 1, 0);
        UpdateResponseColors();
    }

    void UpdateResponseColors()
    {
        if (responseContainer == null)
        {
            Debug.LogError("responseContainer is null.");
            return;
        }

        if (responses == null)
        {
            Debug.LogError("responses list is null.");
            return;
        }

        for (int i = 0; i < responseContainer.childCount; i++)
        {
            if (i >= responses.Count)
            {
                Debug.LogError($"No response available for child index {i}.");
                continue;
            }

            Transform responseTransform = responseContainer.GetChild(i);
            if (responseTransform == null)
            {
                Debug.LogError($"Failed to get child at index {i} from responseContainer.");
                continue;
            }

            Text responseText = responseTransform.GetComponentInChildren<Text>();
            if (responseText == null)
            {
                Debug.LogError("Text component not found on response object.");
                continue;
            }

            // Use the existing response list to get the color for the selected response
            responseText.color = (i == selectedResponseIndex) ? selectedResponseColor : defaultResponseColor;
        }
    }

    public void ConfirmResponse()
    {
        DialogueOption selectedResponse = currentDialogue.responses[selectedResponseIndex];
        selectedResponse.onSelect.Invoke();
    }

    public void ClearContainerUI()
    {
        foreach (Transform child in responseContainer)
        {
            Destroy(child.gameObject);
            dialogueText.text = " ";
            dialogueBackgroundPanel.SetActive(false);
        }
    }
}
