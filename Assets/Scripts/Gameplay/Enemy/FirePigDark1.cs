using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class FirePigDark1 : MonoBehaviour
{
    private float health = 4;
    private int pointValue = 20;

    private ISpawnManager spawnManager;
    private IGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = ServiceLocator.Resolve<ISpawnManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        // Normal weapons
        if (other.tag == "LargeBullet") {
            health -= 1;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "MediumBullet") {
            health -= 0.5f;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "SmallBullet") {
            health -= 0.25f;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }

        // Special weapons Rocket Arrow Fire RailGunBolt
        if (other.tag == "Rocket") {
            health -= 1f;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "Arrow") {
            health -= 1f;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "Fire") {
            health -= 0.05f;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "RailGunBolt") {
            health -= 1f;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fire") {
            health -= 0.05f;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
    }
}
