using System;
using UnityEngine;
namespace MoheySwipeSystem
{
    public class SwipeInputHandler : Singletons<SwipeInputHandler>
    {
        public Action OnTap;
        public Action<Swipe> OnHorizontalSwipDetected;
        public Action<Swipe> OnVerticalSwipDetected;
        [SerializeField] float swipeDurationThreshold;
        [Space]
        [SerializeField] float verticalSwipeThreshold;
        [SerializeField] float horizontalSwipeThreshold;
        [HideInInspector]
        public bool isTouching;
        Vector2 startPos;
        Vector2 endPos;
        float touchingStartingTime;
        void Update()
        {
            GetInputData();
        }
        private void GetInputData()
        {
            if (!isTouching)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isTouching = true;
                    startPos = Input.mousePosition;
                    touchingStartingTime = Time.time;
                }
            }
            if (isTouching && Input.GetMouseButtonUp(0))
            {
                endPos = Input.mousePosition;
                CheckSwiping();
                isTouching = false;
            }
        }
        private void CheckSwiping()
        {
            float horizontalSwipeDistance = Mathf.Abs(endPos.x - startPos.x);
            float verticalSwipeDistance = Mathf.Abs(endPos.y - startPos.y);
            float swipeDuration = Time.time - touchingStartingTime;

            if (horizontalSwipeDistance > horizontalSwipeThreshold)
            {
                SwipeDirection direction = endPos.x > startPos.x ? SwipeDirection.RIGHT : SwipeDirection.LEFT;
                OnHorizontalSwipDetected?.Invoke(new Swipe(direction, horizontalSwipeDistance, swipeDuration));
            }
            else if (verticalSwipeDistance > verticalSwipeThreshold)
            {
                SwipeDirection direction = endPos.y > startPos.y ? SwipeDirection.UP: SwipeDirection.DOWN;
                OnVerticalSwipDetected?.Invoke(new Swipe(direction, verticalSwipeDistance, swipeDuration));
            }
            else if(swipeDuration < swipeDurationThreshold)
            {
                OnTap?.Invoke();
            }
        }
    }
}