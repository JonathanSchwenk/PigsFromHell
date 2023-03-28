using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour
{
    private float initTime;
    private float timeBeforeDespawn;

    // Start is called before the first frame update
    void Start()
    {
        initTime = Time.time;
        timeBeforeDespawn = 5.0f;

    }

    // Update is called once per frame
    void Update()
    {
        /*
            Wait an amount of time before despawning / doing something (Could be used for range of weapons or damage fall off)
        */
        float diffInTime = Time.time - initTime;
        if (diffInTime > timeBeforeDespawn) {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enviornment") {
            gameObject.SetActive(false);
        }
        if (other.tag == "Enemy") {
            gameObject.SetActive(false);
        }
    }
    
    /*
    // I think that since these are tiggers they dont collide with other objects so its fine and bullets wont hit other bullets

    private void OnCollisionEnter(Collision other) {
        if (other.collider.tag == "DefaultBullet") {
            //gameObject.SetActive(false);
            Physics.IgnoreCollision(other.collider, gameObject.GetComponent<Collider>());
        }
    }
    */

}