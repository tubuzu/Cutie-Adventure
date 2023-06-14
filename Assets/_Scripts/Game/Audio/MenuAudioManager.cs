using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : AudioManager
{
    //Sound Effects
    public AudioClip buttonClickSound;
    //Music
    public AudioClip musicSound;

    public override void PlaySFX(EffectSound sfxName)
    {
        switch (sfxName)
        {
            case EffectSound.ButtonClickSound:
                SoundObjectCreation(buttonClickSound);
                break;
            default:
                break;
        }
    }

    public override void PlayMusic(MusicSound musicName)
    {
        switch (musicName)
        {
            case MusicSound.BackgroundSound:
                MusicObjectCreation(musicSound);
                break;
            default:
                break;
        }
    }
}