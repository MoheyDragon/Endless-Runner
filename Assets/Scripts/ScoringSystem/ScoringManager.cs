using UnityEngine;
namespace EndlessRunner
{
    public class ScoringManager : Singletons<ScoringManager>
    {
        int score;
        int highestScore;
        string highScorePlayerPrefName = "HighestScore";
        private void Start()
        {
            highestScore = PlayerPrefs.GetInt(highScorePlayerPrefName, 0);
            UIManager.Singleton.Setup(score,highestScore);
        }
        public void OnCollecting()
        {
            score++;
            bool updateHighScore = score >= highestScore;
            if (updateHighScore)
            {
                PlayerPrefs.SetInt(highScorePlayerPrefName, score);
                PlayerPrefs.Save();
            }
            UIManager.Singleton.UpdateScore(score,updateHighScore);
        }
    }
}