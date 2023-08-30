using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class SpawnManager : MonoBehaviour , ISpawnManager
{
    [SerializeField] private GameObject startNewRoundButton;

    public int numEnemies {get; set;}
    public int bankValue {get; set;}
    public bool canSpawn {get; set;}

    private int bankCap = 400; // Max enemies for a level 
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

        if (newRoundNum % 5 == 0) {
            bankMultiplier += 1;
        }

        // Depending on the game managers round we set the bank, each individual spawner spawns enemies and subtracts from bank
        if (gameManager.RoundNum * bankMultiplier <= bankCap) {
            bankValue = gameManager.RoundNum * bankMultiplier; 
        } else {
            bankValue = bankCap;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        /*
            Issue: There are no crawlers so its impossible to wait inbetween rounds

            Could start a coroutine here that delays this
            Could do a button to start a new round
        */

        // if bank value == 0 and there are no enemies left then rounds over, and update the UI and values in game manager
        if (bankValue == 0 & numEnemies == 0) {
            // Drops reset here because I would do it on the new round button pressed but I want the drops to go away before
            gameManager.UpdateDrops("Clear");

            startNewRoundButton.SetActive(true);
        }
    }


    public void StartRound() {
        // Update the round number in the game manager
        gameManager.UpdateRound();

        // Update the UI
        startNewRoundButton.SetActive(false);
    }






}
