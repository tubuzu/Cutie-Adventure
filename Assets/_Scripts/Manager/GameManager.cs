using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] protected int currentLevel;
    public bool pause = false;
    public int CurrentLevel { get => currentLevel; }
    public bool levelCompleted = false;
    protected override void Awake()
    {
        this.MakeSingleton(false);
        // Get the name of the active scene
        string sceneName = SceneManager.GetActiveScene().name;
        // Get the last two characters of the scene name
        currentLevel = int.Parse(sceneName.Substring(sceneName.Length - 2));
    }
    protected override void Start()
    {
        base.Start();
        AudioManager.Ins.PlayMusic(MusicSound.BackgroundSound);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pause = true;
        AudioManager.Ins.StopCurrentMusic(true);
        UIManager.Ins.pauseMenu.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        AudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        Time.timeScale = 1;
        pause = false;
        AudioManager.Ins.StopCurrentMusic(false);
        UIManager.Ins.pauseMenu.gameObject.SetActive(false);
    }
    public void OnCompleteLevel()
    {
        AudioManager.Ins.StopCurrentMusic(true);
        AudioManager.Ins.PlaySFX(EffectSound.WinSound);

        this.levelCompleted = true;

        // PlayerCtrl.Ins.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        // PlayerCtrl.Ins.PlayerAnimation.ChangeAnimationState(PlayerAnimationState.PLAYER_IDLE);
        PlayerPrefs.SetInt("LevelStar_" + (GameManager.Ins.CurrentLevel).ToString("00"), StarCount.Ins.starCount);

        UIManager.Ins.completeMenu.GetComponent<CompleteMenu>().EnableCompleteMenu();
    }
    public void RestartLevel()
    {
        AudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        UIManager.Ins.gameObject.SetActive(false);
        this.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Quit()
    {
        AudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        Application.Quit();
    }
    public void Home()
    {
        AudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        Time.timeScale = 1;
        pause = false;
        UIManager.Ins.gameObject.SetActive(false);
        this.LoadScene("MainMenu");
    }
    public void NextLevel()
    {
        AudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        this.currentLevel++;
        if (SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/Level_" + currentLevel.ToString("00") + ".unity") != -1)
        {
            if (this.currentLevel > PlayerPrefs.GetInt("ReachedLevel"))
                PlayerPrefs.SetInt("ReachedLevel", currentLevel);
            UIManager.Ins.gameObject.SetActive(false);
            this.LoadScene("Level_" + currentLevel.ToString("00"));
        }
        else
        {
            UIManager.Ins.gameObject.SetActive(false);
            SceneManager.LoadScene("MainMenu");
        }
    }

    protected void LoadScene(string sceneName)
    {
        // if (SceneLoader.Instance != null)
        //     SceneLoader.Instance.LoadScene(sceneName);
        // else SceneManager.LoadScene(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
