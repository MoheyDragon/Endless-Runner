using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public struct SoundElement
{
    public AudioClip audioClip;
    public AudioMixerGroup audioMixerGroup;

    public SoundElement(AudioClip audioClip, AudioMixerGroup audioMixerGroup)
    {
        this.audioClip = audioClip;
        this.audioMixerGroup = audioMixerGroup;
    }
}