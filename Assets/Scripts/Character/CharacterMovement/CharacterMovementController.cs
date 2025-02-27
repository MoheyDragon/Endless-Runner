using UnityEngine;
using System.Collections;
using MoheySwipeSystem;
using UnityEngine.Audio;
namespace EndlessRunner
{
    public class CharacterMovementController : MonoBehaviour
    {
        CharacterAnimator animator;
        private Rigidbody rb;

        [SerializeField] Vector2 movementAreaBoundaries;
        [SerializeField] int lanesCounts;
        float[] lanesXPos;
        int currentLane;

        [Space]
        [SerializeField] float[] levelSpeeds;
        [SerializeField] float speed;
        [SerializeField] float slideSpeed;
        public float Speed => speed;

        [Space]
        [SerializeField] private float recoveryTime = 0.5f; 
        [SerializeField] private float pushbackDistance = 5;
        [Space]
        [Header("Jump")]
        [Space]
        [SerializeField] Transform groundChecker;
        [SerializeField] float jumpPower = 10;
        [SerializeField] LayerMask groundMask;
        [SerializeField] float groundCheckDistance;
        private float laneChangeDuration=0.5f;
        private bool isSliding;
        private bool isGameRunning;
        public bool IsGameRunning => isGameRunning;
        private void Awake()
        {
            animator = GetComponent<CharacterAnimator>();
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            SetupLanesPositions();
            Subscribe();
        }
        public void OnMatchBegan()
        {
            isGameRunning = true;
            animator.StartRunning();
        }
        private void Update()
        {
            if (!isGameRunning) return;
            animator.Jump(IsGrounded());
        }
        private void SetupLanesPositions()
        {
            lanesXPos = new float[lanesCounts];
            float distanceBetweenPoints = Mathf.Abs(movementAreaBoundaries.y - movementAreaBoundaries.x) / (lanesCounts-1);
            for (int i = 0; i < lanesCounts; i++)
            {
                lanesXPos[i] = movementAreaBoundaries.x + distanceBetweenPoints * i;
            }
            currentLane = GetMiddleLane();
            Vector3 startPosition = transform.position;
            startPosition.x = lanesXPos[currentLane];
            transform.position = startPosition;
        }
        private int GetMiddleLane()
        {
            return lanesXPos.Length / 2;
        }
        public void OnHit()
        {
            if (isRecovering) return;
            StartCoroutine(SpeedRecovery());
        }
        bool isRecovering;
        private IEnumerator SpeedRecovery()
        {
            isRecovering = true;
            float elapsedTime = 0f;
            float originalSpeed = speed;
            float targetSpeed = -levelSpeeds[0] * pushbackDistance; 

            while (elapsedTime < recoveryTime)
            {
                float t = elapsedTime / recoveryTime;
                float easedT = Mathf.SmoothStep(0, 1, Mathf.Sin(t * Mathf.PI * 0.5f));
                speed = Mathf.Lerp(targetSpeed, originalSpeed, easedT);

                elapsedTime += Time.deltaTime;
                yield return null;
            }
            speed = originalSpeed;
            isRecovering = false;
        }
        public void OnGameEnd()
        {
            isGameRunning = false;
            animator.Death();
        }
        public void Subscribe()
        {
            SwipeInputHandler.Singleton.OnHorizontalSwipDetected += OnSwipe;
            SwipeInputHandler.Singleton.OnTap += OnJumpInput;
            DifficultyManager.Singleton.OnDifficultyIncrease += IncreaseDifficulty;
        }
        private void OnSwipe(Swipe swipe)
        {
            if (swipe.direction == SwipeDirection.RIGHT)
            {
                if (currentLane == lanesXPos.Length - 1)
                {
                    HandleOutOfBoundriesSwipe(SwipeDirection.RIGHT);
                    return;
                }
                Move(SwipeDirection.RIGHT);
            }
            else
            {
                if (currentLane == 0)
                {
                    HandleOutOfBoundriesSwipe(SwipeDirection.LEFT);
                    return;
                }
                Move(SwipeDirection.LEFT);
            }
        }
        private void Move(SwipeDirection direction)
        {
            if (isSliding) return;
            int targetLane = currentLane + (direction == SwipeDirection.RIGHT ? 1 : -1);

            if (targetLane < 0 || targetLane >= lanesXPos.Length) return;

            currentLane = targetLane;
            StartCoroutine(SmoothMove(lanesXPos[currentLane],direction));
        }
        private IEnumerator SmoothMove(float targetX,SwipeDirection direction)
        {
            SoundsManager.Singleton.Woosh();
            float elapsedTime = 0f;
            Vector3 startPosition = rb.position;
            Vector3 targetPosition = new Vector3(targetX, startPosition.y, startPosition.z);
            animator.SetDirection(direction == SwipeDirection.RIGHT ? 1 : -1);
            float duration = laneChangeDuration / slideSpeed; 

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, t));
                yield return null;
            }

            rb.MovePosition(targetPosition);
            isSliding = false;
            animator.SetDirection(0);
        }
        private void HandleOutOfBoundriesSwipe(SwipeDirection direction)
        {
            // Nothing for now 
        }
        private void OnJumpInput()
        {
            if(IsGrounded())
                Jump();
        }
        private void Jump()
        {
            SoundsManager.Singleton.Woosh();
            rb.AddForce(Vector3.up*jumpPower,ForceMode.VelocityChange);
        }
        private bool IsGrounded()
        {
            return Physics.Raycast(groundChecker.position,Vector3.down, groundCheckDistance, groundMask);
        }
        private void IncreaseDifficulty(int level)
        {
            speed = levelSpeeds[level];
        }

    }
}