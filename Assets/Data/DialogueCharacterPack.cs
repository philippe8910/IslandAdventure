using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSpriteData")]
public class DialogueCharacterPack : ScriptableObject
{
    public List<Sprite> currentPack = new List<Sprite>();
}
