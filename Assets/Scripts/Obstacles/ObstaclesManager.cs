using UnityEngine;
using MoheyPoolingSystem;
using UnityEngine.TextCore.Text;
using System.Collections.Generic;
namespace EndlessRunner
{
    public class ObstaclesManager : Singletons<ObstaclesManager>
    {
        PoolingManager<Obstacle> obstaclesPool;
        [SerializeField] PoolingData<Obstacle> obstaclesPoolData;
        [SerializeField] DifficultySpawnPatterns[] difficultySpawnPatterns;
        private int difficultyLevel;
        protected override void Awake()
        {
            base.Awake();
            obstaclesPool = new PoolingManager<Obstacle>(obstaclesPoolData);
        }
        public void IncreaseSpawnRate()
        {
            difficultyLevel++;
            if (difficultyLevel == difficultySpawnPatterns.Length)
                difficultyLevel = difficultySpawnPatterns.Length;
        }
        public List<Obstacle> HandleObstaclesSpawn(Transform[,] spawners)
        {
            List<Obstacle> chunkObstacles = new List<Obstacle>();

            SpawnPattern spawnPattern = difficultySpawnPatterns[difficultyLevel].spawnFormulas
                [Random.Range(0, difficultySpawnPatterns[difficultyLevel].spawnFormulas.Length)];
            foreach(DirectionSpawnPoints direction in spawnPattern.DirectionSpawnPoints)
            {
                foreach (int position in direction.Positions)
                {
                    Obstacle newObstacle = obstaclesPool.pool.Get();
                    newObstacle.transform.SetParent(spawners[direction.directionIndex, position]);
                    newObstacle.transform.localPosition = Vector3.zero;
                    chunkObstacles.Add(newObstacle);
                }
            }
            return chunkObstacles;
        }
        public void HandleObstaclesRelease(List<Obstacle> chunkObstacles)
        {
            foreach (var obstacle in chunkObstacles)
                obstaclesPool.pool.Release(obstacle);
        }
    }
}