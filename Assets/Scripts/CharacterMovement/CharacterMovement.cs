using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    const string moveSpeedParameter= "Moving";
    const string animationSpeedParameter= "AnimationSpeed";
    const string jumpParameter= "Jumping";
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.SetBool(moveSpeedParameter, Input.GetAxisRaw("Vertical")>0);
        animator.SetBool(jumpParameter, Input.GetKey(KeyCode.Space));
        if (Input.GetKey(KeyCode.LeftShift))
            animator.SetFloat(animationSpeedParameter, 2);
        if (Input.GetKey(KeyCode.LeftControl))
            animator.SetFloat(animationSpeedParameter, 0.5f);
        if (Input.GetKey(KeyCode.CapsLock))
            animator.SetFloat(animationSpeedParameter, 1);


    }
}
