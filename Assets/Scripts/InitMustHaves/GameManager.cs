using UnityEngine;
using System;
using Dorkbots.ServiceLocatorTools;
using System.Collections.Generic;

public class GameManager : MonoBehaviour, IGameManager
{
    public GameState State {get; set;}
    public Action<GameState> OnGameStateChanged {get; set;}
    public Action<int> OnRoundChanged {get; set;}
    public int RoundNum {get; set;}
    public int points {get; set;}
    public GameObject activeBuyObject {get; set;}
    public bool currentlyBuyingNewGun {get; set;}
    public WeaponData[] currentWeapons {get; set;}
    public WeaponData activeWeapon {get; set;}
    public List<string> dropsList {get; set;} // Might have prob with this bc its setting it to a new list now (Init in start in here)
    public Action<string> OnDropChanged {get; set;}
    public float enemySpeed {get; set;}



    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameplayUICanvas;
    [SerializeField] private GameObject gameplayControlsCanvas;



    private ISaveManager saveManager;





    void OnDestroy() {
        OnGameStateChanged = null;
        OnRoundChanged = null;
    }

    // Sets the state to ready when the game starts 
    void Start() {
        UpdateGameState(GameState.Playing);
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        // RoundNum gets set to 0 and then the round gets updated which increases the round number and invokes all subscribed actions
        RoundNum = 0; // 0 for actual game
        UpdateRound();

        // CurrentWeapons get set as well as the active weapon
        currentWeapons = new WeaponData[3];
        InitCurWeapons();

        activeWeapon = new WeaponData();
        InitActiveWeapon();

        // Sets the active to the current primary at the start to refrence the pointer so their connected 
        activeWeapon = currentWeapons[0];

        dropsList = new List<string>();
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

        enemySpeed = 1.5f + (RoundNum * 0.1f);

        if (enemySpeed >= 3.2f) {
            enemySpeed = 3.2f;
        }

        // Null checker then calls the action for anthing subscribed to it
        OnRoundChanged?.Invoke(RoundNum);
    }

    public void UpdateDrops(string drop) {

        // If drop == stopInstaKill then it removes intaKill
        if (drop == "StopInstaKill") {
            if (dropsList.Contains("InstaKill")) {
                dropsList.Remove("InstaKill");
            }
        } else if (drop == "Clear") {
            if (dropsList.Count > 0) {
                dropsList.Clear();
            }
        } else if (dropsList.Contains(drop) != true) {
            dropsList.Add(drop);
        }

        // Null checker then calls the action for anthing subscribed to it
        OnDropChanged?.Invoke(drop);
    }

    private void HandelGameOverState() {
        // Stops the time
        Time.timeScale = 0;

        // Changes canvases
        gameplayUICanvas.SetActive(false);
        gameplayControlsCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
    } 




