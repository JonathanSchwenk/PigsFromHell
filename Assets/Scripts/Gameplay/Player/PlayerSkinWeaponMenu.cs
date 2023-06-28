using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class PlayerSkinWeaponMenu : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject animatorPool;
    [SerializeField] private GameObject skinPool;


    private ISaveManager saveManager;

    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }

    private void SaveManagerOnSave(int num) {
        // Skin
        for (int i = 0; i < skinPool.transform.childCount; i++) {
            if (skinPool.transform.GetChild(i).name == saveManager.saveData.currentSkin) {
                skinPool.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                skinPool.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        //print(saveManager.saveData.activeWeapon.name);

        // Weapon
        for (int i = 0; i < rightHand.transform.childCount; i++) {
            if (rightHand.transform.GetChild(i).name == saveManager.saveData.activeWeapon.name) {
                rightHand.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                rightHand.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // Animator
        animator.runtimeAnimatorController = animatorPool.transform.Find(saveManager.saveData.activeWeapon.name).GetComponent<Animator>().runtimeAnimatorController;

    }



    // Start is called before the first frame update
    void Start()
    {
        // Skin
        for (int i = 0; i < skinPool.transform.childCount; i++) {
            if (skinPool.transform.GetChild(i).name == saveManager.saveData.currentSkin) {
                skinPool.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                skinPool.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // Weapon
        for (int i = 0; i < rightHand.transform.childCount; i++) {
            if (rightHand.transform.GetChild(i).name == saveManager.saveData.activeWeapon.name) { 
                rightHand.transform.GetChild(i).gameObject.SetActive(true);
            } else {
                rightHand.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // Animator
        animator.runtimeAnimatorController = animatorPool.transform.Find(saveManager.saveData.activeWeapon.name).GetComponent<Animator>().runtimeAnimatorController;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
