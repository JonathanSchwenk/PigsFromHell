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
    public WeaponData[] totalSpecialWeapons = new WeaponData[5];

    public String[] totalSkins = {"BasicFemale", "BasicMale", "Cowboy", "Hero", "Hoodie", "Jester", "Knight", "Ninja", "Robber", "Soldier"};
    public ArrayList unlockedSkins= new ArrayList(){"BasicFemale", "BasicMale"};
}
