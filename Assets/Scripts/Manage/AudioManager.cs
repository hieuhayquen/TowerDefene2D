using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSources, sfxSources;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        if (DataPersistenceManager.instance.gameData != null) {
            if (DataPersistenceManager.instance.gameData.isMusicOn) {
                musicSources.mute = false;
                PlayMusic("BackgroundMusic");
            } else {
                musicSources.mute = true;
            }
            if (DataPersistenceManager.instance.gameData.isSfxOn) {
                sfxSources.mute = false;
            } else {
                sfxSources.mute = true;
            }
        } 
        PlayMusic("BackgroundMusic");
        
    }

    public void PlayMusic(string name) {
        Sound sound = Array.Find(musicSounds, s => s.name == name);
        if (sound != null) {
            musicSources.clip = sound.clip;
            musicSources.Play();
        } else {
            Debug.Log("Sound not found");
        }
    }

    public void PlaySfx(string name) {
        Sound sfx = Array.Find(sfxSounds, s => s.name == name);
        if (sfx != null) {
            sfxSources.clip = sfx.clip;
            sfxSources.PlayOneShot(sfx.clip);
        } else {
            Debug.Log("Sfx not found");
        }
    }

    public void ToggleMusic() {
        musicSources.mute = !musicSources.mute;
        if (DataPersistenceManager.instance.gameData != null) {
            DataPersistenceManager.instance.gameData.isMusicOn = !DataPersistenceManager.instance.gameData.isMusicOn;
        }
    }

    public void ToggleSfx() {
        sfxSources.mute = !sfxSources.mute;
        if (DataPersistenceManager.instance.gameData != null) {
            DataPersistenceManager.instance.gameData.isSfxOn = !DataPersistenceManager.instance.gameData.isSfxOn;
        }
    }

    public void MusicVolume(float volume) {
        if (DataPersistenceManager.instance.gameData != null) {
            DataPersistenceManager.instance.gameData.sound = volume;
        }
        musicSources.volume = volume;
    }

    public void SfxVolume(float volume) {
        if (DataPersistenceManager.instance.gameData != null) {
            DataPersistenceManager.instance.gameData.sfx = volume;
        }
        sfxSources.volume = volume;
    }
}
