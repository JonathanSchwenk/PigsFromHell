using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class BuyGunInMenu : MonoBehaviour
{
    [SerializeField] private GameObject buyGunsCanvas;
    [SerializeField] private GameObject characterCanvas;

    public string gunToBuyName;
    public int primaryOrSecondary;


    private ISaveManager saveManager;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cancel() {
        buyGunsCanvas.SetActive(false);
        characterCanvas.SetActive(true);
    }


    public void BuyGun(int cost) {
        if (saveManager.saveData.coins <= cost) { // change back to >=

            // Loops through normal weapons to set the correct weapon to the current one
            for (int i = 0; i < saveManager.saveData.totalNormalWeapons.Length; i++) {
                // Sets the correct one
                if (saveManager.saveData.totalNormalWeapons[i].name == gunToBuyName) {
                    if (primaryOrSecondary == 0) {
                        // Adds the gun to the unlocked ones
                        saveManager.saveData.unlockedWeapons.Add(saveManager.saveData.totalNormalWeapons[i]);

                        // Sets the current and active weapons
                        saveManager.saveData.currentWeapons[0] = saveManager.saveData.totalNormalWeapons[i];
                        saveManager.saveData.activeWeapon = saveManager.saveData.totalNormalWeapons[i];
                    } else {
                        // Adds the gun to the unlocked ones
                        saveManager.saveData.unlockedWeapons.Add(saveManager.saveData.totalNormalWeapons[i]);

                        // Sets the current and active weapons
                        saveManager.saveData.currentWeapons[1] = saveManager.saveData.totalNormalWeapons[i];
                    }
                }
            }
            // Loops through special weapons to set the correct weapon to the current one
            for (int i = 0; i < saveManager.saveData.totalSpecialWeapons.Length; i++) {
                // Sets the correct one
                if (saveManager.saveData.totalSpecialWeapons[i].name == gunToBuyName) {
                    if (primaryOrSecondary == 0) {
                        
                        saveManager.saveData.unlockedWeapons.Add(saveManager.saveData.totalSpecialWeapons[i]);

                        // Sets the current and active weapons
                        saveManager.saveData.currentWeapons[0] = saveManager.saveData.totalSpecialWeapons[i];
                        saveManager.saveData.activeWeapon = saveManager.saveData.totalSpecialWeapons[i];
                    } else {
                        saveManager.saveData.unlockedWeapons.Add(saveManager.saveData.totalSpecialWeapons[i]);

                        // Sets the current and active weapons
                        saveManager.saveData.currentWeapons[1] = saveManager.saveData.totalSpecialWeapons[i];
                    }
                }
            }
        }

        saveManager.saveData.coins -= cost;

        // Change canvases back
        buyGunsCanvas.SetActive(false);
        characterCanvas.SetActive(true);

        saveManager.Save();
    }



}
