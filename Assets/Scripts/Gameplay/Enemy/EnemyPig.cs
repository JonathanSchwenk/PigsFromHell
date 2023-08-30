using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

/*
- when each enemy spawns in I need to set the health and point value
*/

public class EnemyPig : MonoBehaviour
{
    public float health = 4;
    public int pointValue = 20;

    private ISpawnManager spawnManager;
    private IGameManager gameManager;
    private IObjectPooler objectPooler;


    private float groundFireDamange;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = ServiceLocator.Resolve<ISpawnManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
        objectPooler = ServiceLocator.Resolve<IObjectPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        // Want to make it so when the player is a certian distance away or further the pig teleports to a spawner in range
        // Problem with that is I have no way of referencing the spawners.

        // Possible solution is to just despawn the enemy and respawn it at a closer spawner.
        // Problem with this is the pig could regain all of its health which might not be the wordt problem
        // Would still subtract the number of enemies by 1 but would have to add the bankValue back the value of the pig so
        // somehow I have to get the pig value. Maybe getting the name and go from there. I have conserns about the (Clone) at the end of each name
    }

    private void OnTriggerEnter(Collider other) {

        // If it gets hit, deal the damage and if the pig dies then do the corresponding stuff
        // Need to know what script to get

        // Normal Weapons
        if (other.tag == "LargeBullet" || other.tag == "MediumBullet" || other.tag == "SmallBullet") {
            health -= other.GetComponent<Bullet>().damage;
            //print(health);
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }


        // Special weapons Rocket Arrow Fire RailGunBolt
        if (other.tag == "Rocket") {
            health -= other.GetComponent<Rocket>().damage;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "Arrow") {
            health -= other.GetComponent<Arrow>().damage;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "Fire") {
            groundFireDamange = other.GetComponent<FireBullet>().damage;
            health -= other.GetComponent<FireBullet>().damage;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "RailGunBolt") {
            health -= other.GetComponent<RailGunBolt>().damage;
            if (health <= 0) {
                Drop();
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
        if (other.tag == "GroundFire") {
            health -= groundFireDamange;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
    }



    // Make a spawn drops on death function
    private void Drop() {
        int shouldDrop = Random.Range(1, 100);

        if (shouldDrop <= 10) {
            // Should drop
            int whichDrop = Random.Range(1, 5);

            if (whichDrop == 1) {
                // drop max ammo
                GameObject drop = objectPooler.SpawnFromPool("MaxAmmoDrop", gameObject.transform.position, Quaternion.identity);

            } else if (whichDrop == 2) {
                // drop instaKill
                GameObject drop = objectPooler.SpawnFromPool("InstaKillDrop", gameObject.transform.position, Quaternion.identity);
            } else if (whichDrop == 3) {
                // drop instaKill
                GameObject drop = objectPooler.SpawnFromPool("HealthDrop", gameObject.transform.position, Quaternion.identity);
            } else if (whichDrop == 4) {
                // drop instaKill
                GameObject drop = objectPooler.SpawnFromPool("SpeedDrop", gameObject.transform.position, Quaternion.identity);
            } else if (whichDrop == 5) {
                // drop instaKill
                GameObject drop = objectPooler.SpawnFromPool("ImpactDrop", gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
