using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueSystem : SingletonService<DialogueSystem>
{
    [Header("Component Setting")]
    [Space]
    public DialogueData dialogueData;
    public DialogueCharacterPack characterPack;
    public AudioSource speakerAudio;
    public CanvasGroup canvasGroup;
    
    public Text speakerText;
    public Text dialogueText;

    public Image speakerImage;

    [Header("Value Setting")] [Space] 
    [SerializeField] private bool facingPlayer;
    [SerializeField] private bool isDisplayingText = false;
    [SerializeField] private int currentDialogueIndex = 0;

    private Transform playerTransform;
    private Action onDialogEnd;

    void Start()
    {
        TryGetComponent<CanvasGroup>(out canvasGroup);
        
        if (speakerAudio == null)
            Debug.LogError(this.name + "Missing Component : " + speakerAudio.GetType().ToString());
        if (dialogueText == null) 
            Debug.LogError(this.name + "Missing Component : " + dialogueText.GetType().ToString());
        if (speakerText == null) 
            Debug.LogError(this.name + "Missing Component : " + dialogueText.GetType().ToString());
        if (canvasGroup == null) 
            Debug.LogError(this.name + "Missing Component : " + canvasGroup.GetType().ToString());
        if (speakerImage == null) 
            Debug.LogError(this.name + "Missing Component : " + speakerImage.GetType().ToString());
        if (characterPack == null)
            characterPack = Resources.Load<DialogueCharacterPack>("CharacterSpritePack/CharacterSpriteData");
        
        currentDialogueIndex = 0;
        playerTransform = FindObjectOfType<XROrigin>().transform;
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
                // 在这里添加对话结束逻辑
                OnDialogComplete();
            }
        }

        if (facingPlayer)
        {
            transform.localScale = new Vector3(-Math.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.LookAt(new Vector3(playerTransform.position.x , transform.position.y , playerTransform.position.z));
        }
    }

    [ContextMenu("Send Test")]
    public void SendText()
    {
        DialogueSystem.instance.Send("Test_01" , delegate { Debug.Log("The End"); });
    }

    public void ResetPosition(float duration , Vector3 worldSpace)
    {
        transform.DOMove(worldSpace, duration);
    }

    public void Send(DialogueData data , Action action)
    {
        dialogueData = data;
        onDialogEnd = action;
        
        DisplayNextDialogue();
    }
    
    public void Send(string dataName , Action action)
    {
        var path = "DialogData/" + dataName;
        var data = Resources.Load<DialogueData>(path);

        if (data != null)
        {
            onDialogEnd = action;
            dialogueData = data;
        }
        
        DisplayNextDialogue();
    }

    private void OnDialogComplete()
    {
        onDialogEnd?.Invoke();
        
        onDialogEnd = null;
        dialogueData = null;
        currentDialogueIndex = 0;
        canvasGroup.alpha = 0;
    }

    private void DisplayNextDialogue()
    {
        canvasGroup.alpha = 1;
        
        if (currentDialogueIndex < dialogueData.dialogueEntries.Count)
        {
            DialogueEntry currentEntry = dialogueData.dialogueEntries[currentDialogueIndex];
            string fullText = currentEntry.context + "\n";
            string speakerTexts = GetSpeakerText(currentEntry.currentSpeaker);

            speakerText.text = speakerTexts;
            
            SetSpeakerImage(currentEntry.currentSpeaker);
            
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
            yield return new WaitForSeconds(0.01f); // 控制文本逐字显示速度
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

    public string GetSpeakerText(Speaker characterID)
    {
        switch (characterID)
        {
            case Speaker.Sword:
                return "刀劍";
                break;
            case Speaker.Rifle:
                return "步槍";
                break;
            case Speaker.Merchant:
                return "濱田彌兵衛";
                break;
            case Speaker.Missionary:
                return "尤紐斯";
                break;
            case Speaker.Player:
                return "玩家";
                break;
            case Speaker.Narration:
                return "旁白";
                break;
            default:
                return "沒這人";
                break;
        }
    }

    public void SetSpeakerImage(Speaker characterID)
    {
        Sprite currentSprite = null;
        
        switch (characterID)
        {
            case Speaker.Sword:
                currentSprite =  characterPack.currentPack[0];
                break;
            case Speaker.Rifle:
                currentSprite =  characterPack.currentPack[1];
                break;
            case Speaker.Merchant:
                currentSprite =  characterPack.currentPack[2];
                break;
            case Speaker.Missionary:
                currentSprite =  characterPack.currentPack[3];
                break;
            case Speaker.Player:
                currentSprite =  characterPack.currentPack[4];
                break;
            case Speaker.Narration:
                currentSprite =  characterPack.currentPack[5];
                break;
        }

        speakerImage.sprite = currentSprite;
    }
}
