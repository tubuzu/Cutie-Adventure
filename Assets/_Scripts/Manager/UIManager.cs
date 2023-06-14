using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject pauseMenu;
    public GameObject completeMenu;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPauseMenu();
        this.LoadCompleteMenu();
    }

    protected virtual void LoadPauseMenu()
    {
        if (this.pauseMenu != null) return;
        this.pauseMenu = transform.Find("PauseMenu").gameObject;
        this.pauseMenu.SetActive(false);
    }

    protected virtual void LoadCompleteMenu()
    {
        if (this.completeMenu != null) return;
        this.completeMenu = transform.Find("CompleteMenu").gameObject;
        this.completeMenu.SetActive(false);
    }

    protected override void Awake()
    {
        this.MakeSingleton(false);
    }
}
