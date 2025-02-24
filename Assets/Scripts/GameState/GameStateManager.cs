using UnityEngine;
using MoheySwipeSystem;
using UnityEngine.Events;
namespace EndlessRunner
{
    public class GameStateManager : Singletons<GameStateManager>
    {
        GameState currentGameState;
        private void Start()
        {
            currentGameState = GameState.MAINMENU;
            SetState(currentGameState);
        }
        public void SetState(GameState state)
        {
            switch (currentGameState)
            {
                case GameState.MAINMENU:
                    break;
                case GameState.GAME:
                    break;
                case GameState.END:
                    break;
            }
        }
        private void EnterMainMenu()
        {
            SwipeInputHandler.Singleton.enabled = false;
            CharacterSelector.Singleton.enabled = true;
        }
        public void OnMatchStart()
        {
            SwipeInputHandler.Singleton.enabled = true;
        }
        public void OnMatchEnd()
        {
            SwipeInputHandler.Singleton.enabled = false;
        }
    }
}