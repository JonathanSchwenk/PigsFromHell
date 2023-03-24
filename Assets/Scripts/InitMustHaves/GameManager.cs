using UnityEngine;
using System;
using Dorkbots.ServiceLocatorTools;


public class GameManager : MonoBehaviour, IGameManager
{
    public GameState State {get; set;}
    public Action<GameState>OnGameStateChanged {get; set;}
    public Action<int>OnRoundChanged {get; set;}


    public int RoundNum {get; set;}



    private ISaveManager saveManager;





    void OnDestroy() {
        OnGameStateChanged = null;
        OnRoundChanged = null;
    }

    // Sets the state to ready when the game starts 
    void Start() {
        UpdateGameState(GameState.Playing);

        // RoundNum gets set to 0 and then the round gets updated which increases the round number and invokes all subscribed actions
        RoundNum = 0; // 0 for actual game
        UpdateRound();


        //saveManager = ServiceLocator.Resolve<ISaveManager>();
        //saveManager.Load();
    }

    // Update game state function
    public void UpdateGameState(GameState newState) {
        State = newState;

        // Swtich statement that deals with each possible state 
        switch(newState) {
            case GameState.Idle:
                
                break;
            case GameState.Playing:
                
                break;
            case GameState.GameOver:
                HandelGameOverState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        
        // Null checker then calls the action for anthing subscribed to it
        OnGameStateChanged?.Invoke(newState);
    }

    public void UpdateRound() {
        // Increased thet round number
        RoundNum += 1;

        // Null checker then calls the action for anthing subscribed to it
        OnRoundChanged?.Invoke(RoundNum);
    }

    private void HandelGameOverState() {
        
    } 
}





// GameState enum (basically a definition)
public enum GameState {
    Idle,
    Playing,
    GameOver
}


