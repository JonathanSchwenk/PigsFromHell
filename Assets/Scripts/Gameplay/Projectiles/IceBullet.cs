using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : MonoBehaviour
{
    public int impact = 1;
    public float damage = 1;


    private void OnTriggerEnter(Collider other) {
        print(other.tag);
        if (other.tag == "Enviornment") {
            // I want the bullets to no matter what deactivate because I don't want them flying through the walls
            //print(damage);
            //impact -= 1;
            //if (impact < 1) {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            gameObject.SetActive(false);
            //}
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
