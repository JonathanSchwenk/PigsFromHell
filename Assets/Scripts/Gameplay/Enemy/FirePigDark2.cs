using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class FirePigDark2 : MonoBehaviour
{
    private float health = 3;
    private int pointValue = 15;

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

        // If it gets hit, deal the damage and if the pig dies then do the corresponding stuff
        // Need to know what script to get

        // Normal Weapons
        if (other.tag == "LargeBullet" || other.tag == "MediumBullet" || other.tag == "SmallBullet") {
            health -= other.GetComponent<Bullet>().damage;
            //print(health);
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }


        // Special weapons Rocket Arrow Fire RailGunBolt
        if (other.tag == "Rocket") {
            health -= other.GetComponent<Rocket>().damage;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "Arrow") {
            health -= other.GetComponent<Arrow>().damage;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "Fire") {
            health -= other.GetComponent<FireBullet>().damage;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "RailGunBolt") {
            health -= other.GetComponent<RailGunBolt>().damage;
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
            health -= other.GetComponent<FireBullet>().damage;
            gameManager.points += 1;
            if (health <= 0) {
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
    }
}
