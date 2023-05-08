using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int impact = 1;
    public float damage = 1;


    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enviornment") {
            print(damage);
            impact -= 1;
            if (impact < 1) {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                gameObject.SetActive(false);
            }
        }
        if (other.tag == "Enemy") {
            impact -= 1;
            if (impact < 1) {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                gameObject.SetActive(false);
            }
        }
    }
}
