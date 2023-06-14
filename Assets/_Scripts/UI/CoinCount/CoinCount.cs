using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : Singleton<CoinCount>
{
    [SerializeField] public int currentCount;

    [SerializeField] public Text countText;

    protected override void Awake()
    {
        this.LoadComponents();
        this.ResetValues();
        this.MakeSingleton(false);
    }
    protected override void Start()
    {
        base.Start();
        currentCount = 0;
        countText.text = "X " + currentCount.ToString();
    }

    public void AddOneCoin()
    {
        //Decrease the value of livesRemaining
        currentCount++;
        //Hide one of the life images
        countText.text = "X " + currentCount.ToString();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Z))
    //         AddOneCoin();
    // }
}
