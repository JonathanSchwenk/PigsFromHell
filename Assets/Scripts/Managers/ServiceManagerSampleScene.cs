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


    private void Awake() {
        
        // If there is no SaveManager service registered, create one, else, do nothing
        if (ServiceLocator.IsRegistered<ISaveManager>()) {
            Debug.Log("A SaveManager already exists");
        } else {
            Debug.Log("SaveManager not found, creating one");
            ServiceLocator.Register<ISaveManager>(saveManager);
        }
        


        ServiceLocator.Register<IObjectPooler>(objectPooler);
        ServiceLocator.Register<IGameManager>(gameManager);
        ServiceLocator.Register<ISpawnManager>(spawnManager);
    }


    private void OnDestroy() {
        ServiceLocator.Unregister<IObjectPooler>();
        ServiceLocator.Unregister<IGameManager>();
        ServiceLocator.Unregister<ISpawnManager>();
    }

    
    private void OnApplicationQuit() {
        if (ServiceLocator.IsRegistered<ISaveManager>()) {
            ServiceLocator.Unregister<ISaveManager>();
        }
    }
    
}
