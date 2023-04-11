using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;

public class CharacterCanvasManager : MonoBehaviour
{


    private ISaveManager saveManager;



    /*
    private void Awake() {
        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        }
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        }
    }
    */


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        //saveManager.Load(); // IDK if I want this load here

        //print(saveManager.saveData.currentWeapons[0]);
        //print(saveManager.saveData.currentWeapons[1]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //private void SaveManagerOnSave(int num) {

    //}


/*
    public void PrimaryGunButtonChangeGun(GameObject gun) {
        //print(gun.GetComponent<TextMeshProUGUI>().text);

        // if the weapon is unlocked then set the primary current weapon to the gun, if not, go to a new UI canvas and give the option to unlock the gun

        if (saveManager.saveData.unlockedWeapons.Contains(gun.GetComponent<TextMeshProUGUI>().text)) {
            saveManager.saveData.currentWeapons[0] = gun.GetComponent<TextMeshProUGUI>().text;
            saveManager.Save();
        }
    }
    public void PrimaryGunButtonChangeColor(GameObject weaponCard) {
        weaponCard.transform.GetChild(2).GetComponent<Image>().color = Color.black;
        
    }
*/
}
