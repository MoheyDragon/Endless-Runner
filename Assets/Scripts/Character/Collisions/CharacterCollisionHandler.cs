using UnityEngine;
namespace EndlessRunner
{
    public class CharacterCollisionHandler : MonoBehaviour
    {
        CharacterHealthSystem healthSystem;
        [SerializeField] AudioClip[] collectingSounds;
        private void Awake()
        {
            healthSystem = GetComponent<CharacterHealthSystem>();
        }
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.TryGetComponent(out ITouchable tochedObject))
            {
                tochedObject.OnTouch();
                if(tochedObject.DoesHarm())
                {
                    healthSystem.OnHit(tochedObject.Damage);
                }
            }
        }
    }
}