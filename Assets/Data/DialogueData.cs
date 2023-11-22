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
    public string context;
    public float waitTime;
    public AudioEffect audioEffect;
    public AudioClip audioClip;
    public Speaker currentSpeaker = Speaker.Narration;
}

public enum Speaker
{
    Sword,
    Rifle,
    Merchant,
    Missionary,
    Player,
    Narration
}

public enum AudioEffect
{
    defaults,
    surprise,
    disappointment,
    happy
}

