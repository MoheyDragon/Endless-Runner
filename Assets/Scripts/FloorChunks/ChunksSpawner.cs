using UnityEngine;
using MoheyPoolingSystem;
namespace EndlessRunner
{
    public class ChunksSpawner : Singletons<ChunksSpawner>
    {
        [SerializeField] PoolingData<Chunk> chunksPoolData;
        PoolingManager<Chunk> chunksPool;
        [Space]
        [SerializeField] CharacterMovementController character;
        [Header("SpawnPositions")]
        [Space]
        [SerializeField] float chunkLength;
        [SerializeField] Vector3 startingSpawnPosition;
        private void Start()
        {
            chunksPool = new PoolingManager<Chunk>(chunksPoolData);
            SpawnInitalChuncks();
        }
        
        public void SpawnInitalChuncks()
        {
            for (int i = 0; i < chunksPoolData.defaultCapacity; i++)
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