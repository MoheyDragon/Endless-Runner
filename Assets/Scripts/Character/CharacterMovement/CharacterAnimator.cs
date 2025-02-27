using UnityEngine;
namespace EndlessRunner
{
    public class CharacterAnimator : MonoBehaviour
    {
        Animator animator;
        [SerializeField] private float[] animationSpeedLevels;
        const string moveSpeedParameter = "Moving";
        const string animationSpeedParameter = "AnimationSpeed";
        const string isGroundedParameter = "IsGrounded";
        const string deathParameter = "Death";
        const string directionParameter = "Direction";
        private void Start()
        {
            animator = GetComponent<Animator>();
            DifficultyManager.Singleton.OnDifficultyIncrease += IncreaseAnimationSpeedBasedOnDifficulty;
        }
        // Animation events Receviers
        public void OnStep()
        {
            SoundsManager.Singleton.OnFootStep();
        }
        public void OnLand()
        {
            SoundsManager.Singleton.OnFootStep();
        }
        public void StartRunning(bool enabled=true)
        {
            animator.SetBool(moveSpeedParameter, enabled);
            animator.SetFloat(directionParameter, 0);
        }
        public void SetDirection(float direction)
        {
            animator.SetFloat(directionParameter, direction);
        }
        public void Jump(bool isGounded)
        {
            animator.SetBool(isGroundedParameter, isGounded);
        }
        public void Death()
        {
            StartRunning(false);
            animator.SetTrigger(deathParameter);
        }

        private void IncreaseAnimationSpeedBasedOnDifficulty(int level)
        {
            animator.SetFloat(animationSpeedParameter, animationSpeedLevels[level]);
        }
    }
}