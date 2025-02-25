using System.Collections.Generic;
using UnityEngine;
namespace EndlessRunner
{
    [RequireComponent(typeof(ChunkMovement))]
    public class Chunk : MonoBehaviour,IPoolable
    {
        ChunkMovement chunkMovement;
        [SerializeField] bool isDebugging;
        [SerializeField] Transform spawnPointsParent;
        [SerializeField] Vector2 spawnXRange;
        [SerializeField] Vector2 spawnZRange;
        [SerializeField] private int DirectionsCount = 5;
        [SerializeField] private int SpawnCountInDirection = 6;
        Transform[,] spawners;

        private void Awake()
        {
            chunkMovement = GetComponent<ChunkMovement>();
            AssignSpawnPoints();
        }
        void Start()
        {
            chunkMovement.enabled = isDebugging;
        }
        public void SetCharacterMovement(CharacterMovementController character)
        {
            chunkMovement.SetCharacterMovementController(character);
        }
        public void Clear()
        {

        }
        public void AssignSpawnPoints()
        {
            spawners = new Transform[DirectionsCount, SpawnCountInDirection];
            float xStep = (spawnXRange.y - spawnXRange.x) / (DirectionsCount - 1);
            float zStep = (spawnZRange.y - spawnZRange.x) / (SpawnCountInDirection - 1);
            for (int directionIndex = 0; directionIndex < DirectionsCount; directionIndex++)
            {
                GameObject direction = new GameObject("direction "+directionIndex);
                direction.transform.SetParent(spawnPointsParent);
                direction.transform.localScale = Vector3.one;
                for (int lineIndex = 0; lineIndex < SpawnCountInDirection; lineIndex++)
                {
                    GameObject spawnPoint = new GameObject("SpawnPoint "+lineIndex);

                    spawnPoint.transform.SetParent(direction.transform);
                    spawnPoint.transform.localPosition = new Vector3(spawnXRange.x + xStep * directionIndex, 0, spawnZRange.x + zStep * lineIndex);
                    spawnPoint.transform.localScale = Vector3.one;

                    spawners[directionIndex, lineIndex] = spawnPoint.transform;
                }
            }
        }

        // IPoolable Functions
        List<Obstacle> currentObstacles=new List<Obstacle>();
        public void OnGet()
        {
            currentObstacles= ObstaclesManager.Singleton.HandleObstaclesSpawn(spawners);
        }

        public void OnRelease()
        {
            ObstaclesManager.Singleton.HandleObstaclesRelease(currentObstacles);
        }

        public void OnDestroy()
        {
        }
    }
}
