using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class FireBullet : MonoBehaviour
{
    public int impact = 1;
    public float damage = 1;


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
            GameObject groundFire = objectPooler.SpawnFromPool("GroundFire", gameObject.transform.position, Quaternion.identity);
            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(GroundFireCollider(groundFire, gameObject));

            // Teleports actual bullet away so it doesn't keep hitting stuff but also is still active for the animation
            gameObject.transform.position = new Vector3(0,-10000,0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            
        }
        if (other.tag == "Enemy") {
            GameObject groundFire = objectPooler.SpawnFromPool("GroundFire", gameObject.transform.position, Quaternion.identity);
            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(GroundFireCollider(groundFire, gameObject));

            // Teleports actual bullet away so it doesn't keep hitting stuff but also is still active for the animation
            gameObject.transform.position = new Vector3(0,-10000,0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
    }


    IEnumerator GroundFireCollider(GameObject groundFire, GameObject projectile) {
        yield return new WaitForSeconds(2.5f);
        groundFire.SetActive(false);
        impact -= 1;
        if (impact < 1) {
            projectile.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            projectile.SetActive(false);
        }
    }
}
