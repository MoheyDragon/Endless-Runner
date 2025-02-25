using UnityEngine;
namespace EndlessRunner
{
    public class Obstacle : MonoBehaviour, IPoolable
    {
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