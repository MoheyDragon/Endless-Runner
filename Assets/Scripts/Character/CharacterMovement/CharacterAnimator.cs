using UnityEngine;
namespace EndlessRunner
{
    public class CharacterAnimator : MonoBehaviour
    {
        Animator animator;
        const string moveSpeedParameter = "Moving";
        const string animationSpeedParameter = "AnimationSpeed";
        const string isGroundedParameter = "IsGrounded";
        const string deathParameter = "Death";

        private void Start()
        {
            animator = GetComponent<Animator>();
            StartRunning();
        }
        public void StartRunning(bool enabled=true)
        {
            animator.SetBool(moveSpeedParameter, enabled);
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
        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftShift))
                animator.SetFloat(animationSpeedParameter, 2);
            if (Input.GetKey(KeyCode.LeftControl))
                animator.SetFloat(animationSpeedParameter, 0.5f);
            if (Input.GetKey(KeyCode.CapsLock))
                animator.SetFloat(animationSpeedParameter, 1);


        }
    }
}