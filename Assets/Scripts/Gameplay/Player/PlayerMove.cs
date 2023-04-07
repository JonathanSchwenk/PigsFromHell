using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private FloatingJoystick movementJoystick;
    [SerializeField] private float speed;
    [SerializeField] Animator animator;


    private Vector3 movement;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movement joystick moves player
        movement.x = movementJoystick.Horizontal * speed;
        movement.z = movementJoystick.Vertical * speed;
    }

    // Fixed update applies to rigidbodys
    private void FixedUpdate() {
        // Player movement
        gameObject.GetComponent<Rigidbody>().MovePosition(gameObject.GetComponent<Rigidbody>().position + movement * speed * Time.fixedDeltaTime);
        
        // Running animation
        // animation can be set based off both direction and movement
        if (movementJoystick.Horizontal != 0 || movementJoystick.Vertical != 0) { 
            animator.SetBool("Idle", false);
            animator.SetBool("RunningFWD", true);
        } else {
            animator.SetBool("Idle", true);
            animator.SetBool("RunningFWD", false);
        }
    }
}
