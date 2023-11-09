using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueSystem : SingletonService<DialogueSystem>
{
    [Header("Component Setting")]
    [Space]
    public DialogueData dialogueData;
    public AudioSource speakerAudio;
    public CanvasGroup canvasGroup;
    public Text dialogueText;
    
    [Header("Value Setting")]
    [Space]
    [SerializeField] private int currentDialogueIndex = 0;
    [SerializeField] private bool isDisplayingText = false;

    void Start()
    {
        TryGetComponent<CanvasGroup>(out canvasGroup);
        
        if (dialogueData == null)
            Debug.LogError(this.name + "Missing Component : " + dialogueData.GetType().ToString());
        if (speakerAudio == null)
            Debug.LogError(this.name + "Missing Component : " + speakerAudio.GetType().ToString());
        if (dialogueText == null) 
            Debug.LogError(this.name + "Missing Component : " + dialogueText.GetType().ToString());
        if (canvasGroup == null) 
            Debug.LogError(this.name + "Missing Component : " + canvasGroup.GetType().ToString());
        
        currentDialogueIndex = 0;
        //DisplayNextDialogue(); 
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isDisplayingText && dialogueData != null)
        {
            if (currentDialogueIndex < dialogueData.dialogueEntries.Count)
            {
                DisplayNextDialogue();
                currentDialogueIndex++;
            }
            else
            {
                // 对话结束
                // 在这里添加你的对话结束逻辑
                OnDialogComplete();
            }
        }
    }

    public void Send(DialogueData data)
    {
        dialogueData = data;
        
        DisplayNextDialogue();
    }
    
    public void Send(string dataName)
    {
        var path = "DialogData/" + dataName;
        var data = Resources.Load<DialogueData>(path);

        if (data != null)
        {
            dialogueData = data;
        }
        
        DisplayNextDialogue();
    }

    private void OnDialogComplete()
    {
        currentDialogueIndex = 0;
        dialogueData = null;
        canvasGroup.alpha = 0;
    }

    private void DisplayNextDialogue()
    {
        canvasGroup.alpha = 1;
        
        if (currentDialogueIndex < dialogueData.dialogueEntries.Count)
        {
            DialogueEntry currentEntry = dialogueData.dialogueEntries[currentDialogueIndex];
            string fullText = currentEntry.speaker + ": " +
                              currentEntry.context + "\n";

            StartCoroutine(ShowText(fullText));

            if (currentEntry.audioClip != null)
            {
                PlayAudio(currentEntry.audioClip);
            }
        }
    }

    private IEnumerator ShowText(string text)
    {
        isDisplayingText = true;
        dialogueText.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            dialogueText.text += text[i];
            yield return new WaitForSeconds(0.05f); // 控制文本逐字显示速度
        }
        isDisplayingText = false;
    }

    private void PlayAudio(AudioClip audioClip)
    {
        if (speakerAudio != null)
        {
            speakerAudio.clip = audioClip;
            speakerAudio.Play();
        }
    }
}
