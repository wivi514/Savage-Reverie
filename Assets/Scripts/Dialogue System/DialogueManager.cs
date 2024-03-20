using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class DialogueOption
{
    public string Text;
    public UnityEvent OnSelect; // Events to trigger on selection
}

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI; // Your dialogue panel
    public TMP_Text characterLineText; // Text element for the character's line
    public GameObject responseButtonPrefab; // A prefab for the response buttons
    public Transform responseButtonContainer; // Container to hold response buttons

    private Dialogue currentDialogue;
    private int currentLineIndex = 0; // To keep track of the current line in the dialogue

    private Queue<Dialogue> dialogueQueue = new Queue<Dialogue>();

    // Call this to start a dialogue
    public void StartDialogue(List<Dialogue> dialogues)
    {
        dialogueQueue.Clear();
        foreach (var dialogue in dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }
        ShowNext();
    }

    // Call this to show the next dialogue in the queue
    private void ShowNext()
    {
        if (dialogueQueue.Count == 0 && currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        if (currentLineIndex < currentDialogue.dialogueLines.Count)
        {
            // Display the current line of dialogue
            characterLineText.text = currentDialogue.dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            // Once all lines are displayed, show responses
            foreach (Transform child in responseButtonContainer)
            {
                Destroy(child.gameObject);
            }

            // Create response buttons
            foreach (var option in currentDialogue.responses)
            {
                GameObject button = Instantiate(responseButtonPrefab, responseButtonContainer);
                button.GetComponentInChildren<TMP_Text>().text = option.Text;
                button.GetComponent<Button>().onClick.AddListener(() => OnResponseSelected(option));
            }

            // Reset for the next dialogue in the queue
            if (dialogueQueue.Count > 0)
            {
                currentDialogue = dialogueQueue.Dequeue();
                currentLineIndex = 0;
            }
        }
    }

    public void StartDialogue(DialogueScriptableObject dialogueSO)
    {
        dialogueQueue.Clear();
        foreach (var dialogue in dialogueSO.dialogues)
        {
            dialogueQueue.Enqueue(dialogue);
        }
        ShowNext();
    }

    private void OnResponseSelected(DialogueOption option)
    {
        option.OnSelect.Invoke();
        ShowNext();
    }

    private void EndDialogue()
    {
        dialogueUI.SetActive(false);
        // Re-enable player control if disabled
    }
}
