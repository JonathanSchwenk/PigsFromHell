using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;
using System;

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
        // Going to try to use a dictionary to look up weapons which will make it easier
        // Using it here and making a new one every time isn't idea for memory I think but whatevs
        // Not great way 
        Dictionary<int, string> unlockedWeaponsDictionary = new Dictionary<int, string>();
        
        for (int i = 0; i < saveManager.saveData.unlockedWeapons.Count; i++) {
            unlockedWeaponsDictionary.Add(i, saveManager.saveData.unlockedWeapons[i].name);
        }

        if (unlockedWeaponsDictionary.ContainsValue(gunTextGO.GetComponent<TextMeshProUGUI>().text)) {
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
        } else {
            BuyGunInMenuHelper(gunTextGO, primaryOrSecondary);
        }


        
    }



    private void BuyGunInMenuHelper(GameObject gunTextGO, int primOrSec) {
        // Getting canvases
        GameObject buyGunsCanvas = GameObject.Find("Canvases").gameObject.transform.Find("BuyGunsCanvas").gameObject; // GO.Find only works for active GO, need to get the child
        GameObject characterCanvas = GameObject.Find("CharacterCanvas").gameObject;

        // Setting buy canvas to active
        buyGunsCanvas.SetActive(true);
        characterCanvas.SetActive(false);

        // Still need to set the gun image and name
        buyGunsCanvas.transform.GetChild(2).gameObject.transform.GetChild(1).GetComponent<Image>().sprite = gunTextGO.transform.parent.transform.GetChild(1).GetComponent<Image>().sprite;
        buyGunsCanvas.transform.GetChild(2).gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = gunTextGO.transform.parent.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text;


        // Set which gun is being bought
        GameObject canvasManger = GameObject.Find("CharacterCanavasManager").gameObject;
        canvasManger.GetComponent<BuyGunInMenu>().gunToBuyName = gunTextGO.GetComponent<TextMeshProUGUI>().text;
        canvasManger.GetComponent<BuyGunInMenu>().primaryOrSecondary = primOrSec;

    }
    
}
