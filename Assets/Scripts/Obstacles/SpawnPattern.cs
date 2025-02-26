using UnityEngine;
namespace EndlessRunner
{
    [CreateAssetMenu(fileName = "spawnPattern", menuName = "SpawnPatterns")]
    public class SpawnPattern : ScriptableObject
    {
        public DirectionSpawnPoints[] DirectionSpawnPoints;
    }
}