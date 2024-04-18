using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/New Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public List<Dialogue> dialogues; // List of all dialogues for this character

    // Call this method to get a specific dialogue by ID.
    public Dialogue GetDialogue(string id)
    {
        return dialogues.Find(d => d.id == id);
    }
}

[System.Serializable]
public class Dialogue
{
    public string id; // Unique identifier for the dialogue
    public string characterName; // Name of the NPC speaking
    public List<string> dialogueLines; // Lines of dialogue for the NPC
    public List<DialogueOption> responses; // Possible player responses
}

[System.Serializable]
public class DialogueOption
{
    public string text; // Text for the response option
    public UnityEvent onSelect; // Event to invoke when this option is selected
}
