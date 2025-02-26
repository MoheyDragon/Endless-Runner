using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class GlobalSoundsManager : Singletons<GlobalSoundsManager>
{
    AudioSource[] musicAuidoSources;
    [SerializeField] AudioSource audioPrefab;
    [SerializeField] AudioSource musicPrefab;
    [SerializeField] Transform soundsParent;
    [SerializeField] Transform musicParent;
    [SerializeField] int maximumAudioSourcePlaying;
    [SerializeField] Vector2 pitchRandom;
    [SerializeField] int musicLayers;
    int currentAudioSourceCount;
    List<AudioSource> audioSources=new ();
    protected override void Awake()
    {
        base.Awake();
        CreateMusicLayersAudioSources();
    }
    private void CreateMusicLayersAudioSources()
    {
        musicAuidoSources = new AudioSource[musicLayers];
        for (int i = 0; i < musicLayers; i++)
        {
            musicAuidoSources[i]=Instantiate(musicPrefab,musicParent);
            musicAuidoSources[i].gameObject.name = "music layer " + (i+1);
        }
    }
    public void OnClipFinished(AudioSource source)
    {
        audioSources.Remove(source);
        currentAudioSourceCount--;
    }

    public void PlayRandomSound(AudioClip[] audioClips, AudioMixerGroup audioMixerGroup)
    {
        AudioClip audioClip = audioClips[Random.Range(0, audioClips.Length)];
        SoundElement soundElement = new SoundElement(audioClip,audioMixerGroup);

        PlaySound(soundElement,true);
    }
    public void PlaySound(SoundElement soundElement ,bool randomPitch = false,bool isLoop=false)
    {
        if (currentAudioSourceCount >= maximumAudioSourcePlaying) audioSources.RemoveAt(0);

        GameObject newAudio = Instantiate(audioPrefab.gameObject, soundsParent);
        AudioSource audioSource = newAudio.GetComponent<AudioSource>();

        audioSource.clip = soundElement.audioClip;;
        audioSource.outputAudioMixerGroup = soundElement.audioMixerGroup;

        audioSource.Play();

        if (randomPitch)
            RandomizePitch(audioSource);

        if (isLoop)
            audioSource.loop = true;

        currentAudioSourceCount++;
        audioSources.Add(audioSource);
    }
    public void PlayMusic(AudioClip music, int layerIndex=0)
    {
        musicAuidoSources[layerIndex].clip = music;
        musicAuidoSources[layerIndex].Play();
    }
    
    private void RandomizePitch(AudioSource source)
    {
        source.pitch = Random.Range(pitchRandom.x, pitchRandom.y);
    }
}
