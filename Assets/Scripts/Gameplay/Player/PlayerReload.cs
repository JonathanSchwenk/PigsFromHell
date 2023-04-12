using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class PlayerReload : MonoBehaviour
{
    [SerializeField] Animator animator;


    private int totalAmmo; // how many bullet the player has currently
    private int ammoCap; // max bullets the player can hold 
    private int magSize;
    private int shotsInMag;


    private WeaponData activeWeapon;


    private ISaveManager saveManager;


    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        } else {
            print("No save manager");
        }

        activeWeapon = saveManager.saveData.activeWeapon;
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }

    private void SaveManagerOnSave(int num) {
        // This happens everytime you save, I think it is fine, want just for when you change weapons. 
        // Could add something else like a bool to check if I wanna actually do something here.

        // This is fine for getting a new weapon but not for switching.

        ammoCap = activeWeapon.totalAmmo;
        totalAmmo = activeWeapon.reserveAmmo;
        magSize = activeWeapon.magSize;
        shotsInMag = activeWeapon.bulletsInMag;
    }


    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = saveManager.saveData.activeWeapon;

        ammoCap = activeWeapon.totalAmmo;
        totalAmmo = activeWeapon.reserveAmmo;
        magSize = activeWeapon.magSize;
        shotsInMag = activeWeapon.bulletsInMag;
    }

    // Update is called once per frame
    void Update()
    {
        shotsInMag = activeWeapon.bulletsInMag;
    }
    


    public void Reload() {
        // if the current weapon isn't a knife then reload the gun, else do nothing
        // And check to make sure that the mag isnt full before reloading
        if (activeWeapon.name != "Knife" && shotsInMag < magSize && saveManager.saveData.activeWeapon.reserveAmmo > 1) {
            print("Reloading");
            
            if (saveManager.saveData.activeWeapon.reserveAmmo > magSize) {
                saveManager.saveData.activeWeapon.bulletsInMag = magSize;
                saveManager.saveData.activeWeapon.reserveAmmo -= (magSize - shotsInMag);
            } else {
                saveManager.saveData.activeWeapon.bulletsInMag = saveManager.saveData.activeWeapon.reserveAmmo;
                saveManager.saveData.activeWeapon.reserveAmmo = 0;
            }
            saveManager.Save();
        } 
    }
}
