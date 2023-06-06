using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class InitInGameWeapons : MonoBehaviour
{

    private ISaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        // Resetting the bullets and ammo for the current weapons in case they got saved as something different after the game ended. 

        //InitTotalWeapons();

        // Normal guns
        for (int i = 0; i < saveManager.saveData.currentWeapons.Length; i++) {
            for (int j = 0; j < saveManager.saveData.totalNormalWeapons.Length; j++) {
                if (saveManager.saveData.currentWeapons[i].name == saveManager.saveData.totalNormalWeapons[j].name) {
                    saveManager.saveData.currentWeapons[i] = saveManager.saveData.totalNormalWeapons[j];
                    //print(saveManager.saveData.currentWeapons[i].reserveAmmo);
                }
            }
        }

        // Special guns
        for (int i = 0; i < saveManager.saveData.currentWeapons.Length; i++) {
            for (int j = 0; j < saveManager.saveData.totalSpecialWeapons.Length; j++) {
                if (saveManager.saveData.currentWeapons[i].name == saveManager.saveData.totalSpecialWeapons[j].name) {
                    saveManager.saveData.currentWeapons[i] = saveManager.saveData.totalSpecialWeapons[j];
                }
            }
        }

        saveManager.saveData.activeWeapon = saveManager.saveData.currentWeapons[0];

        saveManager.Save();
        
    }


    private void InitTotalWeapons() {
        // Normal Guns
        saveManager.saveData.totalNormalWeapons[0] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[0].name = "Hunting Rifle";
        saveManager.saveData.totalNormalWeapons[0].firePointPos = new Vector3(0.315f, 0.625f, 1.2f);
        saveManager.saveData.totalNormalWeapons[0].fireRate = 0.5f;
        saveManager.saveData.totalNormalWeapons[0].movementSpeed = 2;
        saveManager.saveData.totalNormalWeapons[0].bulletForce = 30;
        saveManager.saveData.totalNormalWeapons[0].magSize = 10;
        saveManager.saveData.totalNormalWeapons[0].totalAmmo = 100;
        saveManager.saveData.totalNormalWeapons[0].impact = 1;
        saveManager.saveData.totalNormalWeapons[0].damage = 1;
        saveManager.saveData.totalNormalWeapons[0].bulletsInMag = 10;
        saveManager.saveData.totalNormalWeapons[0].reserveAmmo = 100;

        saveManager.saveData.totalNormalWeapons[1] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[1].name = "Pistol";
        saveManager.saveData.totalNormalWeapons[1].firePointPos = new Vector3(0.315f, 0.625f, 0.65f);
        saveManager.saveData.totalNormalWeapons[1].fireRate = 0.25f;
        saveManager.saveData.totalNormalWeapons[1].movementSpeed = 3;
        saveManager.saveData.totalNormalWeapons[1].bulletForce = 15;
        saveManager.saveData.totalNormalWeapons[1].magSize = 12;
        saveManager.saveData.totalNormalWeapons[1].totalAmmo = 120;
        saveManager.saveData.totalNormalWeapons[1].impact = 1;
        saveManager.saveData.totalNormalWeapons[1].damage = 0.5f;
        saveManager.saveData.totalNormalWeapons[1].bulletsInMag = 12;
        saveManager.saveData.totalNormalWeapons[1].reserveAmmo = 120;

        saveManager.saveData.totalNormalWeapons[2] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[2].name = "Assault Rifle";
        saveManager.saveData.totalNormalWeapons[2].firePointPos = new Vector3(0.31f, 0.656f, 1.325f);
        saveManager.saveData.totalNormalWeapons[2].fireRate = 0.15f;
        saveManager.saveData.totalNormalWeapons[2].movementSpeed = 2;
        saveManager.saveData.totalNormalWeapons[2].bulletForce = 25;
        saveManager.saveData.totalNormalWeapons[2].magSize = 40;
        saveManager.saveData.totalNormalWeapons[2].totalAmmo = 400;
        saveManager.saveData.totalNormalWeapons[2].impact = 1;
        saveManager.saveData.totalNormalWeapons[2].damage = 0.5f;
        saveManager.saveData.totalNormalWeapons[2].bulletsInMag = 40;
        saveManager.saveData.totalNormalWeapons[2].reserveAmmo = 400;

        saveManager.saveData.totalNormalWeapons[3] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[3].name = "HMG";
        saveManager.saveData.totalNormalWeapons[3].firePointPos = new Vector3(0.325f, 0.595f, 0.75f);
        saveManager.saveData.totalNormalWeapons[3].fireRate = 0.1f;
        saveManager.saveData.totalNormalWeapons[3].movementSpeed = 2.5f;
        saveManager.saveData.totalNormalWeapons[3].bulletForce = 20;
        saveManager.saveData.totalNormalWeapons[3].magSize = 25;
        saveManager.saveData.totalNormalWeapons[3].totalAmmo = 250;
        saveManager.saveData.totalNormalWeapons[3].impact = 1;
        saveManager.saveData.totalNormalWeapons[3].damage = 0.25f;
        saveManager.saveData.totalNormalWeapons[3].bulletsInMag = 25;
        saveManager.saveData.totalNormalWeapons[3].reserveAmmo = 250;

        saveManager.saveData.totalNormalWeapons[4] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[4].name = "SMG";
        saveManager.saveData.totalNormalWeapons[4].firePointPos = new Vector3(0.325f, 0.6f, 0.85f);
        saveManager.saveData.totalNormalWeapons[4].fireRate = 0.1f;
        saveManager.saveData.totalNormalWeapons[4].movementSpeed = 2.25f;
        saveManager.saveData.totalNormalWeapons[4].bulletForce = 20;
        saveManager.saveData.totalNormalWeapons[4].magSize = 30;
        saveManager.saveData.totalNormalWeapons[4].totalAmmo = 300;
        saveManager.saveData.totalNormalWeapons[4].impact = 1;
        saveManager.saveData.totalNormalWeapons[4].damage = 0.25f;
        saveManager.saveData.totalNormalWeapons[4].bulletsInMag = 30;
        saveManager.saveData.totalNormalWeapons[4].reserveAmmo = 300;

        saveManager.saveData.totalNormalWeapons[5] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[5].name = "Sniper";
        saveManager.saveData.totalNormalWeapons[5].firePointPos = new Vector3(0.325f, 0.575f, 1.5f);
        saveManager.saveData.totalNormalWeapons[5].fireRate = 0.75f;
        saveManager.saveData.totalNormalWeapons[5].movementSpeed = 1.5f;
        saveManager.saveData.totalNormalWeapons[5].bulletForce = 35;
        saveManager.saveData.totalNormalWeapons[5].magSize = 10;
        saveManager.saveData.totalNormalWeapons[5].totalAmmo = 100;
        saveManager.saveData.totalNormalWeapons[5].impact = 2;
        saveManager.saveData.totalNormalWeapons[5].damage = 1;
        saveManager.saveData.totalNormalWeapons[5].bulletsInMag = 10;
        saveManager.saveData.totalNormalWeapons[5].reserveAmmo = 100;

        saveManager.saveData.totalNormalWeapons[6] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[6].name = "Shotgun";
        saveManager.saveData.totalNormalWeapons[6].firePointPos = new Vector3(0.3f, 0.625f, 1.075f);
        saveManager.saveData.totalNormalWeapons[6].fireRate = 0.75f;
        saveManager.saveData.totalNormalWeapons[6].movementSpeed = 2.25f;
        saveManager.saveData.totalNormalWeapons[6].bulletForce = 20;
        saveManager.saveData.totalNormalWeapons[6].magSize = 8;
        saveManager.saveData.totalNormalWeapons[6].totalAmmo = 80;
        saveManager.saveData.totalNormalWeapons[6].impact = 1;
        saveManager.saveData.totalNormalWeapons[6].damage = 0.25f;
        saveManager.saveData.totalNormalWeapons[6].bulletsInMag = 8;
        saveManager.saveData.totalNormalWeapons[6].reserveAmmo = 80;

        saveManager.saveData.totalNormalWeapons[7] = new WeaponData();
        saveManager.saveData.totalNormalWeapons[7].name = "Knife";
        saveManager.saveData.totalNormalWeapons[7].firePointPos = new Vector3(0f, 0f, 0f);
        saveManager.saveData.totalNormalWeapons[7].fireRate = 0.3f;
        saveManager.saveData.totalNormalWeapons[7].movementSpeed = 3f;
        saveManager.saveData.totalNormalWeapons[7].bulletForce =  -1;
        saveManager.saveData.totalNormalWeapons[7].magSize = -1;
        saveManager.saveData.totalNormalWeapons[7].totalAmmo = -1;
        saveManager.saveData.totalNormalWeapons[7].impact = -1;
        saveManager.saveData.totalNormalWeapons[7].damage = -1f;
        saveManager.saveData.totalNormalWeapons[7].bulletsInMag = -1;
        saveManager.saveData.totalNormalWeapons[7].reserveAmmo = -1;
        

        // Special Guns
        saveManager.saveData.totalSpecialWeapons[0] = new WeaponData();
        saveManager.saveData.totalSpecialWeapons[0].name = "Flame Thrower";
        saveManager.saveData.totalSpecialWeapons[0].firePointPos = new Vector3(0.3f, 0.625f, 1.05f);
        saveManager.saveData.totalSpecialWeapons[0].fireRate = 0.1f;
        saveManager.saveData.totalSpecialWeapons[0].movementSpeed = 1.75f;
        saveManager.saveData.totalSpecialWeapons[0].bulletForce = 10;
        saveManager.saveData.totalSpecialWeapons[0].magSize = 100;
        saveManager.saveData.totalSpecialWeapons[0].totalAmmo = 800;
        saveManager.saveData.totalSpecialWeapons[0].impact = 1;
        saveManager.saveData.totalSpecialWeapons[0].damage = 0.5f;
        saveManager.saveData.totalSpecialWeapons[0].bulletsInMag = 100;
        saveManager.saveData.totalSpecialWeapons[0].reserveAmmo = 800;

        saveManager.saveData.totalSpecialWeapons[1] = new WeaponData();
        saveManager.saveData.totalSpecialWeapons[1].name = "Rail Gun";
        saveManager.saveData.totalSpecialWeapons[1].firePointPos = new Vector3(0.325f, 0.625f, 1.25f);
        saveManager.saveData.totalSpecialWeapons[1].fireRate = 1f;
        saveManager.saveData.totalSpecialWeapons[1].movementSpeed = 1.75f;
        saveManager.saveData.totalSpecialWeapons[1].bulletForce = 80;
        saveManager.saveData.totalSpecialWeapons[1].magSize = 2;
        saveManager.saveData.totalSpecialWeapons[1].totalAmmo = 16;
        saveManager.saveData.totalSpecialWeapons[1].impact = 0;
        saveManager.saveData.totalSpecialWeapons[1].damage = 1f;
        saveManager.saveData.totalSpecialWeapons[1].bulletsInMag =2;
        saveManager.saveData.totalSpecialWeapons[1].reserveAmmo = 16;

        saveManager.saveData.totalSpecialWeapons[2] = new WeaponData();
        saveManager.saveData.totalSpecialWeapons[2].name = "Cross Bow";
        saveManager.saveData.totalSpecialWeapons[2].firePointPos = new Vector3(0.325f, 0.625f, 1.175f);
        saveManager.saveData.totalSpecialWeapons[2].fireRate = 0.5f;
        saveManager.saveData.totalSpecialWeapons[2].movementSpeed = 3f;
        saveManager.saveData.totalSpecialWeapons[2].bulletForce = 40;
        saveManager.saveData.totalSpecialWeapons[2].magSize = 2;
        saveManager.saveData.totalSpecialWeapons[2].totalAmmo = 0;
        saveManager.saveData.totalSpecialWeapons[2].impact = 1;
        saveManager.saveData.totalSpecialWeapons[2].damage = 1f;
        saveManager.saveData.totalSpecialWeapons[2].bulletsInMag = 2;
        saveManager.saveData.totalSpecialWeapons[2].reserveAmmo = -1;

        saveManager.saveData.totalSpecialWeapons[3] = new WeaponData();
        saveManager.saveData.totalSpecialWeapons[3].name = "RPG";
        saveManager.saveData.totalSpecialWeapons[3].firePointPos = new Vector3(0.325f, 0.65f, 1.3f);
        saveManager.saveData.totalSpecialWeapons[3].fireRate = 1f;
        saveManager.saveData.totalSpecialWeapons[3].movementSpeed = 1.5f;
        saveManager.saveData.totalSpecialWeapons[3].bulletForce = 10;
        saveManager.saveData.totalSpecialWeapons[3].magSize = 2;
        saveManager.saveData.totalSpecialWeapons[3].totalAmmo = 16;
        saveManager.saveData.totalSpecialWeapons[3].impact = 1;
        saveManager.saveData.totalSpecialWeapons[3].damage = 1f;
        saveManager.saveData.totalSpecialWeapons[3].bulletsInMag = 2;
        saveManager.saveData.totalSpecialWeapons[3].reserveAmmo = 16;

        saveManager.saveData.totalSpecialWeapons[4] = new WeaponData();
        saveManager.saveData.totalSpecialWeapons[4].name = "Mini Gun";
        saveManager.saveData.totalSpecialWeapons[4].firePointPos = new Vector3(0.125f, 0.5f, 1.35f);
        saveManager.saveData.totalSpecialWeapons[4].fireRate = 0.025f;
        saveManager.saveData.totalSpecialWeapons[4].movementSpeed = 1f;
        saveManager.saveData.totalSpecialWeapons[4].bulletForce = 20;
        saveManager.saveData.totalSpecialWeapons[4].magSize = 200;
        saveManager.saveData.totalSpecialWeapons[4].totalAmmo = 1600;
        saveManager.saveData.totalSpecialWeapons[4].impact = 1;
        saveManager.saveData.totalSpecialWeapons[4].damage = 0.25f;
        saveManager.saveData.totalSpecialWeapons[4].bulletsInMag = 200;
        saveManager.saveData.totalSpecialWeapons[4].reserveAmmo = 1600;
    }

}
