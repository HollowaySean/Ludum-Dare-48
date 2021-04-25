using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.75f,
        volumeVariance = 0.1f,
        pitch = 1f,
        pitchVariance = 0.1f;

    public bool loop = false;

    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;
}
