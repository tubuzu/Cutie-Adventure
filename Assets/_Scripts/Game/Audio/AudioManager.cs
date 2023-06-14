using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    //Music Object
    [SerializeField] private AudioMixer myMixer;
    protected float playbackPosition;
    public GameObject currentMusicObject;
    //Sound Object
    public GameObject sfxObject;
    public GameObject musicObject;
    public float soundVolume = 1f;
    public float musicVolume = 1f;
    
    protected override void Awake()
    {
        this.MakeSingleton(false);

        myMixer.SetFloat("music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolume")) * 20);
        myMixer.SetFloat("sfx", Mathf.Log10(PlayerPrefs.GetFloat("sfxVolume")) * 20);
    }

    public virtual void PlaySFX(EffectSound sfxName) { }

    protected void SoundObjectCreation(AudioClip clip)
    {
        //Create SoundsObject gameobject
        GameObject newObject = Instantiate(sfxObject, transform);
        //Destroy gameobject when sound is not playing
        newObject.AddComponent<KillSound>();
        //Assign audioclip to its audiosource
        newObject.GetComponent<AudioSource>().clip = clip;
        //Apply volume
        newObject.GetComponent<AudioSource>().volume = soundVolume;
        //Play the audio
        newObject.GetComponent<AudioSource>().Play();
    }
    public virtual void PlayMusic(MusicSound musicName) { }

    public void StopCurrentMusic(bool stop)
    {
        AudioSource audioSource = currentMusicObject.GetComponent<AudioSource>();
        if (stop && audioSource.isPlaying)
        {
            playbackPosition = audioSource.time;
            audioSource.Stop();
        }
        else if (!stop && !audioSource.isPlaying)
        {
            // Later, resume the audio from where it left off
            audioSource.time = playbackPosition;
            audioSource.Play();
        }
    }

    protected void MusicObjectCreation(AudioClip clip)
    {
        //Check if there's an existing music object, if so delete it
        if (currentMusicObject)
            Destroy(currentMusicObject);
        //Create SoundsObject gameobject
        currentMusicObject = Instantiate(musicObject, transform);
        AudioSource aus = currentMusicObject.GetComponent<AudioSource>();
        //Assign audioclip to its audiosource
        aus.clip = clip;
        //Make the audio source looping
        aus.loop = true;
        //Apply volume
        aus.volume = musicVolume;
        //Play the audio
        aus.Play();
    }
}