using UnityEngine;
namespace EndlessRunner
{
    public class CharacterAnimator : MonoBehaviour
    {
        Animator animator;
        const string moveSpeedParameter = "Moving";
        const string animationSpeedParameter = "AnimationSpeed";
        const string jumpParameter = "Jumping";
        private void Start()
        {
            animator = GetComponent<Animator>();
            StartRunning();
        }
        public void StartRunning(bool enabled=true)
        {
            animator.SetBool(moveSpeedParameter, enabled);
        }
        public void Jump()
        {
            animator.SetBool(jumpParameter, true);
        }
        public void Land()
        {
            animator.SetBool(jumpParameter, false);
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