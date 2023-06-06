using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class ServiceManagerMenu : MonoBehaviour
{
    
    public SaveManager saveManager;
    public AudioManager audioManager;
    public AdManager adManager;
    


    private void Awake() {
        // If there is no SaveManager service registered, create one, else, do nothing
        if (ServiceLocator.IsRegistered<ISaveManager>()) {
            //Debug.Log("A SaveManager already exists");
            // Loading save here because this script gets executed early which is where I need to load so Im trying here.
            saveManager.Load();
        } else {
            //Debug.Log("SaveManager not found, creating one");
            ServiceLocator.Register<ISaveManager>(saveManager);

            // Loading save here because this script gets executed early which is where I need to load so Im trying here.
            saveManager.Load();
        }

        // If there is no AudioManager service registered, create one, else, do nothing
        if (ServiceLocator.IsRegistered<IAudioManager>()) {
            //Debug.Log("An AudioManager already exists");
        } else {
            //Debug.Log("AudioManager not found, creating one");
            ServiceLocator.Register<IAudioManager>(audioManager);
        }

        // AdManager should be registered in the menu but this is a check
        // If there is no AdManager service registered, create one, else, do nothing
        if (ServiceLocator.IsRegistered<IAdManager>()) {
            //Debug.Log("An AdManager already exists");
        } else {
            //Debug.Log("AdManager not found, creating one");
            ServiceLocator.Register<IAdManager>(adManager);
        }

    }


    private void OnDestroy() {

    }

    private void OnApplicationQuit() {
        if (ServiceLocator.IsRegistered<ISaveManager>()) {
            ServiceLocator.Unregister<ISaveManager>();
        }
        if (ServiceLocator.IsRegistered<IAudioManager>()) {
            ServiceLocator.Unregister<IAudioManager>();
        }
        if (ServiceLocator.IsRegistered<IAdManager>()) {
            ServiceLocator.Unregister<IAdManager>();
        }
    }
    

}

