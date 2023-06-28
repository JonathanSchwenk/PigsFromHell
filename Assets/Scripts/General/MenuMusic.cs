using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class MenuMusic : MonoBehaviour
{
    private IAudioManager audioManager; 
    private ISaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = ServiceLocator.Resolve<IAudioManager>();
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        if (saveManager.saveData.musicOn == true) {
            audioManager.PlayMusic("MenuBackgroundMusic");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (saveManager.saveData.musicOn == false) {
            audioManager.StopMusic("MenuBackgroundMusic");
        }
    }
}
