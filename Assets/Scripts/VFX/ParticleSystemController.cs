using UnityEngine;
using System.Collections;
//Put this script on the following game object:
//1-not the main prefab object put one of the childs
//2-the main prefab should have no particle system
//3-all childs should have partilce system and are not nested
//4-put the script on only THE FIRST CHILD ,it must be the longest one to finish
//5-put stop action of this game object to :Callback
namespace EndlessRunner
{
    public class ParticleSystemController : MonoBehaviour
    {
        int counter;
        ParticleSystem[] systems;
        Transform parent;
        private void Awake()
        {
            parent = transform.parent;
            counter = transform.parent.childCount;
            systems = new ParticleSystem[counter];
            for (int i = 0; i < counter; i++)
                systems[i] = parent.GetChild(i).GetComponent<ParticleSystem>();
            Deactivate();
        }
        public void Play(Transform place, bool IsWorldSpace)
        {
            parent.gameObject.SetActive(true);
            if (!IsWorldSpace)
                parent.parent = place;
            parent.position = place.position;
            foreach (ParticleSystem item in systems)
                item.Play();
        }
        public void OnParticleSystemStopped()
        {
            parent.gameObject.SetActive(false);
        }
        public void Deactivate()
        {
            parent.gameObject.SetActive(false);
        }
    }
}