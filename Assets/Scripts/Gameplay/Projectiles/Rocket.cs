using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class Rocket : MonoBehaviour
{
    public int health = 1;
    private IObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        // Null check for service debugging
        if (ServiceLocator.IsRegistered<IObjectPooler>()) {
            objectPooler = ServiceLocator.Resolve<IObjectPooler>();
        } else {
            print("ERROR service has not been registered yet");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enviornment") {
            GameObject explosion = objectPooler.SpawnFromPool("Explosion", gameObject.transform.position, Quaternion.identity);
            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(GroundFireCollider(explosion, gameObject));

            // Teleports actual bullet away so it doesn't keep hitting stuff but also is still active for the animation
            gameObject.transform.position = new Vector3(0,-10000,0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            explosion.transform.GetChild(1).gameObject.SetActive(false); // if this doesnt work then just deal with the damage after
        }
        if (other.tag == "Enemy") {
            GameObject explosion = objectPooler.SpawnFromPool("Explosion", gameObject.transform.position, Quaternion.identity);
            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(GroundFireCollider(explosion, gameObject));

            // Teleports actual bullet away so it doesn't keep hitting stuff but also is still active for the animation
            gameObject.transform.position = new Vector3(0,-10000,0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            explosion.transform.GetChild(1).gameObject.SetActive(false); // if this doesnt work then just deal with the damage after
        }
    }

    IEnumerator GroundFireCollider(GameObject explosion, GameObject projectile) {
        yield return new WaitForSeconds(2.5f);
        explosion.SetActive(false);
        health -= 1;
        if (health < 1) {
            projectile.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            projectile.SetActive(false);
        }
    }
}
