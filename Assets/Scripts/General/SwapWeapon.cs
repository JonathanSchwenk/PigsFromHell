using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using UnityEngine.UI;

public class SwapWeapon : MonoBehaviour
{

    [SerializeField] GameObject gunImage;
    [SerializeField] GameObject weaponImages;


    private ISaveManager saveManager;
    private IGameManager gameManager;
    private IAudioManager audioManager;
    private WeaponData activeWeapon;

    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
        audioManager = ServiceLocator.Resolve<IAudioManager>();

        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        } else {
            print("No save manager");
        }

        activeWeapon = new WeaponData();
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }

    private void SaveManagerOnSave(int num) {
        activeWeapon = gameManager.activeWeapon;

        for (int i = 0; i < weaponImages.transform.childCount; i++) {
            if (activeWeapon.name == weaponImages.transform.GetChild(i).name) {
                gunImage.GetComponent<Image>().sprite = weaponImages.transform.GetChild(i).GetComponent<Image>().sprite;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = gameManager.activeWeapon;

        for (int i = 0; i < weaponImages.transform.childCount; i++) {
            if (activeWeapon.name == weaponImages.transform.GetChild(i).name) {
                gunImage.GetComponent<Image>().sprite = weaponImages.transform.GetChild(i).GetComponent<Image>().sprite;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SwitchWeapon() {
        // Plays sound
        audioManager.PlaySFX("ChangeGun");

        if (activeWeapon.name == gameManager.currentWeapons[0].name) {
            gameManager.activeWeapon = gameManager.currentWeapons[1];
        } else if (activeWeapon.name == gameManager.currentWeapons[1].name) {
            gameManager.activeWeapon = gameManager.currentWeapons[2];
        } else if (activeWeapon.name == gameManager.currentWeapons[2].name) {
            gameManager.activeWeapon = gameManager.currentWeapons[0];
        }


        saveManager.Save();
    }
}
