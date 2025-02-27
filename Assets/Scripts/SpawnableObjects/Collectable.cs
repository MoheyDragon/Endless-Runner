using UnityEngine;
namespace EndlessRunner
{
    public class Collectable : MonoBehaviour, ITouchable, IPoolable
    {
        //ITouchable
        public void OnTouch()
        {
            //Actually using Action/Events and subscribing to it is better in this situation, but done it this way to save time, also this is not the only place, Check how I am handelling Actions in Difficulty Manager 
            SoundsManager.Singleton.OnCollecting();
            ObjetsSpawnerManager.Singleton.ReleaseCollectable(this);
            ScoringManager.Singleton.OnCollecting();
        }
        public bool DoesHarm()
        {
            return false;
        }

        public int Damage => 0;

        //IPoolable
        public void OnDestroy()
        {
        }


        public void OnGet()
        {
        }


        public void OnRelease()
        {
        }

    }
}
