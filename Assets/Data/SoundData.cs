using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Audio/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    [System.Serializable]
    public class SoundEntry
    {
        public string id;
        public AudioClip audioClip;
    }

    public SoundEntry[] soundEntries;
}