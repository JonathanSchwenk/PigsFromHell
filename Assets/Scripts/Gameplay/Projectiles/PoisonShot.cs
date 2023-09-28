using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class PoisonShot : MonoBehaviour
{
    public int impact = 1;
    public float damage = 1;

    private IObjectPooler objectPooler;
    private IAudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        // Null check for service debugging
        if (ServiceLocator.IsRegistered<IObjectPooler>()) {
            objectPooler = ServiceLocator.Resolve<IObjectPooler>();
        } else {
            print("ERROR service has not been registered yet");
        }

        audioManager = ServiceLocator.Resolve<IAudioManager>();
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enviornment") {
            GameObject PoisonSmoke = objectPooler.SpawnFromPool("PoisonSmoke", gameObject.transform.position, Quaternion.identity);
            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(PoisonSmokeGO(PoisonSmoke));

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0); // Stops projectile from moving for next use
            gameObject.SetActive(false); // Removes projectile because its spawning a whole new object 

            audioManager.PlaySFX("Explosion");
        }
        if (other.tag == "Enemy") {
            GameObject PoisonSmoke = objectPooler.SpawnFromPool("PoisonSmoke", gameObject.transform.position, Quaternion.identity);
            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(PoisonSmokeGO(PoisonSmoke));

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0); // Stops projectile from moving for next use
            gameObject.SetActive(false); // Removes projectile because its spawning a whole new object 
            
            audioManager.PlaySFX("Explosion");
        }
    }

    IEnumerator PoisonSmokeGO(GameObject poisonSmoke) {
        yield return new WaitForSeconds(2.5f);
        poisonSmoke.SetActive(false);
    }
}

