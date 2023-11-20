using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : SingletonService<AudioSystem>
{
    [Header("Data Setting")]
    public SoundData soundData;
    
    private AudioSource audioSource;
    private Vector3 defaultPosition;
    

    public void PlaySound(string soundID)
    {
        SoundData.SoundEntry soundEntry = FindSoundEntry(soundID);

        if (soundEntry != null)
        {
            audioSource.transform.position = defaultPosition;
            audioSource.PlayOneShot(soundEntry.audioClip);
        }
        else
        {
            Debug.LogError("Invalid sound ID: " + soundID);
        }
    }
    
    public void PlaySound(string soundID , Vector3 targetPosition)
    {
        SoundData.SoundEntry soundEntry = FindSoundEntry(soundID);

        if (soundEntry != null)
        {
            audioSource.transform.position = targetPosition;
            audioSource.PlayOneShot(soundEntry.audioClip);
        }
        else
        {
            Debug.LogError("Invalid sound ID: " + soundID);
        }
    }

    private SoundData.SoundEntry FindSoundEntry(string soundID)
    {
        if (soundData != null && soundData.soundEntries != null)
        {
            foreach (SoundData.SoundEntry entry in soundData.soundEntries)
            {
                if (entry.id == soundID)
                {
                    return entry;
                }
            }
        }

        return null;
    }
}
