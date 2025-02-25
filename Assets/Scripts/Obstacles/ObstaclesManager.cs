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
        void Start()
        {
            obstaclesPool = new PoolingManager<Obstacle>(obstaclesPoolData);
        }
        int deleteMe;
        public List<Obstacle> HandleObstaclesSpawn(Transform[,]spawners)
        {
            List<Obstacle> chunkObstacles=new List<Obstacle>();
            for (int i = 0; i < spawners.GetLength(1); i++)
            {
                Obstacle newObstacle = obstaclesPool.pool.Get();
                newObstacle.transform.SetParent(spawners[deleteMe,i]);
                newObstacle.transform.localPosition = Vector3.zero;
                chunkObstacles.Add(newObstacle);
            }
            deleteMe++;
            if(deleteMe==spawners.GetLength(0))deleteMe=0;
            return chunkObstacles;
        }
        public void HandleObstaclesRelease(List<Obstacle> chunkObstacles)
        {
            foreach (var obstacle in chunkObstacles)
                obstaclesPool.pool.Release(obstacle);
        }

    }
}