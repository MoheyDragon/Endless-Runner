using EndlessRunner;
using System;
using UnityEngine;

public class DifficultyManager : Singletons<DifficultyManager>
{
    public Action<int> OnDifficultyIncrease;
    [SerializeField] int[] chunksCountToNextLevel;
    int currentDifficultyLevel;
    int currentChunkCount;
    private void OnEnable()
    {
        ChunkMovement.OnChunkReachedEnd += StepIncreaseDifficulty;
    }
    private void StepIncreaseDifficulty()
    {
        currentChunkCount++;
        if (currentChunkCount >= chunksCountToNextLevel[currentDifficultyLevel])
            IncreaseDiificulty();
    }
    private void IncreaseDiificulty()
    {
        currentDifficultyLevel++;
        OnDifficultyIncrease?.Invoke(currentDifficultyLevel);
        if (currentDifficultyLevel == chunksCountToNextLevel.Length)
            gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        ChunkMovement.OnChunkReachedEnd -= StepIncreaseDifficulty;
    }
}
