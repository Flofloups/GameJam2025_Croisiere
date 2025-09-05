using System;
using UnityEngine;

[Serializable]
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

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;

            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
        }

        foreach (Sound sound in Sounds)
        {
            if (sound.Loop)
            {
                PlaySingleSound(sound.Name);
            }
        }
    }
    
    public void PlaySingleSound(string soundName)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Le son: " + soundName + " n'existe pas!");
            return;
        }
        s.Source.Play();

        /*
        Pour jouer un son unique dans d'autres scripts:
        AudioManager.Instance.PlaySingleSound("nom du son");
        */
    }

    public void StopSingleSound(string soundName)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        s.Source.Stop();
    }
}