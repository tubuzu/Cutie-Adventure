using UnityEngine;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    private enum Sub
    {
        Main,
        LevelSelection,
        Options,
        About,
        HowToPlay
    }

    GameObject mainSub;
    GameObject optionSub;
    GameObject aboutSub;
    GameObject levelSelectSub;

    [SerializeField] private AudioMixer myMixer;

    private void Awake()
    {
        mainSub = transform.Find("MainSub").gameObject;
        optionSub = transform.Find("OptionSub").gameObject;
        aboutSub = transform.Find("AboutSub").gameObject;
        levelSelectSub = transform.Find("LevelSelectSub").gameObject;
        mainSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        optionSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        aboutSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        levelSelectSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    private void Start()
    {
        // Play music
        myMixer.SetFloat("sfx", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume", 1)) * 20);
        myMixer.SetFloat("music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume", 1)) * 20);
        MenuAudioManager.Ins.PlayMusic(MusicSound.BackgroundSound);

        // Display main menu
        ShowSub(Sub.Main);
    }
    private void ShowSub(Sub sub)
    {
        mainSub.SetActive(false);
        optionSub.SetActive(false);
        aboutSub.SetActive(false);
        levelSelectSub.SetActive(false);

        switch (sub)
        {
            case Sub.Main:
                mainSub.SetActive(true);
                break;
            case Sub.Options:
                optionSub.SetActive(true);
                break;
            case Sub.About:
                aboutSub.SetActive(true);
                break;
            case Sub.LevelSelection:
                levelSelectSub.SetActive(true);
                break;
        }
    }
    public void Play()
    {
        MenuAudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        ShowSub(Sub.LevelSelection);
    }
    public void Option()
    {
        MenuAudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        ShowSub(Sub.Options);
    }
    public void About()
    {
        MenuAudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        ShowSub(Sub.About);
    }
    public void Back()
    {
        MenuAudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        ShowSub(Sub.Main);
    }
    public void Quit()
    {
        MenuAudioManager.Ins.PlaySFX(EffectSound.ButtonClickSound);
        Application.Quit();
    }
}