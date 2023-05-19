using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject damageIndecator;


    private int health;
    private int maxHealth;
    private float healthRecoveryRate;
    private float healthRecoveryCounter;
    private float timeToWaitBeforeRecovery;
    private float timeToWaitBeforeRecoveryCounter;

    private IGameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();

        // Player Health (These will be set from the game manager and savemanager depending on armor...)
        health = 5;
        maxHealth = 5;

        // Health recovery active timer init (1 is kinda fast, maybe try 2)
        healthRecoveryRate = 1.0f; //
        healthRecoveryCounter = healthRecoveryRate;

        // Health Recovery time to wait before you start getting health back (2 is a bit too fast, maybe try 4-5)
        timeToWaitBeforeRecovery = 2.0f; 
        timeToWaitBeforeRecoveryCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        DamageIndecator();

        if (health < maxHealth) {
            timeToWaitBeforeRecoveryCounter += 1 * Time.deltaTime;
            if (timeToWaitBeforeRecoveryCounter >= timeToWaitBeforeRecovery) {
                RecoverHealth();
            }
        }


    }


    private void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "Enemy") {
            health -= 1;
            timeToWaitBeforeRecoveryCounter = 0;
            //print("Player damaged");

            
            // Sets the enemies script animation counter to the current times ran
            // This basically resets it to start at whatever the actual number of times thet animation finished
            // Resets the counter so if the contact breaks and starts again, it will still damage accoridingly
            other.collider.gameObject.GetComponent<EnemyAnimationScript>().animationTimesFinished = (int)other.collider.gameObject.GetComponent<EnemyAnimationScript>().animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (health <= 0) {
                // dead
                print("Player died, Game Over");
                gameManager.UpdateGameState(GameState.GameOver);
            }
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.collider.tag == "Enemy") {
            if (other.collider.gameObject.GetComponent<EnemyAnimationScript>().DealDamage() == true) {
                print("Players Health: " + health);
                // Deals damage
                health -= 1;
                timeToWaitBeforeRecoveryCounter = 0;

                if (health <= 0) {
                    // dead
                    print("Player died, Game Over");
                    gameManager.UpdateGameState(GameState.GameOver);
                }
            }
        }
    }


    private void RecoverHealth() {

        healthRecoveryCounter += 1 * Time.deltaTime;

        if (healthRecoveryCounter >= healthRecoveryRate) {
            if (health < maxHealth) {
                health += 1;
                healthRecoveryCounter = 0;
            }
        }
    }


    private void DamageIndecator() {
        if (health == 5) {
            damageIndecator.transform.GetChild(0).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(1).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(2).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(3).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(4).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(5).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(6).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(7).gameObject.SetActive(false);
        } else if (health == 4) {
            damageIndecator.transform.GetChild(0).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(1).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(2).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(3).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(4).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(5).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(6).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(7).gameObject.SetActive(false);
        } else if (health == 3) {
            damageIndecator.transform.GetChild(0).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(1).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(2).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(3).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(4).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(5).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(6).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(7).gameObject.SetActive(false);
        } else if (health == 2) {
            damageIndecator.transform.GetChild(0).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(1).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(2).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(3).gameObject.SetActive(false);
            damageIndecator.transform.GetChild(4).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(5).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(6).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(7).gameObject.SetActive(false);
        } else if (health == 1) {
            damageIndecator.transform.GetChild(0).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(1).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(2).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(3).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(4).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(5).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(6).gameObject.SetActive(true);
            damageIndecator.transform.GetChild(7).gameObject.SetActive(true);
        }
        
    }

}
