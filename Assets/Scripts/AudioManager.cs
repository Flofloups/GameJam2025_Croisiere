using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;

    [Range(0, 1)]
    public float Volume;
    [Range(.1f, 3f)]
    public float Pitch;

    public bool Loop;

    [HideInInspector]
    public AudioSource Source;
}

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    public static AudioManager Instance;

    private AudioSource _audioSource;

    private AudioClip[] _exempleListAudioClip;

    private int _randomSoundNum;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _exempleListAudioClip = Resources.LoadAll<AudioClip>("SFX_Exemple_List_AudioClip");
    }

    public void PlaySingleSound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Le son: " + name + " n'existe pas!");
            return;
        }
        s.Source.Play();

        /*
        Pour jouer un son unique dans d'autres scripts:
        AudioManager.Instance.PlaySingleSound("nom du son");
        */
    }

    public void StopSingleSound(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.Source.Stop();

        /*
        Pour stoper un son unique dans d'autres scripts:
        AudioManager.Instance.StopSingleSound("nom du son");
        */
    }

    public void PlayExempleListAudioClip()
    {
        _randomSoundNum = Random.Range(0, 3);
        _audioSource.PlayOneShot(_exempleListAudioClip[_randomSoundNum]);
    }

    /*
    Pour jouer un son al�atoire parmi une liste dans d'autres scripts:
    AudioManager.Instance.PlayExempleListAudioClip();
    */
}