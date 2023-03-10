using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static audioManager instance;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " cannot be found");
            return;
        }
        s.source.Play();
    }
}
