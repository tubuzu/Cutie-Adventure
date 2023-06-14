using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : AudioManager
{
    //Sound Effects
    public AudioClip loseSound;
    public AudioClip jumpSound;
    public AudioClip collectSound;
    public AudioClip coinSound;
    public AudioClip winSound;
    public AudioClip hitSound;
    //Music
    public AudioClip musicSound;

    public override void PlaySFX(EffectSound sfxName)
    {
        switch (sfxName)
        {
            case EffectSound.LoseSound:
                SoundObjectCreation(loseSound);
                break;
            case EffectSound.JumpSound:
                SoundObjectCreation(jumpSound);
                break;
            case EffectSound.CollectItemSound:
                SoundObjectCreation(collectSound);
                break;
            case EffectSound.CoinSound:
                SoundObjectCreation(coinSound);
                break;
            case EffectSound.WinSound:
                SoundObjectCreation(winSound);
                break;
            case EffectSound.HitSound:
                SoundObjectCreation(hitSound);
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