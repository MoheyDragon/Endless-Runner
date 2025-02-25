using UnityEngine;
using UnityEngine.Pool;
namespace EndlessRunner
{
    public class ChunksSpawner : Singletons<ChunksSpawner>
    {
        [SerializeField] CharacterMovementController character;
        PoolingManager<Chunk> chunksPool;
        [Header("Pooling")]
        [Space]
        [SerializeField] Chunk prefab;
        [SerializeField] Transform poolParent;
        [SerializeField] int defaultCapacity;
        [SerializeField] int maxCapacity;
        [Space]
        [Header("SpawnPositions")]
        [Space]
        [SerializeField] float chunkLength;
        [SerializeField] Vector3 startingSpawnPosition;
        private void Start()
        {
            chunksPool = new PoolingManager<Chunk>(prefab, poolParent, defaultCapacity, maxCapacity);
            SpawnInitalChuncks();
        }
        
        public void SpawnInitalChuncks()
        {
            for (int i = 0; i < defaultCapacity; i++)
                SpawnInitialChuncks(i);
        }
        public void SpawnInitialChuncks(int i)
        {
            Chunk newChunk= chunksPool.pool.Get();
            newChunk.SetCharacterMovement(character);
            Vector3 newSpawnPosition= startingSpawnPosition;
            newSpawnPosition.z += chunkLength*i;
            newChunk.transform.position = newSpawnPosition;
            lastSpawnedChunk = newChunk.transform;
        }
        Transform lastSpawnedChunk;
        public void SpawnChunk(Chunk chunkToRelease)
        {
            chunksPool.pool.Release(chunkToRelease);
            Chunk newChunk= chunksPool.pool.Get();
            Vector3 spawnPosition = lastSpawnedChunk.position;
            spawnPosition.z += chunkLength-character.Speed;
            newChunk.transform.position = spawnPosition;
            lastSpawnedChunk= newChunk.transform;
        }
    }
}