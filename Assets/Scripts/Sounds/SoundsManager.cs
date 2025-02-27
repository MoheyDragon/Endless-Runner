using UnityEngine;
using MoheyGeneralMethods;
using UnityEngineInternal;
namespace EndlessRunner
{
    public class SoundsManager : Singletons<SoundsManager>
    {
        [SerializeField] AudioSource music;
        [SerializeField] AudioSource whooshSound;
        [SerializeField] AudioSource death;

        [Space]

        [SerializeField] Transform footstepsParent;
        [SerializeField] Transform hitSoundsParent;
        [SerializeField] Transform collectingSoundsParent;
        [SerializeField] Vector2 pitchRandom;

        AudioSource[] hitSounds;
        AudioSource[] footsteps;
        AudioSource[] collectingSounds;
        private void Start()
        {
            footsteps=GeneralMethods.PopulateArrayFromParent<AudioSource>(footstepsParent);
            hitSounds = GeneralMethods.PopulateArrayFromParent<AudioSource>(hitSoundsParent);
            collectingSounds=GeneralMethods.PopulateArrayFromParent<AudioSource>(collectingSoundsParent);
        }
        public void PlayMusic()
        {
            music.Play();
        }
        public void StopMusic()
        {
            music.Stop();
        }
        public void Woosh()
        {
            whooshSound.Play();
        }
        public void OnCollecting()
        {
            PlayRandomSound(collectingSounds);
        }
        public void OnFootStep()
        {
            PlayRandomSound(footsteps);
        }
        public void OnHit()
        {
            hitSounds[Random.Range(0, hitSounds.Length)].Play();
        }
        public void OnDeath()
        {
            death.Play();
        }
        private void PlayRandomSound(AudioSource[] sounds)
        {
            int random = Random.Range(0, sounds.Length);
            sounds[random].Play();
            sounds[random].pitch = Random.Range(pitchRandom.x, pitchRandom.y);
        }
        
    }
}