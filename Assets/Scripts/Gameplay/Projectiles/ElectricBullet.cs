using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class ElectricBullet : MonoBehaviour
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
            GameObject blast = objectPooler.SpawnFromPool("ElectricBlast", gameObject.transform.position, Quaternion.identity);
            blast.GetComponent<ElectricBlast>().damage = gameObject.GetComponent<ElectricBullet>().damage;
            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(ElectricBlastAnimation(blast, gameObject));
            StartCoroutine(ElectricBlastCollider(blast));

            // Teleports actual bullet away so it doesn't keep hitting stuff but also is still active for the animation
            gameObject.transform.position = new Vector3(0,-10000,0);
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);

            // explosion.transform.GetChild(1).gameObject.SetActive(false); // if this doesnt work then just deal with the damage after

            audioManager.PlaySFX("Explosion");
        }
    }

    IEnumerator ElectricBlastAnimation(GameObject blast, GameObject projectile) {
        yield return new WaitForSeconds(2.5f);
        blast.SetActive(false);
        projectile.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        projectile.SetActive(false);
    }

    IEnumerator ElectricBlastCollider(GameObject blast) {
        yield return new WaitForSeconds(0.5f);
        blast.transform.GetChild(1).gameObject.SetActive(false);
    }
}