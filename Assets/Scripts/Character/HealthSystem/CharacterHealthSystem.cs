using UnityEngine;
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
            health-=damage;
            if (health <= 0)
                Death();
            else
                characterMovementController.OnHit();
        }
        public void Death()
        {
            GameStateManager.Singleton.SetState(GameState.END);
        }
    }
}