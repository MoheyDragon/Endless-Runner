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
        public void IncreaseSpawnRate()
        {
            difficultyLevel++;
            if (difficultyLevel == difficultySpawnPatterns.Length)
                difficultyLevel = difficultySpawnPatterns.Length;
        }
        public ChunkObjects HandleObstaclesSpawn(Transform[,] spawners)
        {
            List<Obstacle> chunkObstacles = new List<Obstacle>();
            List<Collectable> chunkCollectables= new List<Collectable>();
            SpawnPattern spawnPattern = difficultySpawnPatterns[difficultyLevel].spawnFormulas
                [Random.Range(0, difficultySpawnPatterns[difficultyLevel].spawnFormulas.Length)];
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
                obstaclesPool.pool.Release(obstacle);

            foreach (var collectable in chunkObjects.collectables)
                collectablesPool.pool.Release(collectable);
        }
        public void ReleaseCollectable(Collectable collectable)
        {
            collectablesPool.pool.Release(collectable);
        }
    }
}
