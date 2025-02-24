namespace MoheySwipeSystem
{
    public struct Swipe
    {
        public SwipeDirection direction;
        public float length;
        public float duration;
        public Swipe(SwipeDirection direction, float length, float duration)
        {
            this.direction = direction;
            this.length = length;
            this.duration = duration;
        }
    }
}