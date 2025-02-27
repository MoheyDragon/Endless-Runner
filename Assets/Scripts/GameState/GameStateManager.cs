using UnityEngine;
using MoheySwipeSystem;
using UnityEngine.SceneManagement;
namespace EndlessRunner
{
    public class GameStateManager : Singletons<GameStateManager>
    {
        GameState currentGameState;
        [SerializeField] CharacterMovementController character;
        private void Start()
        {
            currentGameState = GameState.GAME;
            SetState(GameState.START);
        }
        public void SetState(int state)
        {
            SetState((GameState)state);
        }
        public void SetState(GameState state)
        {
            currentGameState = state;
            switch (currentGameState)
            {
                case GameState.START:
                    EnterMainMenu();
                    break;
                case GameState.GAME:
                    OnMatchStart();
                    break;
                case GameState.END:
                    OnMatchEnd();
                    break;
            }
            UIManager.Singleton.SwitchToPage((int)state);
        }
        private void EnterMainMenu()
        {
            SwipeInputHandler.Singleton.enabled = false;
        }
        public void OnMatchStart()
        {
            SwipeInputHandler.Singleton.enabled = true;
            character.OnMatchBegan();
            SoundsManager.Singleton.PlayMusic();
        }
        public void OnMatchEnd()
        {
            SwipeInputHandler.Singleton.enabled = false;
            SoundsManager.Singleton.StopMusic();
            character.OnGameEnd();
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}
