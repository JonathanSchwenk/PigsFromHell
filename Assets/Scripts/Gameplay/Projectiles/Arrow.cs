using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int impact = 1;
    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Enviornment") {
            // I want the bullets to no matter what deactivate because I don't want them flying through the walls
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            gameObject.SetActive(false);
        }
        if (other.tag == "Enemy") {
            impact -= 1;
            //print(health);
            if (impact < 1) {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                gameObject.SetActive(false);
            }
        }
    }
}
