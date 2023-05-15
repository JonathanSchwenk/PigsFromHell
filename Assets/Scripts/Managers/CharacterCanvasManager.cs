using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;

public class CharacterCanvasManager : MonoBehaviour
{

    [SerializeField] private GameObject priNormWeapons;
    [SerializeField] private GameObject priSpecWeapons;
    [SerializeField] private GameObject secNormWeapons;
    [SerializeField] private GameObject secSpecWeapons;

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

    }

    

    public void ChangeWeaponTypeNormal(int primaryOrSecondary) {
        if (primaryOrSecondary == 0) {
            priNormWeapons.gameObject.SetActive(true);
            priSpecWeapons.gameObject.SetActive(false);
        } else {
            secNormWeapons.gameObject.SetActive(true);
            secSpecWeapons.gameObject.SetActive(false);
        }
    }

    public void ChangeWeaponTypeSpecial(int primaryOrSecondary) {
        if (primaryOrSecondary == 0) {
            priNormWeapons.gameObject.SetActive(false);
            priSpecWeapons.gameObject.SetActive(true);
        } else {
            secNormWeapons.gameObject.SetActive(false);
            secSpecWeapons.gameObject.SetActive(true);
        }
    }

}
