using UnityEngine;
using UnityEngine.Audio;
namespace EndlessRunner
{
    public class CharacterHealthSystem : MonoBehaviour
    {
        [SerializeField] int maxHealth=1;
        CharacterMovementController characterMovementController;
        int health;
        private void Awake()
        {
            characterMovementController = GetComponent<CharacterMovementController>();
        }
        private void Start()
        {
            health = maxHealth;
        }
        public void OnHit(int damage)
        {
            SoundsManager.Singleton.OnHit();
            health-=damage;
            if (health <= 0)
                Death();
            else
                characterMovementController.OnHit();
        }
        public void Death()
        {
            SoundsManager.Singleton.OnDeath();
            GameStateManager.Singleton.SetState(GameState.END);
        }
    }
}