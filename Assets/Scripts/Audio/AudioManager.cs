using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Music[] musicSounds;

    public float musicVolume = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {        
        foreach (Music m in musicSounds)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
        }
    }

    void Update()
    {
        foreach (Music m in musicSounds)
        {
            m.source.volume = m.volume;
        }
    }

    public void Play(string name)
    {
        Music m = Array.Find(musicSounds, sound => sound.name == name);
        if (m == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        m.source.Play();
    }
}
