using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class UpgradeGunsButton : MonoBehaviour
{
    private int cost = 1000;

    private IGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeGun() {
        // Finds the active gun and upgrades its starValue
        if (gameManager.points > cost && gameManager.activeWeapon.starValue < 4) { // Change back to > after testing
            gameManager.activeWeapon.starValue += 1;

            gameManager.points -= cost;
        }
    }
}
