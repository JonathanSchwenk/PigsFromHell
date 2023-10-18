using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class ServiceManagerSampleScene : MonoBehaviour
{
    public SaveManager saveManager;
    public ObjectPooler objectPooler;
    public GameManager gameManager;
    public SpawnManager spawnManager;
    public AudioManager audioManager;
    public AdManager adManager;


    private void Awake() {
        
        // If there is no SaveManager service registered, create one, else, do nothing
        if (ServiceLocator.IsRegistered<ISaveManager>()) {
            Debug.Log("A SaveManager already exists");
            // Loading save here because this script gets executed early which is where I need to load so Im trying here.
            saveManager.Load();
        } else {
            Debug.Log("SaveManager not found, creating one");
            ServiceLocator.Register<ISaveManager>(saveManager);
            // Loading save here because this script gets executed early which is where I need to load so Im trying here.
            saveManager.Load();
        }

        // I want to make the audio manager in the menu but this is for testing and debugging so theres not two of them

        // If there is no SaveManager service registered, create one, else, do nothing
        if (ServiceLocator.IsRegistered<IAudioManager>()) {
            Debug.Log("An AudioManager already exists");
        } else {
            Debug.Log("AudioManager not found, creating one");
            ServiceLocator.Register<IAudioManager>(audioManager);
        }

        // If there is no SaveManager service registered, create one, else, do nothing
        if (ServiceLocator.IsRegistered<IAdManager>()) {
            Debug.Log("An IAdManager already exists");
        } else {
            Debug.Log("IAdManager not found, creating one");
            ServiceLocator.Register<IAdManager>(adManager);
        }
        


        ServiceLocator.Register<IObjectPooler>(objectPooler);
        ServiceLocator.Register<IGameManager>(gameManager);
        ServiceLocator.Register<ISpawnManager>(spawnManager);
    }


    private void OnDestroy() {
        ServiceLocator.Unregister<IObjectPooler>();
        ServiceLocator.Unregister<IGameManager>();
        ServiceLocator.Unregister<ISpawnManager>();
        ServiceLocator.Unregister<IAdManager>();
    }

    
    private void OnApplicationQuit() {
        if (ServiceLocator.IsRegistered<ISaveManager>()) {
            ServiceLocator.Unregister<ISaveManager>();
        }
        if (ServiceLocator.IsRegistered<IAudioManager>()) {
            ServiceLocator.Unregister<IAudioManager>();
        }
    }
    
}
