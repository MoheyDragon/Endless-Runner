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
        [SerializeField] Transform footstepsParent;
        [SerializeField] Transform hitSoundsParent;
        [SerializeField] Vector2 pitchRandom;
        AudioSource[] hitSounds;
        AudioSource[] footsteps;
        private void Start()
        {
            footsteps=GeneralMethods.PopulateArrayFromParent<AudioSource>(footstepsParent);
            hitSounds = GeneralMethods.PopulateArrayFromParent<AudioSource>(hitSoundsParent);
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
            int random = Random.Range(0, footsteps.Length);
            sounds[random].Play();
            sounds[random].pitch = Random.Range(pitchRandom.x, pitchRandom.y);
        }
        
    }
}