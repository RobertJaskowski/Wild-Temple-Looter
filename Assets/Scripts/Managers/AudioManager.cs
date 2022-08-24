using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region instance

    public static AudioManager instance;

    private void Instantiate()
    {
        if (instance != null)
        {
            Debug.Log("AudioManager already exists");
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }

    }

    #endregion


    public Sound[] sounds;

    private void Awake()
    {
        Instantiate();
        DontDestroyOnLoad(this.gameObject);


        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();


            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    internal void StopPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Can't stop playing " + name);
            return;
        }

        s.source.Stop();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Can't play " + name);
            return;
        }

        s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];


        s.source.Play();
    }

}
