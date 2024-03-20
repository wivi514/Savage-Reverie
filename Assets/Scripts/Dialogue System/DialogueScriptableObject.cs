using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/New Dialogue")]
public class DialogueScriptableObject : ScriptableObject
{
    public List<Dialogue> dialogues; // List of all dialogues for this character
}

[System.Serializable]
public class Dialogue
{
    public string id; // Unique identifier for the dialogue, useful for branching
    public string characterName; // Name of the character speaking
    public List<string> dialogueLines; // Lines of dialogue for the character
    public List<DialogueOption> responses; // Possible player responses
}