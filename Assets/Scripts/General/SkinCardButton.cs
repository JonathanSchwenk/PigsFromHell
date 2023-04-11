using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;

public class SkinCardButton : MonoBehaviour
{
    [SerializeField] private GameObject skinNameObject;

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


    public void ChangeSkin() {
        //print(gun.GetComponent<TextMeshProUGUI>().text);

        // if the skin is unlocked then set the skin to the new one, if not then open new UI to buy it

        if (saveManager.saveData.unlockedSkins.Contains(skinNameObject.GetComponent<TextMeshProUGUI>().text)) {
            saveManager.saveData.currentSkin = skinNameObject.GetComponent<TextMeshProUGUI>().text;
            saveManager.Save();
        }
    }
}
