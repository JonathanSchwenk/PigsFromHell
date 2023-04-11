using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int health = 1;

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
            health -= 1;
            print(health);
            if (health < 1) {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                gameObject.SetActive(false);
            }
        }
        if (other.tag == "Enemy") {
            health -= 1;
            print(health);
            if (health < 1) {
                gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
                gameObject.SetActive(false);
            }
        }
    }
}
