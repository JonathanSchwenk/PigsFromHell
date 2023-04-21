using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;

public class WeaponCardButton : MonoBehaviour
{

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


    public void PrimaryGunButtonChangeGun(GameObject gunTextGO) {

        if (gunTextGO.GetComponent<TextMeshProUGUI>().text != saveManager.saveData.currentWeapons[0].name &&
        gunTextGO.GetComponent<TextMeshProUGUI>().text != saveManager.saveData.currentWeapons[1].name) {

            ChangeGunHelper(gunTextGO);
        }


        saveManager.Save();
        
    }


    private void ChangeGunHelper(GameObject gunTextGO) {
        // Normal Weapons
        // Loops through unlocked weapons
        for (int j = 0; j < saveManager.saveData.unlockedWeapons.Count; j++) {
            // checks to see if the gun you pressed is unlocked
            if (saveManager.saveData.unlockedWeapons[j].name == gunTextGO.GetComponent<TextMeshProUGUI>().text) {
                // Loops through normal weapons to set the correct weapon to the current one
                for (int i = 0; i < saveManager.saveData.totalNormalWeapons.Length; i++) {
                    // Sets the correct one
                    if (saveManager.saveData.totalNormalWeapons[i].name == gunTextGO.GetComponent<TextMeshProUGUI>().text) {
                        if (primaryOrSecondary == 0) {
                            saveManager.saveData.currentWeapons[0] = saveManager.saveData.totalNormalWeapons[i];
                            saveManager.saveData.activeWeapon = saveManager.saveData.totalNormalWeapons[i];
                        } else {
                            saveManager.saveData.currentWeapons[1] = saveManager.saveData.totalNormalWeapons[i];
                        }
                    }
                }
            }
        }
        // Special Weapons
        for (int j = 0; j < saveManager.saveData.unlockedWeapons.Count; j++) {
            // checks to see if the gun you pressed is unlocked
            if (saveManager.saveData.unlockedWeapons[j].name == gunTextGO.GetComponent<TextMeshProUGUI>().text) {
                // Loops through normal weapons to set the correct weapon to the current one
                for (int i = 0; i < saveManager.saveData.totalSpecialWeapons.Length; i++) {
                    // Sets the correct one
                    if (saveManager.saveData.totalSpecialWeapons[i].name == gunTextGO.GetComponent<TextMeshProUGUI>().text) {
                        if (primaryOrSecondary == 0) {
                            saveManager.saveData.currentWeapons[0] = saveManager.saveData.totalSpecialWeapons[i];
                            saveManager.saveData.activeWeapon = saveManager.saveData.totalSpecialWeapons[i];
                        } else {
                            saveManager.saveData.currentWeapons[1] = saveManager.saveData.totalSpecialWeapons[i];
                        }
                    }
                }
            }
        }
    }
    
}
