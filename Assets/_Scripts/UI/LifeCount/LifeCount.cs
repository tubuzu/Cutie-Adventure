using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : Singleton<LifeCount>
{
    [SerializeField] public List<Image> lives;
    [SerializeField] public int livesRemaining;

    [SerializeField] Sprite activeHeart;
    [SerializeField] Sprite disabledHeart;

    protected override void Awake()
    {
        this.LoadComponents();
        this.ResetValues();
        this.MakeSingleton(false);
    }
    protected override void Start()
    {
        base.Start();
        livesRemaining = lives.Count;
        foreach (Image img in lives)
        {
            img.sprite = activeHeart;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImages();
    }

    protected virtual void LoadImages()
    {
        if (lives.Count > 0) return;
        this.lives = transform.GetComponentsInChildren<Image>().ToList();
    }

    public void LoseLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining == 0)
            return;
        //Decrease the value of livesRemaining
        livesRemaining--;
        //Hide one of the life images
        lives[livesRemaining].sprite = disabledHeart;
    }

    public void AddLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining >= lives.Count)
            return;
        //Decrease the value of livesRemaining
        livesRemaining++;
        //Hide one of the life images
        lives[livesRemaining - 1].sprite = activeHeart;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //         LoseLife();
    // }

}