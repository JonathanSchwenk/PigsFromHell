using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{

    private int maxEnemiesAllowed = 25; // This is the maximum amount of enemies that can be alive at once (Could be any of the 4 types of enemies) (For now 25)
    private float spawnDelayMin = 5.0f;
    private float spawnDelayMax = 8.0f; 
    private float spawnDelayCounter;
    private float spawnDelayRate;

    // These will be set based off the spawnManager and Roundnumber
    private int enemyStrengthMin = 1;
    private int enemyStrengthMax = 4;
    private float distanceToSpawn = 75;


    private ISpawnManager spawnManager;
    private IObjectPooler objectPooler;
    private IGameManager gameManager;
    private ISaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = ServiceLocator.Resolve<ISpawnManager>();
        objectPooler = ServiceLocator.Resolve<IObjectPooler>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
        saveManager = ServiceLocator.Resolve<ISaveManager>();


        spawnDelayRate = Random.Range(spawnDelayMin, spawnDelayMax);
        spawnDelayCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.canSpawn == true) {
            // Spawn timer delay
            spawnDelayCounter += 1 * Time.deltaTime;

            float distance = Vector3.Distance(gameObject.transform.position, gameManager.playerGOGlobal.transform.position);

            // If the timer allows a pig to spawn and the spawner is in range (Add this in range part)
            // Get distance from this game object to the player and if the distance is within a certain value then an enemy can spawn
            if (spawnDelayCounter >= spawnDelayRate && distance <= distanceToSpawn) {
                // Spawn enemy
                SpawnEnemy();

                // Reset counter and rate values
                spawnDelayCounter = 0;
                spawnDelayRate = Random.Range(spawnDelayMin, spawnDelayMax);
            }
        }
    }


    private void SpawnEnemy() {
        // as the level gets higher I want to spawn stonger enemies

        int enemyStrength = Random.Range(enemyStrengthMin, enemyStrengthMax);
        bool hasSpawned = false;
        // int enemyStrength = 1;

        if (spawnManager.bankValue >= 1 && spawnManager.numEnemies < maxEnemiesAllowed) {
            while (hasSpawned == false) {
                if (enemyStrength == 1) {
                    if (spawnManager.bankValue >= 1) {
                        // Spawn FirePigLight2
                        GameObject enemy = objectPooler.SpawnFromPool("FirePigLight2", gameObject.transform.position, Quaternion.identity);

                        enemy.GetComponent<NavMeshAgent>().Warp(gameObject.transform.position);

                        enemy.GetComponent<EnemyPig>().health = 1;
                        enemy.GetComponent<EnemyPig>().pointValue = 5;

                        enemy.gameObject.GetComponent<NavMeshAgent>().speed = gameManager.enemySpeed;

                        spawnManager.numEnemies += 1;
                        if (saveManager.saveData.gameMode == "Survival") {
                            spawnManager.bankValue -= 1;
                        }

                        hasSpawned = true;
                    }
                }
                if (enemyStrength == 2) {
                    if (spawnManager.bankValue >= 2) {
                        // Spawn FirePigLight1
                        GameObject enemy = objectPooler.SpawnFromPool("FirePigLight1", gameObject.transform.position, Quaternion.identity);

                        enemy.GetComponent<NavMeshAgent>().Warp(gameObject.transform.position);

                        enemy.GetComponent<EnemyPig>().health = 2;
                        enemy.GetComponent<EnemyPig>().pointValue = 10;

                        enemy.gameObject.GetComponent<NavMeshAgent>().speed = gameManager.enemySpeed;

                        spawnManager.numEnemies += 1;
                        if (saveManager.saveData.gameMode == "Survival") {
                            spawnManager.bankValue -= 2;
                        }

                        hasSpawned = true;
                    } else {
                        enemyStrength -= 1;
                    }
                }
                if (enemyStrength == 3) {
                    if (spawnManager.bankValue >= 3) {
                        // Spawn FirePigDark2
                        GameObject enemy = objectPooler.SpawnFromPool("FirePigDark2", gameObject.transform.position, Quaternion.identity);

                        enemy.GetComponent<NavMeshAgent>().Warp(gameObject.transform.position);

                        enemy.GetComponent<EnemyPig>().health = 3;
                        enemy.GetComponent<EnemyPig>().pointValue = 15;

                        enemy.gameObject.GetComponent<NavMeshAgent>().speed = gameManager.enemySpeed;

                        spawnManager.numEnemies += 1;
                        if (saveManager.saveData.gameMode == "Survival") {
                            spawnManager.bankValue -= 3;
                        }

                        hasSpawned = true;
                    } else {
                        enemyStrength -= 1;
                    }
                }
                if (enemyStrength == 4) {
                    if (spawnManager.bankValue >= 4) {
                        // Spawn FirePigDark1
                        GameObject enemy = objectPooler.SpawnFromPool("FirePigDark1", gameObject.transform.position, Quaternion.identity);

                        enemy.GetComponent<NavMeshAgent>().Warp(gameObject.transform.position);

                        enemy.GetComponent<EnemyPig>().health = 4;
                        enemy.GetComponent<EnemyPig>().pointValue = 20;

                        enemy.gameObject.GetComponent<NavMeshAgent>().speed = gameManager.enemySpeed;

                        spawnManager.numEnemies += 1;
                        if (saveManager.saveData.gameMode == "Survival") {
                            spawnManager.bankValue -= 4;
                        }

                        hasSpawned = true;
                    } else {
                        enemyStrength -= 1;
                    }
                }
            }
        }

        
    }

}
