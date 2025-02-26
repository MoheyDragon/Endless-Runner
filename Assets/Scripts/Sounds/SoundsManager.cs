using UnityEngine;
using UnityEngine.Audio;
public class SoundsManager : GlobalSoundsManager
{
    [Header("Audio Mixer Group")]
    [Space]
    [SerializeField] AudioMixerGroup masterAMG;
    [SerializeField] AudioMixerGroup musicAMG;
    [SerializeField] AudioMixerGroup sfxAMG;
    [SerializeField] AudioMixerGroup footStepsAMG;
    [Space]
    [Header("Music")]
    [SerializeField] AudioClip mainMusic;
    [SerializeField] AudioClip intenseMusic;
    [Space]
    [Space]
    [Header("General Sounds")]
    [SerializeField] SoundsSurfaceFootSteps[] soundsSurfaces;
    [SerializeField] AudioClip[] selectCharacter;
    [SerializeField] AudioClip[] actionNotPossibleSounds;
    [SerializeField] AudioClip[] showInteractionIconSounds;
    [Space]
    public static new SoundsManager Singleton;
    SoundsSurfaceFootSteps currentSoundsSurface;
    protected override void Awake()
    {
        base.Awake();
        Singleton = this;
        currentSoundsSurface=soundsSurfaces[0];
    }
    public void StartMusic()
    {
        PlayMusic(mainMusic);
    }
    public void MuteMusic()
    {
        musicAMG.audioMixer.SetFloat("MusicVolume", -40);
    }
    public void ChangeSoundsSurface(SoundsSurfaceFootSteps newSurface)
    {
        currentSoundsSurface = newSurface;
    }
    public void FootStepSound(Vector3 position)
    {
        PlayRandomSound(currentSoundsSurface.sounds, footStepsAMG);
    }
    public void SelectCharacter()
    {
        PlayRandomSound(selectCharacter, sfxAMG);
    }
    public void ShowInteractionIcon()
    {
        PlayRandomSound(showInteractionIconSounds, sfxAMG);
    }
    public void ActionNotPossible()
    {
        PlayRandomSound(actionNotPossibleSounds, sfxAMG);
    }
}
