using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class PlayerMove : MonoBehaviour
{

    [SerializeField] private FloatingJoystick movementJoystick;
    [SerializeField] Animator animator;

    private IGameManager gameManager;

    private Vector3 movement;
    private float speed;
    


    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
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
        // Sets speed based off speedDrop
        // This will changeg to the speed value from thte weapon date for thte active gun from the game manager.
        if (gameManager.dropsList.Contains("Speed")) {
            speed = gameManager.activeWeapon.movementSpeed + 0.5f;
        } else {
            speed = gameManager.activeWeapon.movementSpeed;
        }
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
