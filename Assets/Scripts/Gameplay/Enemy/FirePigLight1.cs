using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class FirePigLight1 : MonoBehaviour
{
    private float health = 2;

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
        
        // Repeat for all projectiles

        if (other.tag == "LargeBullet") {
            health -= 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
            }
        }
        if (other.tag == "MediumBullet") {
            health -= 0.5f;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
            }
        }
        if (other.tag == "SmallBullet") {
            health -= 0.25f;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
            }
        }
    }
}
