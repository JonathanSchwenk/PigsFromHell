using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;

public class BuyGunsButton : MonoBehaviour
{
    private int cost; 
    private TextMeshProUGUI gunName;

    // If player is within a certain distance from the gun buying station then trigger a UI button to pop up that allows you to buy the gun if you have enough

    private ISaveManager saveManager;
    private IGameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }


    private int PrimaryOrSecondaryHelper() {
        if (saveManager.saveData.activeWeapon.name == saveManager.saveData.currentWeapons[0].name) {
            return 0;
        } else {
            return 1;
        }
    }


    public void BuyGun() {
        // if the player already has the gun in prymary or secondary then it refills the bullets
        // else if its a knife then it does nothing and if its not a knife then it buys the gun and replaces the currentWeapon and active weapon

        // get cost and name from game manager
        cost = int.Parse(gameManager.activeBuyObject.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text);
        gunName = gameManager.activeBuyObject.transform.GetChild(0).transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        if (gameManager.points < cost) { // Change back to >
            // Primary
            if (gameManager.currentWeapons[0].name == gunName.text) {
                print("Refill gun");
                // normal guns
                for (int i = 0; i < saveManager.saveData.totalNormalWeapons.Length; i++) {
                    if (gameManager.currentWeapons[0].name == saveManager.saveData.totalNormalWeapons[i].name) {
                        gameManager.currentWeapons[0].reserveAmmo = saveManager.saveData.totalNormalWeapons[i].reserveAmmo;
                    }
                }
                // special guns
                for (int i = 0; i < saveManager.saveData.totalSpecialWeapons.Length; i++) {
                    if (gameManager.currentWeapons[0].name == saveManager.saveData.totalSpecialWeapons[i].name) {
                        gameManager.currentWeapons[0].reserveAmmo = saveManager.saveData.totalSpecialWeapons[i].reserveAmmo;
                    }
                }
            // Secondary
            } else if (gameManager.currentWeapons[1].name == gunName.text) {
                // normal guns
                for (int i = 0; i < saveManager.saveData.totalNormalWeapons.Length; i++) {
                    if (gameManager.currentWeapons[1].name == saveManager.saveData.totalNormalWeapons[i].name) {
                        gameManager.currentWeapons[1].reserveAmmo = saveManager.saveData.totalNormalWeapons[i].reserveAmmo;
                    }
                }
                // special guns
                for (int i = 0; i < saveManager.saveData.totalSpecialWeapons.Length; i++) {
                    if (gameManager.currentWeapons[1].name == saveManager.saveData.totalSpecialWeapons[i].name) {
                        gameManager.currentWeapons[1].reserveAmmo = saveManager.saveData.totalSpecialWeapons[i].reserveAmmo;
                    }
                }
            // Lastly if its not a knife then can buy and replace
            } else {
                if (gameManager.activeWeapon.name != "Knife") {
                    BuyGunHelper();
                }
            }

            gameManager.points -= cost;
        }
        
    }


    private void BuyGunHelper() {
        // Normal Weapons
        // Loops through unlocked weapons
        for (int j = 0; j < saveManager.saveData.unlockedWeapons.Count; j++) {
            // checks to see if the gun you pressed is unlocked
            if (saveManager.saveData.unlockedWeapons[j].name == gunName.text) {
                // Loops through normal weapons to set the correct weapon to the current one
                for (int i = 0; i < saveManager.saveData.totalNormalWeapons.Length; i++) {
                    // Sets the correct one
                    if (saveManager.saveData.totalNormalWeapons[i].name == gunName.text) {
                        if (PrimaryOrSecondaryHelper() == 0) {
                            SetGunsFromSaveDataNorm(0,i);
                        } else {
                            SetGunsFromSaveDataNorm(1,i);
                        }
                    }
                }
            }
        }
        // Special Weapons
        for (int j = 0; j < saveManager.saveData.unlockedWeapons.Count; j++) {
            // checks to see if the gun you pressed is unlocked
            if (saveManager.saveData.unlockedWeapons[j].name == gunName.text) {
                // Loops through normal weapons to set the correct weapon to the current one
                for (int i = 0; i < saveManager.saveData.totalSpecialWeapons.Length; i++) {
                    // Sets the correct one
                    if (saveManager.saveData.totalSpecialWeapons[i].name == gunName.text) {
                        if (PrimaryOrSecondaryHelper() == 0) {
                            SetGunsFromSaveDataSpec(0,i);
                        } else {
                            SetGunsFromSaveDataSpec(1,i);
                        }
                    }
                }
            }
        }

        saveManager.Save();
    }


    private void SetGunsFromSaveDataNorm(int primaryOrSecondary, int i) {

        // Current weapon
        gameManager.currentWeapons[primaryOrSecondary].name = saveManager.saveData.totalNormalWeapons[i].name;
        gameManager.currentWeapons[primaryOrSecondary].firePointPos = saveManager.saveData.totalNormalWeapons[i].firePointPos;
        gameManager.currentWeapons[primaryOrSecondary].fireRate = saveManager.saveData.totalNormalWeapons[i].fireRate;
        gameManager.currentWeapons[primaryOrSecondary].movementSpeed = saveManager.saveData.totalNormalWeapons[i].movementSpeed;
        gameManager.currentWeapons[primaryOrSecondary].bulletForce = saveManager.saveData.totalNormalWeapons[i].bulletForce;
        gameManager.currentWeapons[primaryOrSecondary].magSize = saveManager.saveData.totalNormalWeapons[i].magSize;
        gameManager.currentWeapons[primaryOrSecondary].totalAmmo = saveManager.saveData.totalNormalWeapons[i].totalAmmo;
        gameManager.currentWeapons[primaryOrSecondary].impact = saveManager.saveData.totalNormalWeapons[i].impact;
        gameManager.currentWeapons[primaryOrSecondary].damage = saveManager.saveData.totalNormalWeapons[i].damage;
        gameManager.currentWeapons[primaryOrSecondary].bulletsInMag = saveManager.saveData.totalNormalWeapons[i].bulletsInMag;
        gameManager.currentWeapons[primaryOrSecondary].reserveAmmo = saveManager.saveData.totalNormalWeapons[i].reserveAmmo;
        gameManager.currentWeapons[primaryOrSecondary].starValue = saveManager.saveData.totalNormalWeapons[i].starValue;

        //Active weapon
        /*
        gameManager.activeWeapon.name = saveManager.saveData.totalNormalWeapons[i].name;
        gameManager.activeWeapon.firePointPos = saveManager.saveData.totalNormalWeapons[i].firePointPos;
        gameManager.activeWeapon.fireRate = saveManager.saveData.totalNormalWeapons[i].fireRate;
        gameManager.activeWeapon.movementSpeed = saveManager.saveData.totalNormalWeapons[i].movementSpeed;
        gameManager.activeWeapon.bulletForce = saveManager.saveData.totalNormalWeapons[i].bulletForce;
        gameManager.activeWeapon.magSize = saveManager.saveData.totalNormalWeapons[i].magSize;
        gameManager.activeWeapon.totalAmmo = saveManager.saveData.totalNormalWeapons[i].totalAmmo;
        gameManager.activeWeapon.impact = saveManager.saveData.totalNormalWeapons[i].impact;
        gameManager.activeWeapon.damage = saveManager.saveData.totalNormalWeapons[i].damage;
        gameManager.activeWeapon.bulletsInMag = saveManager.saveData.totalNormalWeapons[i].bulletsInMag;
        gameManager.activeWeapon.reserveAmmo = saveManager.saveData.totalNormalWeapons[i].reserveAmmo;
        */

        gameManager.activeWeapon = gameManager.currentWeapons[primaryOrSecondary]; // Need to make a refrence to the pointer for the weapons so their connected
    }

    private void SetGunsFromSaveDataSpec(int primaryOrSecondary, int i) {

        // Current weapon
        gameManager.currentWeapons[primaryOrSecondary].name = saveManager.saveData.totalSpecialWeapons[i].name;
        gameManager.currentWeapons[primaryOrSecondary].firePointPos = saveManager.saveData.totalSpecialWeapons[i].firePointPos;
        gameManager.currentWeapons[primaryOrSecondary].fireRate = saveManager.saveData.totalSpecialWeapons[i].fireRate;
        gameManager.currentWeapons[primaryOrSecondary].movementSpeed = saveManager.saveData.totalSpecialWeapons[i].movementSpeed;
        gameManager.currentWeapons[primaryOrSecondary].bulletForce = saveManager.saveData.totalSpecialWeapons[i].bulletForce;
        gameManager.currentWeapons[primaryOrSecondary].magSize = saveManager.saveData.totalSpecialWeapons[i].magSize;
        gameManager.currentWeapons[primaryOrSecondary].totalAmmo = saveManager.saveData.totalSpecialWeapons[i].totalAmmo;
        gameManager.currentWeapons[primaryOrSecondary].impact = saveManager.saveData.totalSpecialWeapons[i].impact;
        gameManager.currentWeapons[primaryOrSecondary].damage = saveManager.saveData.totalSpecialWeapons[i].damage;
        gameManager.currentWeapons[primaryOrSecondary].bulletsInMag = saveManager.saveData.totalSpecialWeapons[i].bulletsInMag;
        gameManager.currentWeapons[primaryOrSecondary].reserveAmmo = saveManager.saveData.totalSpecialWeapons[i].reserveAmmo;
        gameManager.currentWeapons[primaryOrSecondary].starValue = saveManager.saveData.totalSpecialWeapons[i].starValue;

        //Active weapon
        /*
        gameManager.activeWeapon.name = saveManager.saveData.totalSpecialWeapons[i].name;
        gameManager.activeWeapon.firePointPos = saveManager.saveData.totalSpecialWeapons[i].firePointPos;
        gameManager.activeWeapon.fireRate = saveManager.saveData.totalSpecialWeapons[i].fireRate;
        gameManager.activeWeapon.movementSpeed = saveManager.saveData.totalSpecialWeapons[i].movementSpeed;
        gameManager.activeWeapon.bulletForce = saveManager.saveData.totalSpecialWeapons[i].bulletForce;
        gameManager.activeWeapon.magSize = saveManager.saveData.totalSpecialWeapons[i].magSize;
        gameManager.activeWeapon.totalAmmo = saveManager.saveData.totalSpecialWeapons[i].totalAmmo;
        gameManager.activeWeapon.impact = saveManager.saveData.totalSpecialWeapons[i].impact;
        gameManager.activeWeapon.damage = saveManager.saveData.totalSpecialWeapons[i].damage;
        gameManager.activeWeapon.bulletsInMag = saveManager.saveData.totalSpecialWeapons[i].bulletsInMag;
        gameManager.activeWeapon.reserveAmmo = saveManager.saveData.totalSpecialWeapons[i].reserveAmmo;
        */

        gameManager.activeWeapon = gameManager.currentWeapons[primaryOrSecondary]; // Need to make a refrence to the pointer for the weapons so their connected
    }
}
