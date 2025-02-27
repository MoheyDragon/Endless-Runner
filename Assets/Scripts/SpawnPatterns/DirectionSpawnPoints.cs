namespace EndlessRunner
{
    [System.Serializable]
    public struct DirectionSpawnPoints
    {
        public int directionIndex;
        public int[] ObstaclesPositions;
        public int[] CollectablesPositions;
    }
}