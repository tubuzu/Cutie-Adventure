using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCount : Singleton<StarCount>
{
    [SerializeField] public List<Image> stars;
    [SerializeField] public int starCount;

    [SerializeField] Sprite activeStar;
    [SerializeField] Sprite disabledStar;

    protected override void Awake()
    {
        this.LoadComponents();
        this.ResetValues();
        this.MakeSingleton(false);
    }
    protected override void Start()
    {
        base.Start();
        starCount = 0;
        foreach (Image img in stars)
        {
            img.sprite = disabledStar;
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImages();
    }

    protected virtual void LoadImages()
    {
        if (stars.Count > 0) return;
        this.stars = transform.GetComponentsInChildren<Image>().ToList();
    }

    public void AddStar()
    {
        if (starCount >= stars.Count)
            return;
        stars[starCount].sprite = activeStar;
        starCount++;
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //         LoseStar();
    // }

}