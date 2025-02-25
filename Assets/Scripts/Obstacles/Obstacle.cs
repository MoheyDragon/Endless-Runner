using UnityEngine;
namespace EndlessRunner
{
    public class Obstacle : MonoBehaviour, ITouchable, IPoolable
    {
        [SerializeField] int damageAmount=1;
        //ITouchable
        public void OnTouch()
        {
        }
        public bool DoesHarm()
        {
            return true;
        }

        public int Damage => damageAmount;

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