    private void InitCurWeapons() {
        currentWeapons[0] = new WeaponData();
        currentWeapons[0].name = saveManager.saveData.currentWeapons[0].name;
        currentWeapons[0].firePointPos = saveManager.saveData.currentWeapons[0].firePointPos;
        currentWeapons[0].fireRate = saveManager.saveData.currentWeapons[0].fireRate;
        currentWeapons[0].movementSpeed = saveManager.saveData.currentWeapons[0].movementSpeed;
        currentWeapons[0].bulletForce = saveManager.saveData.currentWeapons[0].bulletForce;
        currentWeapons[0].magSize = saveManager.saveData.currentWeapons[0].magSize;
        currentWeapons[0].totalAmmo = saveManager.saveData.currentWeapons[0].totalAmmo;
        currentWeapons[0].impact = saveManager.saveData.currentWeapons[0].impact;
        currentWeapons[0].damage = saveManager.saveData.currentWeapons[0].damage;
        currentWeapons[0].bulletsInMag = saveManager.saveData.currentWeapons[0].bulletsInMag;
        currentWeapons[0].reserveAmmo = saveManager.saveData.currentWeapons[0].reserveAmmo;
        currentWeapons[0].starValue = saveManager.saveData.currentWeapons[0].starValue;

        currentWeapons[1] = new WeaponData();
        currentWeapons[1].name = saveManager.saveData.currentWeapons[1].name;
        currentWeapons[1].firePointPos = saveManager.saveData.currentWeapons[1].firePointPos;
        currentWeapons[1].fireRate = saveManager.saveData.currentWeapons[1].fireRate;
        currentWeapons[1].movementSpeed = saveManager.saveData.currentWeapons[1].movementSpeed;
        currentWeapons[1].bulletForce = saveManager.saveData.currentWeapons[1].bulletForce;
        currentWeapons[1].magSize = saveManager.saveData.currentWeapons[1].magSize;
        currentWeapons[1].totalAmmo = saveManager.saveData.currentWeapons[1].totalAmmo;
        currentWeapons[1].impact = saveManager.saveData.currentWeapons[1].impact;
        currentWeapons[1].damage = saveManager.saveData.currentWeapons[1].damage;
        currentWeapons[1].bulletsInMag = saveManager.saveData.currentWeapons[1].bulletsInMag;
        currentWeapons[1].reserveAmmo = saveManager.saveData.currentWeapons[1].reserveAmmo;
        currentWeapons[1].starValue = saveManager.saveData.currentWeapons[1].starValue;

        currentWeapons[2] = new WeaponData();
        currentWeapons[2].name = saveManager.saveData.currentWeapons[2].name;
        currentWeapons[2].firePointPos = saveManager.saveData.currentWeapons[2].firePointPos;
        currentWeapons[2].fireRate = saveManager.saveData.currentWeapons[2].fireRate;
        currentWeapons[2].movementSpeed = saveManager.saveData.currentWeapons[2].movementSpeed;
        currentWeapons[2].bulletForce = saveManager.saveData.currentWeapons[2].bulletForce;
        currentWeapons[2].magSize = saveManager.saveData.currentWeapons[2].magSize;
        currentWeapons[2].totalAmmo = saveManager.saveData.currentWeapons[2].totalAmmo;
        currentWeapons[2].impact = saveManager.saveData.currentWeapons[2].impact;
        currentWeapons[2].damage = saveManager.saveData.currentWeapons[2].damage;
        currentWeapons[2].bulletsInMag = saveManager.saveData.currentWeapons[2].bulletsInMag;
        currentWeapons[2].reserveAmmo = saveManager.saveData.currentWeapons[2].reserveAmmo;
        currentWeapons[2].starValue = saveManager.saveData.currentWeapons[2].starValue;
    }


    private void InitActiveWeapon() {
        activeWeapon.name = saveManager.saveData.activeWeapon.name;
        activeWeapon.firePointPos = saveManager.saveData.activeWeapon.firePointPos;
        activeWeapon.fireRate = saveManager.saveData.activeWeapon.fireRate;
        activeWeapon.movementSpeed = saveManager.saveData.activeWeapon.movementSpeed;
        activeWeapon.bulletForce = saveManager.saveData.activeWeapon.bulletForce;
        activeWeapon.magSize = saveManager.saveData.activeWeapon.magSize;
        activeWeapon.totalAmmo = saveManager.saveData.activeWeapon.totalAmmo;
        activeWeapon.impact = saveManager.saveData.activeWeapon.impact;
        activeWeapon.damage = saveManager.saveData.activeWeapon.damage;
        activeWeapon.bulletsInMag = saveManager.saveData.activeWeapon.bulletsInMag;
        activeWeapon.reserveAmmo = saveManager.saveData.activeWeapon.reserveAmmo;
        activeWeapon.starValue = saveManager.saveData.activeWeapon.starValue;
    }
}




// GameState enum (basically a definition)
public enum GameState {
    Idle,
    Playing,
    GameOver
}


