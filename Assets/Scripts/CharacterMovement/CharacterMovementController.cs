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
        private float laneChangeDuration=0.5f;
        private bool isMoving;
        private void Awake()
        {
            characterAnimator = GetComponent<CharacterAnimator>();
            rb = GetComponent<Rigidbody>();
        }
        private void Start()
        {
            SetupLanesPositions();
            SubscribeToSwipes();
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
        public void SubscribeToSwipes()
        {
            SwipeInputHandler.Singleton.OnHorizontalSwipDetected += OnSwipe;
            SwipeInputHandler.Singleton.OnTap += Jump;
        }
        private int GetMiddleLane()
        {
            return lanesXPos.Length / 2;
        }
        private void Jump()
        {
            characterAnimator.Jump();
        }
        private void OnSwipe(Swipe swipe)
        {
            if (swipe.direction == SwipeDirection.RIGHT)
            {

                if (currentLane == lanesXPos.Length - 1)
                {
                    HandleOutOfBoundriesSwipe();
                    return;
                }
                Move(SwipeDirection.RIGHT);
            }
            else
            {

                if (currentLane == 0)
                {
                    HandleOutOfBoundriesSwipe();
                    return;
                }
                Move(SwipeDirection.LEFT);
            }
        }
        private void Move(SwipeDirection direction)
        {
            if (isMoving) return;

            int targetLane = currentLane + (direction == SwipeDirection.RIGHT ? 1 : -1);

            if (targetLane < 0 || targetLane >= lanesXPos.Length) return;

            currentLane = targetLane;
            StartCoroutine(SmoothMove(lanesXPos[currentLane]));
        }
        private IEnumerator SmoothMove(float targetX)
        {
            isMoving = true;
            float elapsedTime = 0f;
            Vector3 startPosition = rb.position;
            Vector3 targetPosition = new Vector3(targetX, startPosition.y, startPosition.z);

            float duration = laneChangeDuration / speed; 

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                rb.MovePosition(Vector3.Lerp(startPosition, targetPosition, t));
                yield return null;
            }

            rb.MovePosition(targetPosition);
            isMoving = false;
        }
        private void HandleOutOfBoundriesSwipe()
        {
            // Nothing for now 
        }
    }
}