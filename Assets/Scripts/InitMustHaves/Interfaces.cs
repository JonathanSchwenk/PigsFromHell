using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IGameManager {
    GameState State {get; set;}
    void UpdateGameState(GameState state);
    void UpdateRound();
    void UpdateDrops(string drop);
    Action<GameState> OnGameStateChanged {get; set;}
    Action<int> OnRoundChanged {get; set;}
    int RoundNum {get; set;}
    int points {get; set;}
    GameObject activeBuyObject {get; set;}
    bool currentlyBuyingNewGun {get; set;}
    WeaponData[] currentWeapons {get; set;}
    WeaponData activeWeapon {get; set;}
    List<string> dropsList  {get; set;}
    Action<string> OnDropChanged {get; set;}

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

public interface IAudioManager {
    void PlaySFX(string name);
    void StopSFX(string name);
    void PlayMusic(string name);
    void StopMusic(string name);
}


public interface IAdManager {
    void LoadRewardedAd(int coinsEarned);
}
