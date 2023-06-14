using System;
// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MyMonoBehaviour
{
    public int currentSelection = 0;

    protected int totalChar = 0;
    // public List<Sprite> characterImages;
    // public List<string> characterNames;

    public List<CharacterInfo> characterInfos;

    public Image characterImage;
    public Text characterName;

    public Animator transitionAnim;

    protected override void Awake()
    {
        base.Awake();
        this.totalChar = characterInfos.Count;
        this.currentSelection = PlayerPrefs.GetInt("SelectedCharacter", 0);
        this.UpdateCharacter();
    }

    public void NextCharacter()
    {
        if (currentSelection < totalChar - 1)
        {
            currentSelection++;
        }
        else currentSelection = 0;
        this.UpdateCharacter();
    }

    public void PrevCharacter()
    {
        if (currentSelection > 0)
        {
            currentSelection--;
        }
        else currentSelection = totalChar - 1;
        this.UpdateCharacter();
    }

    protected void UpdateCharacter()
    {
        this.characterImage.sprite = characterInfos[currentSelection].characterImage;
        this.characterName.text = characterInfos[currentSelection].characterName;
        this.transitionAnim.Play("ZoomInAndOut", -1, 0);
        PlayerPrefs.SetInt("SelectedCharacter", currentSelection);
    }
}

[Serializable]
public class CharacterInfo
{
    public string characterName;
    public Sprite characterImage;
}
