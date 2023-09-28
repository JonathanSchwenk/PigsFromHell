using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class Rocket : MonoBehaviour
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
        if (other.tag == "Enviornment" || other.tag == "Enemy") {
            GameObject explosion = objectPooler.SpawnFromPool("Explosion", gameObject.transform.position, Quaternion.identity);
            explosion.GetComponent<RocketExplosion>().damage = gameObject.GetComponent<Rocket>().damage;

            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(RocketExplosionAnimation(explosion, gameObject));
            StartCoroutine(RocketExplosionCollider(explosion));

            // Teleports actual bullet away so it doesn't keep hitting stuff but also is still active for the animation
            gameObject.transform.position = new Vector3(0,-10000,0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            audioManager.PlaySFX("Explosion");
        }
    }

    IEnumerator RocketExplosionAnimation(GameObject explosion, GameObject projectile) {
        yield return new WaitForSeconds(2.5f);
        explosion.SetActive(false);
        impact -= 1;
        if (impact < 1) {
            projectile.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            projectile.SetActive(false);
        }
    }
    IEnumerator RocketExplosionCollider(GameObject explosion) {
        yield return new WaitForSeconds(0.5f);
        explosion.transform.GetChild(1).gameObject.SetActive(false);
    }
}
