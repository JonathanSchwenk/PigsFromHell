using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float Speed;
    [SerializeField] private float movementThresholdX;
    [SerializeField] private float movementThresholdZ;
    [SerializeField] private GameObject transparentMat;


    private Vector3 moveTemp;
    private bool moveCamera;
    private float xDiff;
    private float zDiff;


    private IGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3 velocity = Vector3.zero;
    private List<Collider> colliders = new List<Collider>();

    [SerializeField] private float dampening;
    [SerializeField] private Vector3 offset;

    /*
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x) {
            xDiff = player.transform.position.x - transform.position.x;
        } else {
            xDiff = transform.position.x - player.transform.position.x;
        }
        if (player.transform.position.z > transform.position.z) {
            zDiff = player.transform.position.z - transform.position.z;
        } else {
            zDiff = transform.position.z - player.transform.position.z;
        }

        if (xDiff >= movementThresholdX) {
            moveTemp.x = player.transform.position.x;
            moveTemp.y = 15;
            //transform.position = Vector3.MoveTowards(transform.position, moveTemp, Speed * Time.deltaTime);
        }
        if (zDiff >= movementThresholdZ) {
            moveTemp.z = player.transform.position.z;
            moveTemp.y = 15;
            //transform.position = Vector3.MoveTowards(transform.position, moveTemp, Speed * Time.deltaTime);
        }
    }
    */

    private void FixedUpdate() {
        Vector3 movePosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, dampening);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (!colliders.Contains(other)) { 
            colliders.Add(other);

            print(other);

            // Make parent of collider transparent
            // Could try to change the material of the other object to transparent. I think this would work but I think it would 
            // change anything with that material to be transparent which could result in unwanted things being transparent
            // Next option is to make a custom material that is transparent and the other object changes to this material
            
            other.gameObject.transform.GetComponent<MeshRenderer>().material = transparentMat.transform.GetComponent<MeshRenderer>().material;
        }
    }

    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);

        // Make parent of collider not transparent anymore
    }
}

