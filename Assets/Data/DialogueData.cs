using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue System/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public List<DialogueEntry> dialogueEntries = new List<DialogueEntry>();
}

[System.Serializable]
public class DialogueEntry
{
    public string speaker;
    public string context;
    public float waitTime;
    public AudioClip audioClip;
}