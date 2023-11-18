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
    private IGameManager gameManager;
    private IAudioManager audioManager;


    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
        audioManager = ServiceLocator.Resolve<IAudioManager>();

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
        activeWeapon = gameManager.activeWeapon;

        // This is fine for getting a new weapon but not for switching.

        ammoCap = activeWeapon.totalAmmo;
        totalAmmo = activeWeapon.reserveAmmo;
        magSize = activeWeapon.magSize;
        shotsInMag = activeWeapon.bulletsInMag;

        //print(saveManager.saveData.activeWeapon.name);
    }


    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = gameManager.activeWeapon;

        ammoCap = activeWeapon.totalAmmo;
        totalAmmo = activeWeapon.reserveAmmo;
        magSize = activeWeapon.magSize;
        shotsInMag = activeWeapon.bulletsInMag;
    }

    // Update is called once per frame
    void Update()
    {
        shotsInMag = gameManager.activeWeapon.bulletsInMag;
    }
    


    public void Reload() {
        // if the current weapon isn't a knife then reload the gun, else do nothing
        // And check to make sure that the mag isnt full before reloading
        if (activeWeapon.name != "Knife" && gameManager.activeWeapon.bulletsInMag < magSize && gameManager.activeWeapon.reserveAmmo > 1) {

            audioManager.PlaySFX("Reload");
            
            if (gameManager.activeWeapon.reserveAmmo >= magSize) {
                gameManager.activeWeapon.bulletsInMag = magSize;
                gameManager.activeWeapon.reserveAmmo -= (magSize - shotsInMag);
            } else {
                // If current shots in mag + reserve amma > magsize
                if ((gameManager.activeWeapon.bulletsInMag + gameManager.activeWeapon.reserveAmmo) >= magSize) {
                    // subtract from reserves
                    gameManager.activeWeapon.reserveAmmo -= (magSize - gameManager.activeWeapon.bulletsInMag);
                    gameManager.activeWeapon.bulletsInMag = magSize;
                } else {
                    gameManager.activeWeapon.bulletsInMag = (gameManager.activeWeapon.bulletsInMag) + gameManager.activeWeapon.reserveAmmo;
                    gameManager.activeWeapon.reserveAmmo = 0;
                }
            }
        } 
    }
}
