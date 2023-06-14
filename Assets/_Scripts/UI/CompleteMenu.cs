using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompleteMenu : MyMonoBehaviour
{
    [SerializeField] List<Image> stars;
    [SerializeField] Text currentLevel;
    [SerializeField] Text earnedCoins;

    [SerializeField] Sprite activeStar;
    [SerializeField] Sprite disabledStar;

    // [SerializeField] Button homeBtn;
    // [SerializeField] Button retryBtn;
    // [SerializeField] Button nextLevelBtn;

    // protected override void LoadComponents()
    // {
    //     base.LoadComponents();
    //     this.LoadHomeBtn();
    //     this.LoadRetryBtn();
    //     this.LoadNextLevelBtn();
    // }

    // protected virtual void LoadHomeBtn()
    // {
    //     if (this.homeBtn != null) return;
    //     this.homeBtn = transform.Find("Content").Find("Options").Find("Home").GetComponent<Button>();
    //     this.homeBtn.onClick.AddListener(GameManager.Ins.Home);
    // }

    // protected virtual void LoadRetryBtn()
    // {
    //     if (this.retryBtn != null) return;
    //     this.retryBtn = transform.Find("Content").Find("Options").Find("Retry").GetComponent<Button>();
    //     this.retryBtn.onClick.AddListener(GameManager.Ins.RestartLevel);
    // }

    // protected virtual void LoadNextLevelBtn()
    // {
    //     if (this.nextLevelBtn != null) return;
    //     this.nextLevelBtn = transform.Find("Content").Find("Options").Find("NextLevel").GetComponent<Button>();
    //     this.nextLevelBtn.onClick.AddListener(GameManager.Ins.NextLevel);
    // }

    public void EnableCompleteMenu()
    {
        currentLevel.text = "level " + GameManager.Ins.CurrentLevel.ToString();
        earnedCoins.text = "x " + CoinCount.Ins.currentCount.ToString();
        PlayerPrefs.SetInt("Coin", CoinCount.Ins.currentCount + PlayerPrefs.GetInt("Coin", 0));
        for (int i = 0; i < stars.Count; i++)
        {
            if (i < StarCount.Ins.starCount) stars[i].sprite = activeStar;
            else stars[i].sprite = disabledStar;
        }
        this.gameObject.SetActive(true);
    }
}
