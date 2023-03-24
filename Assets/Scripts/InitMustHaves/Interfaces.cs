using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IGameManager {
    GameState State {get; set;}
    void UpdateGameState(GameState state);
    Action<GameState>OnGameStateChanged {get; set;}
    Action<int>OnRoundChanged {get; set;}
    int RoundNum {get; set;}

}

public interface IObjectPooler {
    List<Pool> Pools {get; set;}
    Dictionary<string, Queue<GameObject>> poolDictionary {get; set;}

    GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation);
}

public interface ISaveManager {
    SaveData saveData {get; set;}

    void Save();
    void Load();
    void DeleteSavedData();
}

public interface ISpawnManager {
    int numEnemies {get; set;}
    int bankValue {get; set;}
    bool canSpawn {get; set;}
}

