using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IGameManager {
    GameState State {get; set;}
    void UpdateGameState(GameState state);
    void UpdateRound();
    Action<GameState>OnGameStateChanged {get; set;}
    Action<int>OnRoundChanged {get; set;}
    int RoundNum {get; set;}
    int points {get; set;}
    GameObject activeBuyObject {get; set;}
    WeaponData[] currentWeapons {get; set;}
    WeaponData activeWeapon {get; set;}

}

public interface IObjectPooler {
    List<Pool> Pools {get; set;}
    Dictionary<string, Queue<GameObject>> poolDictionary {get; set;}

    GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation);
}

public interface ISaveManager {
    SaveData saveData {get; set;}
    Action<int>OnSave {get; set;}

    void Save();
    void Load();
    void DeleteSavedData();
}

public interface ISpawnManager {
    int numEnemies {get; set;}
    int bankValue {get; set;}
    bool canSpawn {get; set;}
}

