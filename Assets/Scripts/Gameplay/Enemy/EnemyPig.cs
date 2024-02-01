using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using UnityEngine.AI;

/*
- when each enemy spawns in I need to set the health and point value
*/

public class EnemyPig : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;

    public float health = 4;
    public int pointValue = 20;
    private float maxDistanceAway = 75;

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
        
        // If the pig is too far away from the player it respawns closer
        RespawnPig();
        
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

        // Knife Weapons
        if (other.tag == "Knife") {
            health -= 10;
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
            health -= other.GetComponent<Rocket>().damage; // This might be wrong. Might need separate value for damage for explosion
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "RocketExplosion") {
            health -= other.GetComponent<RocketExplosion>().damage; // This might be wrong. Might need separate value for damage for explosion
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
        if (other.tag == "PoisonShot") {
            health -= other.GetComponent<PoisonShot>().damage;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "ElectricBullet") {
            health -= other.GetComponent<ElectricBullet>().damage; // Need separate value / tag
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "GravityShot") {
            health -= other.GetComponent<GravityShot>().damage;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "SawBlade") {
            health -= other.GetComponent<SawBlade>().damage;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "IceBullet") {
            health -= other.GetComponent<IceBullet>().damage;
            float localSpeed = gameManager.enemySpeed;
            navMeshAgent.speed = localSpeed -= 0.5f;
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
        if (other.tag == "PoisonSmoke") {
            // If you shoot this and then switch then it will damage equal to that new weapon. Theres no super easy way to
            // fix this so going with it. Doesn't really matter tho bc it shreds anyway.
            health -= gameManager.activeWeapon.damage * gameManager.activeWeapon.starValue;
            if (health <= 0) {
                Drop();
                gameObject.SetActive(false);
                spawnManager.numEnemies -= 1;
                gameManager.points += pointValue;
            }
        }
        if (other.tag == "ElectricBlast") {
            // If you shoot this and then switch then it will damage equal to that new weapon. Theres no super easy way to
            // fix this so going with it. Doesn't really matter tho bc it shreds anyway.
            health -= gameManager.activeWeapon.damage * gameManager.activeWeapon.starValue;
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

    // If the enemy pig is too far from the player then it despawns and gets respawned closer.
    // This helps so in the early round when the pigs are slow if you are super far away you don't have to wait for the pigs to finally get
    // to you, they just respawn closer. 
    private void RespawnPig() {
        float distance = Vector3.Distance(gameObject.transform.position, gameManager.playerGOGlobal.transform.position);

        if (distance > maxDistanceAway) {
            // Despawn pig
            gameObject.SetActive(false);
            spawnManager.numEnemies -= 1;

            if (gameObject.name == "FirePigLight2(Clone)") {
                print("FirePigLight2");
                spawnManager.bankValue += 1;
            }
            if (gameObject.name == "FirePigLight1(Clone)") {
                print("FirePigLight1");
                spawnManager.bankValue += 2;
            }
            if (gameObject.name == "FirePigDark2(Clone)") {
                print("FirePigDark2");
                spawnManager.bankValue += 3;
            }
            if (gameObject.name == "FirePigDark1(Clone)") {
                print("FirePigDark1");
                spawnManager.bankValue += 4;
            }
        }
    }
}
