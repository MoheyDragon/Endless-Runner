using UnityEngine;
using MoheyPoolingSystem;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
namespace EndlessRunner
{
    public class ObjetsSpawnerManager : Singletons<ObjetsSpawnerManager>
    {
        PoolingManager<Obstacle> obstaclesPool;
        [SerializeField] PoolingData<Obstacle> obstaclesPoolData;

        PoolingManager<Collectable> collectablesPool;
        [SerializeField] PoolingData<Collectable> collectablesPoolData;
        [SerializeField] DifficultySpawnPatterns[] difficultySpawnPatterns;
        private int difficultyLevel;
        protected override void Awake()
        {
            base.Awake();
            obstaclesPool = new PoolingManager<Obstacle>(obstaclesPoolData);
            collectablesPool = new PoolingManager<Collectable>(collectablesPoolData);
        }
        private void Start()
        {
            DifficultyManager.Singleton.OnDifficultyIncrease += IncreaseSpawnRate;
        }
        public void IncreaseSpawnRate(int level)
        {
            difficultyLevel++;
            if (difficultyLevel == difficultySpawnPatterns.Length)
                difficultyLevel = difficultySpawnPatterns.Length;
        }
        public ChunkObjects HandleObstaclesSpawn(Transform[,] spawners,Transform chunk)
        {
            List<Obstacle> chunkObstacles = new List<Obstacle>();
            List<Collectable> chunkCollectables= new List<Collectable>();
            // So higher difficulty can spawn lesser difficulty aslso
            int difficultyRandom=Random.Range(0, difficultyLevel+1);
            SpawnPattern spawnPattern = difficultySpawnPatterns[difficultyRandom].spawnFormulas
                [Random.Range(0, difficultySpawnPatterns[difficultyRandom].spawnFormulas.Length)];
            chunk.name = spawnPattern.name;
            foreach(DirectionSpawnPoints direction in spawnPattern.DirectionSpawnPoints)
            {
                foreach (int position in direction.ObstaclesPositions)
                {
                    Obstacle newObstacle = obstaclesPool.pool.Get();
                    newObstacle.transform.SetParent(spawners[direction.directionIndex, position]);
                    newObstacle.transform.localPosition = Vector3.zero;
                    chunkObstacles.Add(newObstacle);
                }
                foreach (int position in direction.CollectablesPositions)
                {
                    Collectable newCollectable = collectablesPool.pool.Get();
                    newCollectable.transform.SetParent(spawners[direction.directionIndex, position]);
                    newCollectable.transform.localPosition = Vector3.zero;
                    chunkCollectables.Add(newCollectable);
                }
            }
            return new ChunkObjects(chunkCollectables,chunkObstacles);
        }
        public void HandleObstaclesRelease(ChunkObjects chunkObjects)
        {
            foreach (var obstacle in chunkObjects.obstacles)
                if(obstacle != null)
                    obstaclesPool.pool.Release(obstacle);

            foreach (var collectable in chunkObjects.collectables)
                if(collectable != null)
                    collectablesPool.pool.Release(collectable);
        }
        public void ReleaseCollectable(Collectable collectable)
        {
            collectablesPool.pool.Release(collectable);
        }
    }
}
