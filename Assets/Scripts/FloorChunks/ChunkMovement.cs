using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EndlessRunner
{
    public class ChunkMovement : MonoBehaviour
    {
        [SerializeField] private float endPos;
        Vector3 moveVector;

        CharacterMovementController character;
        Chunk chunk;

        private void Start()
        {
            chunk = GetComponent<Chunk>();
            Vector3 moveVector = new Vector3(0, 0, -character.Speed);
        }
        public void SetCharacterMovementController(CharacterMovementController p_character)
        {
            character = p_character;
        }
        void FixedUpdate()
        {
            if (character.IsGameRunning)
            {
                moveVector.z = -character.Speed;
                transform.Translate(moveVector);
                if (transform.position.z < endPos)
                {
                    Clear();
                    ChunksSpawner.Singleton.SpawnChunk(chunk);
                }
            }
        }
        public void Clear()
        {
            chunk.Clear();
        }

    }
}