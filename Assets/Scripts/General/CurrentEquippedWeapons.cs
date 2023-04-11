using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Dorkbots.ServiceLocatorTools;

public class CurrentEquiptedWeapons : MonoBehaviour
{

    [SerializeField] private Image gunImage;
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private GameObject imagesForUI;
    [SerializeField] private int primaryOrSecondary;

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
        gunName.text = saveManager.saveData.currentWeapons[primaryOrSecondary].name;
        gunImage.sprite = imagesForUI.transform.Find(saveManager.saveData.currentWeapons[primaryOrSecondary].name).GetComponent<Image>().sprite;
    }


    // Start is called before the first frame update
    void Start()
    {
        gunName.text = saveManager.saveData.currentWeapons[primaryOrSecondary].name;
        gunImage.sprite = imagesForUI.transform.Find(saveManager.saveData.currentWeapons[primaryOrSecondary].name).GetComponent<Image>().sprite;

    }

    // Update is called once per frame
    void Update()
    {
        //gunName.text = saveManager.saveData.currentWeapons[primaryOrSecondary];
        //gunImage.sprite = imagesForUI.transform.Find(saveManager.saveData.currentWeapons[primaryOrSecondary]).GetComponent<Image>().sprite;
    }
}
