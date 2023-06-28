using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class SpeedDrop : MonoBehaviour
{

    private IGameManager gameManager;
    //private ISaveManager saveManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
        //saveManager = ServiceLocator.Resolve<ISaveManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {

            gameManager.UpdateDrops("Speed");

            gameObject.SetActive(false);
        }
    }
}