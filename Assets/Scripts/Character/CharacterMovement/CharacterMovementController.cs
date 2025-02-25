using UnityEngine;
using System.Collections;
using MoheySwipeSystem;
namespace EndlessRunner
{
    public class CharacterMovementController : MonoBehaviour
    {
        CharacterAnimator characterAnimator;
        private Rigidbody rb;

        [SerializeField] Vector2 movementAreaBoundaries;
        [SerializeField] int lanesCounts;
        float[] lanesXPos;
        int currentLane;

        [Space]
        [SerializeField] float speed;
        [SerializeField] float slideSpeed;
        public float Speed => speed;

        [Space]
        [SerializeField] private float recoveryTime = 0.5f; 
        [SerializeField] private float pushbackDistance = 5;
        private float laneChangeDuration=0.5f;
        private bool isSliding;
        private bool isGameRunning;
        public bool IsGameRunning => isGameRunning;
        private void Awake()
        {
            characterAnimator = GetComponent<CharacterAnimator>();
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            SetupLanesPositions();
            SubscribeToSwipes();
            isGameRunning = true;
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
            float targetSpeed = -originalSpeed * pushbackDistance; 

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
            characterAnimator.Death();
        }
        public void SubscribeToSwipes()
        {
            SwipeInputHandler.Singleton.OnHorizontalSwipDetected += OnSwipe;
            SwipeInputHandler.Singleton.OnTap += Jump;
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
            StartCoroutine(SmoothMove(lanesXPos[currentLane]));
        }
        private IEnumerator SmoothMove(float targetX)
        {
            isGameRunning = true;
            float elapsedTime = 0f;
            Vector3 startPosition = rb.position;
            Vector3 targetPosition = new Vector3(targetX, startPosition.y, startPosition.z);

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
        }
        private void HandleOutOfBoundriesSwipe(SwipeDirection direction)
        {
            // Nothing for now 
        }
        private void Jump()
        {
            characterAnimator.Jump();
        }

    }
}