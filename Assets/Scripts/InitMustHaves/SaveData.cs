using System.Collections;
using System;

[System.Serializable]
public class SaveData 
{
    public int coins = 0;

    public String[] currentWeapons = {"Hunting Rifle", "Pistol"};

    public ArrayList unlockedWeapons = new ArrayList(){"Hunting Rifle", "Pistol"};
    public ArrayList lockedWeapons = new ArrayList(){"HMG", "SMG", "Assault Rifle", "Shotgun", "Sniper", "RPG", "Mini Gun", "Cross Bow", "Rail Gun", "Flame Thrower"};

}
