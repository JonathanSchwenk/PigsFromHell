using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class MysteryGunButton : MonoBehaviour
{
    [SerializeField] private GameObject takeGunButton;
    private GameObject diceIcon;
    private GameObject gunsList; // Will be gotten from the activebuying game onject inbthe game manager


    private int randomGun;
    private int cost = 500; // this can change


    private ISaveManager saveManager;
    private IGameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }


    private int PrimaryOrSecondaryHelper() {
        if (gameManager.activeWeapon.name == gameManager.currentWeapons[0].name) {
            return 0;
        } else {
            return 1;
        }
    }

    /*

        Loop and pick a random gun from the gun list in the game object, then in a short time pick again till the picking slows down.
        Now keep picking till time runs out and which ever gun it stops on gets set to currentWeapon and active weapon

        while loop wont work, too fast

        try loop within coroutine

    */



    public void BuyMysteryGun() {

        // Gets guns list object from the game manager
        // I do it this way because now the player moves from different buy stations it knows which one is within range
        gunsList = gameManager.activeBuyObject.transform.GetChild(1).gameObject;

        // Sets the is currently buying to true to signal that the buy button should turn off so you cant spam
        gameManager.currentlyBuyingNewGun = true;

        if (gameManager.points > cost) { // Change back to > after testing
            diceIcon = gameManager.activeBuyObject.transform.GetChild(0).transform.GetChild(2).gameObject;
            diceIcon.SetActive(false);
            gunsList.SetActive(true);
            StartCoroutine(WaitForNextGun(0.2f, 0));
            gameManager.points -= cost;
        }

    }



    // Gun list can only be the guns that are unlocked
    // Thought is to loop through unlocked weapons and pick random ones, then to actually select the gun it grabs it from the gunlist
    // if same gun that you already have it just refills the gun
    IEnumerator WaitForNextGun(float waitTime, int counter) {
        // Change gun
        if (counter < 25) {
            randomGun = Random.Range(0, saveManager.saveData.unlockedWeapons.Count);
            for (int i = 0; i < gunsList.transform.childCount; i++) {
                if (saveManager.saveData.unlockedWeapons[randomGun].name == "Knife") {
                    randomGun = 0;
                }
                if (saveManager.saveData.unlockedWeapons[randomGun].name == gunsList.transform.GetChild(i).name) {
                    gunsList.transform.GetChild(i).gameObject.SetActive(true);
                } else {
                    gunsList.transform.GetChild(i).gameObject.SetActive(false);
                }
            }

        } else {
            // Stop on that gun and gets it

            // Change this to make it so at this point the buy button changes to a take gun button and when you click that button it does the below functions

            takeGunButton.SetActive(true);
            StartCoroutine(WaitToTakeGun(7.5f, 0)); 
        }
        // wait
        yield return new WaitForSeconds(waitTime);

        if (counter < 25) {
            StartCoroutine(WaitForNextGun(waitTime, counter+=1)); 
        }
    }

    IEnumerator WaitToTakeGun(float waitTime, int counter) {
        // Wait some time
        yield return new WaitForSeconds(waitTime);

        // Changes dice icon to be on to show that the process is over
        diceIcon = gameManager.activeBuyObject.transform.GetChild(0).transform.GetChild(2).gameObject;
        diceIcon.SetActive(true);
        gunsList.SetActive(false);

        // Set the take button to false, signaling that the timer ran out and you can no longer get the gun
        takeGunButton.SetActive(false);

        // Sets the is currently buying to false to signal that the buy button should turn back on
        gameManager.currentlyBuyingNewGun = false;
    }





    public void GetNewGun() {
        string gunName = saveManager.saveData.unlockedWeapons[randomGun].name;
        // if you already have the gun it refills your gun, else if its also not a knife then it gives you the new gun
        // Primary
        if (gameManager.currentWeapons[0].name == gunName) {
            // normal guns
            // Loops through all guns, checks to make sure their the right one and refills it
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
        } else if (gameManager.currentWeapons[1].name == gunName) {
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
                ChangeGunHelper(gunName);
            }
        }
        gunsList = gameManager.activeBuyObject.transform.GetChild(1).gameObject;

        // Changes dice icon to be on to show that the process is over
        diceIcon = gameManager.activeBuyObject.transform.GetChild(0).transform.GetChild(2).gameObject;
        diceIcon.SetActive(true);
        gunsList.SetActive(false);

        // Changes the button from take back to buy
        takeGunButton.SetActive(false);
        // Sets the is currently buying to false to signal that the buy button should turn back on
        gameManager.currentlyBuyingNewGun = false;
    }


    private void ChangeGunHelper(string gunName) {
        // Normal Weapons
        // Loops through unlocked weapons
        for (int j = 0; j < saveManager.saveData.unlockedWeapons.Count; j++) {
            // checks to see if the gun you pressed is unlocked
            if (saveManager.saveData.unlockedWeapons[j].name == gunName) {
                // Loops through normal weapons to set the correct weapon to the current one
                for (int i = 0; i < saveManager.saveData.totalNormalWeapons.Length; i++) {
                    // Sets the correct one
                    if (saveManager.saveData.totalNormalWeapons[i].name == gunName) {
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
            if (saveManager.saveData.unlockedWeapons[j].name == gunName) {
                // Loops through normal weapons to set the correct weapon to the current one
                for (int i = 0; i < saveManager.saveData.totalSpecialWeapons.Length; i++) {
                    // Sets the correct one
                    if (saveManager.saveData.totalSpecialWeapons[i].name == gunName) {
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

        gameManager.activeWeapon = gameManager.currentWeapons[primaryOrSecondary]; // Need to make a refrence to the pointer for the weapons so their connected
    }


}
