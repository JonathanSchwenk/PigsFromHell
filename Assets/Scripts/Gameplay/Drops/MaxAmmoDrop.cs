using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class MaxAmmoDrop : MonoBehaviour
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
            // Give player max ammo
            gameManager.currentWeapons[0].reserveAmmo = gameManager.currentWeapons[0].totalAmmo;
            gameManager.currentWeapons[1].reserveAmmo = gameManager.currentWeapons[1].totalAmmo;

            gameObject.SetActive(false);
        }
    }
}
