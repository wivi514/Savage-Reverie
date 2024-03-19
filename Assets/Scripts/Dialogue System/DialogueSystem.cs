using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

}

[System.Serializable]
public class DialogueOption
{
    public string Text;
    public UnityEvent OnSelect; // Events to trigger on selection
}

[System.Serializable]
public class Dialogue
{
    public string CharacterLine;
    public List<DialogueOption> Responses;
}

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI; // Your dialogue panel
    public TMP_Text characterLineText; // Text element for the character's line
    public GameObject responseButtonPrefab; // A prefab for the response buttons
    public Transform responseButtonContainer; // Container to hold response buttons

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
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        Dialogue currentDialogue = dialogueQueue.Dequeue();
        characterLineText.text = currentDialogue.CharacterLine;

        // Clear old responses
        foreach (Transform child in responseButtonContainer)
        {
            Destroy(child.gameObject);
        }

        // Create response buttons
        foreach (var option in currentDialogue.Responses)
        {
            GameObject button = Instantiate(responseButtonPrefab, responseButtonContainer);
            button.GetComponentInChildren<TMP_Text>().text = option.Text;
            button.GetComponent<Button>().onClick.AddListener(() => OnResponseSelected(option));
        }
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

