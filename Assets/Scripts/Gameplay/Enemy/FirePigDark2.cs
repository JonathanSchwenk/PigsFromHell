using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class FirePigDark2 : MonoBehaviour
{
    private int health = 3;

    private ISpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = ServiceLocator.Resolve<ISpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "DefaultBullet") {
            health -= 1;
            print(health);
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
            }
        }
        // Repeat for all projectiles
    }
}
