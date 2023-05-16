using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour, IDataPersistence
{

    #region Singleton

    public static SoundManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one SoundManager of task found");
            return;
        }

        instance = this;
    }
    #endregion

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource audioSource;

    [Header("Music")]
    public float musicVolume = 0.8f;
    public AudioClip[] musics;

    [Header("Music")]
    public float SFXVolume = 1f;

    [Header("Audio Clips")]
    public AudioClip[] sounds;

    //MUSIC
    public void PlayRelaxingMusic()
    {
        musicSource.clip = musics[0];
        musicSource.Play();
    }
    public void PlayLookingForClues()
    {
        musicSource.clip = musics[1];
        musicSource.Play();
    }

    public void PlayVillageMusic()
    {
        musicSource.clip = musics[2];
        musicSource.Play();
    }

    public void UpdateSFXVolume(float volume)
    {
        if (SFXVolume != volume)
        {
            SFXVolume = volume;
            audioSource.volume = SFXVolume;
        }
    }

    public void UpdateMusicVolume(float volume)
    {
        if (musicVolume != volume)
        {
            musicVolume = volume;
            musicSource.volume = musicVolume;
        }
    }
    public void PlaySoundFromClips(int sound)
    {
        switch (sound)
        {
            case 0:
                audioSource.PlayOneShot(sounds[0], 0.5f);
                break;
            case 1:
                audioSource.PlayOneShot(sounds[1], 1f);
                break;
            case 2:
                audioSource.PlayOneShot(sounds[2], 1f);
                break;
            case 3:
                audioSource.PlayOneShot(sounds[3], 0.3f);
                break;
            case 4:
                audioSource.PlayOneShot(sounds[4], 0.8f);
                break;
            case 5:
                audioSource.PlayOneShot(sounds[5], 0.7f);
                break;
            case 6:
                audioSource.PlayOneShot(sounds[6], 0.7f);
                break;
            case 7:
                audioSource.PlayOneShot(sounds[7], 1f);
                break;
            case 8:
                audioSource.PlayOneShot(sounds[8], 1f);
                break;
            case 9:
                audioSource.PlayOneShot(sounds[9], 0.5f);
                break;
            case 10:
                audioSource.PlayOneShot(sounds[10], 0.7f);
                break;
            case 11:
                //pencil check
                audioSource.PlayOneShot(sounds[11], 0.5f);
                break;
            case 12:
                //water
                audioSource.PlayOneShot(sounds[12], 1f);
                break;
            case 13:
                //treasure chest
                audioSource.PlayOneShot(sounds[13], 1f);
                break;
            default:
                Debug.LogError("No sound sound");
                break;
        }
    }
    public void PlaySound(AudioClip clip)
    { 
        audioSource.PlayOneShot(clip);  
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }


    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    public void LoadData(GameData data)
    {
        musicSlider.value = data.musicVolume; // Set the slider value
        sfxSlider.value = data.SFXVolume; // Set the slider value
        //musicSource.volume = data.musicVolume;
        //audioSource.volume = data.SFXVolume;
    }

    public void SaveData(GameData data)
    {
        data.musicVolume = musicSource.volume;
        data.SFXVolume = audioSource.volume;
    }
}
