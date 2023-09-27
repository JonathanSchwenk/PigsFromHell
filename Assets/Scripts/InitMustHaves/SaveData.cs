using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData 
{
    public int coins = 0;

    public WeaponData[] currentWeapons = new WeaponData[4];
    public WeaponData activeWeapon = new WeaponData(); 
    public String currentSkin = "BasicMale";

    public List<WeaponData> unlockedWeapons = new List<WeaponData>();
    //public ArrayList lockedWeapons = new ArrayList(){"HMG", "SMG", "Assault Rifle", "Shotgun", "Sniper", "RPG", "Mini Gun", "Cross Bow", "Rail Gun", "Flame Thrower"};
    //public String[] totalNormalWeapons = {"Hunting Rifle", "Pistol", "HMG", "SMG", "Assault Rifle", "Shotgun", "Sniper"};
    public WeaponData[] totalNormalWeapons = new WeaponData[8];
    public WeaponData[] totalSpecialWeapons = new WeaponData[10];

    public String[] totalSkins = {"BasicFemale", "BasicMale", "Cowboy", "Hero", "Hoodie", "Jester", "Knight", "Ninja", "Robber", "Soldier"};
    public ArrayList unlockedSkins = new ArrayList(){"BasicFemale", "BasicMale"};

    public bool SFXOn = true;
    public bool musicOn = true;

    public int storyLevelSelected = 1;
    public string survivalMapSelected = "Neighborhood";
    public string gameMode = "Story";

    // int for levels completed, if you complete a level and the storyLevelSelected > the levelsCompleted then set levelsCompleted to storyLevelSelected
    public int levelsCompleted = 0;

    public String[] survivalLevelRecordsKeys = {"Neighborhood", "Highway", "Downtown", "Beach", "ShoppingCenter"};
    public int[] survivalLevelRecordsValues = {0, 0, 0, 0, 0};
}

