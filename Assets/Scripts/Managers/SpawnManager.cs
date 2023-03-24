using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class SpawnManager : MonoBehaviour , ISpawnManager
{
    public int numEnemies {get; set;}
    public int bankValue {get; set;}
    public bool canSpawn {get; set;}

    private int bankCap = 400;
    private int bankMultiplier = 7;



    private IGameManager gameManager; // GameManager and ServiceManager must be executed first



    void Awake() {
        gameManager = ServiceLocator.Resolve<IGameManager>();
        //saveManager = ServiceLocator.Resolve<ISaveManager>();
        

        // Subscribes to gamemanagers actions
        if (gameManager != null) {
            gameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
            gameManager.OnRoundChanged += GameManagerOnRoundChanged; 

        }
    }

    // Un-subscribes this script from the GameManager
    private void OnDestroy() {
        // Check for null
        if (gameManager != null) {
            gameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
            gameManager.OnRoundChanged -= GameManagerOnRoundChanged; 
        }
    }


    // if the GameState if GameOver or Idle then don't spawn
    private void GameManagerOnGameStateChanged(GameState state) { 
        if (state == GameState.Playing) {
            canSpawn = true;
        } else {
            canSpawn = false;
        }
    }


    private void GameManagerOnRoundChanged(int newRoundNum) {
        print("New Round");
        print(newRoundNum);

        if (newRoundNum % 5 == 0) {
            bankMultiplier += 1;
        }

        // Depending on the game managers round we set the bank, each individual spawner spawns enemies and subtracts from bank
        if (gameManager.RoundNum * bankMultiplier <= bankCap) {
            bankValue = gameManager.RoundNum * bankMultiplier; 
        } else {
            bankValue = 400;
        }
    }




// in update if I call gameManager.UpdateRound(), then above gets called with new updated round num that is set in the GameManager



    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }








}
