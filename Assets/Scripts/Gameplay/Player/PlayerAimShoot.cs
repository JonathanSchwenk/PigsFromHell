using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class PlayerAimShoot : MonoBehaviour
{
    [SerializeField] private FloatingJoystick movementJoystick;
    [SerializeField] private FloatingJoystick aimShootJoystick;
    [SerializeField] Animator animator;
    [SerializeField] GameObject rightFirePoint;
    [SerializeField] GameObject leftFirePoint;


    private float angle;
    private Vector3 directionPoint;
    private GameObject currentWeaponRight;
    private GameObject currentWeaponLeft;
    private float bulletForce;
    private float firerateTime;
    private float fireRateCounter;
    private int totalAmmo;
    private int magSize;
    private int shotsInMag;


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

        //currentWeaponRight = rightHand.transform.GetChild(16).gameObject; // repeat for left when I get to that 
        // Get this from save
        // Activate child (all weapons should start inactive)

        //Bullet force changes based off weapon
        bulletForce = 10.0f;

        // Firerate will change depending on the weapon
        firerateTime = 0.5f; // this is for hunting rife
        fireRateCounter = firerateTime;

        // Number of shots (Mag size) 
        // This will be set based on the weapon
        magSize = 8;
        shotsInMag = 50; // change to magsize
    }

    // Update is called once per frame
    void Update()
    {
        // Player rotation
        angle = Mathf.Atan2(directionPoint.x, directionPoint.z) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

        // Firerate
        fireRateCounter += 1 * Time.deltaTime;

        // AimShoot Joystick turns the play
        if (aimShootJoystick.Horizontal != 0 || aimShootJoystick.Vertical != 0) {
            directionPoint.x = aimShootJoystick.Horizontal;
            directionPoint.z = aimShootJoystick.Vertical;
            // shoot here
            if (fireRateCounter >= firerateTime) {
                if (shotsInMag > 0) {
                    // Shoot actual projectile
                    Shoot();
                    // Resets fire rate
                    fireRateCounter = 0;
                } else {
                    // Reload
                    //Reload(); User must click reload button for now (If I want to change then I can refrence the PlayerReload script and call a public function)
                }
            }
        } else {
            if (movementJoystick.Horizontal == 0 || movementJoystick.Vertical == 0) { 
                if (animator.GetBool("Reloading") == false) {
                    animator.SetBool("Idle", true);
                }
            }
        }
    }


    private void Shoot() {
        // Make the FirePoint the last child in each weapon (Try this first, if it doesn't work then use find i guess)

        // Shoot animation
        if (movementJoystick.Horizontal == 0 || movementJoystick.Vertical == 0) { 
            animator.SetTrigger("Shoot");
            animator.SetBool("Idle", false);
            animator.SetBool("Reloading", false);
        }

        //Transform firePoint = currentWeaponRight.transform.GetChild(currentWeaponRight.transform.childCount - 1); // -1 bc i think childcount will be IndexOutBounds
        // Will add some logic to see which type of projectile should be fired
        // Will have to change the rotation of the spawn object for the direction the players facing (Can do this later with projectiles that aren't circles)
        GameObject projectile = objectPooler.SpawnFromPool("DefaultBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
        projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);

        // Lower shots in mag
        shotsInMag -= 1;
    }
}
