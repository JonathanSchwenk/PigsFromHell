using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class PlayerAimShoot : MonoBehaviour
{
    [SerializeField] private FloatingJoystick movementJoystick;
    [SerializeField] private FloatingJoystick aimShootJoystick;
    [SerializeField] Animator animator;
    [SerializeField] GameObject knifeDamagerCollider;
    [SerializeField] GameObject rightFirePoint;


    private float angle;
    private Vector3 directionPoint;
    //private GameObject currentWeaponRight;
    //private GameObject currentWeaponLeft;
    private string currentWeaponRight;
    private float bulletForce;
    private float firerateTime;
    private float fireRateCounter;

    [SerializeField] PlayerReload playerReloadScript;

    private int totalAmmo;
    private int magSize;
    private int shotsInMag;
    private WeaponData activeWeapon;


    private IObjectPooler objectPooler;
    private ISaveManager saveManager;


    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        } else {
            print("No save manager");
        }

        activeWeapon = new WeaponData();
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }

    private void SaveManagerOnSave(int num) {
        // This happens everytime you save, I think it is fine, want just for when you change weapons. 
        // Could add something else like a bool to check if I wanna actually do something here.

        activeWeapon = saveManager.saveData.activeWeapon;

        bulletForce = activeWeapon.bulletForce;
        firerateTime = activeWeapon.fireRate;
        fireRateCounter = firerateTime; // Might get rid of this. I feel that a slight delay is good for the user to aim.
        magSize = activeWeapon.magSize;
        if (activeWeapon.reserveAmmo > magSize) {
            shotsInMag = magSize;
        } else {
            shotsInMag = activeWeapon.reserveAmmo;
        }

        rightFirePoint.transform.localPosition = activeWeapon.firePointPos;
    }


    // Start is called before the first frame update
    void Start()
    {
        // Null check for service debugging
        if (ServiceLocator.IsRegistered<IObjectPooler>()) {
            objectPooler = ServiceLocator.Resolve<IObjectPooler>();
        } else {
            print("ERROR service has not been registered yet");
        }

        activeWeapon = saveManager.saveData.activeWeapon;

        bulletForce = activeWeapon.bulletForce;
        firerateTime = activeWeapon.fireRate;
        fireRateCounter = firerateTime; // Might get rid of this. I feel that a slight delay is good for the user to aim.
        magSize = activeWeapon.magSize;
        if (activeWeapon.reserveAmmo > magSize) {
            shotsInMag = magSize;
        } else {
            shotsInMag = activeWeapon.reserveAmmo;
        }

        rightFirePoint.transform.localPosition = activeWeapon.firePointPos;

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
                } 
            }
        } 
    }


    private void Shoot() {
        // If thte weapon is a gun then set animation and spawn bullets, else spawn knife 
        if (currentWeaponRight != "Knife") {
            // Make the FirePoint the last child in each weapon (Try this first, if it doesn't work then use find i guess)

            // Shoot animation
            if (movementJoystick.Horizontal == 0 || movementJoystick.Vertical == 0) { 
                animator.SetTrigger("Shoot");
                animator.SetBool("Idle", false);
            }


            // Spawn Bullets

            // Large Bullets (Hunting Rifle, Sniper)
            if (saveManager.saveData.activeWeapon.name == "Hunting Rifle" || saveManager.saveData.activeWeapon.name == "Sniper") { 
                GameObject projectile = objectPooler.SpawnFromPool("LargeBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
            }
            // Medium bullets (Pistol, Assault Rifle)
            else if (saveManager.saveData.activeWeapon.name == "Pistol" || saveManager.saveData.activeWeapon.name == "Assault Rifle") {
                GameObject projectile = objectPooler.SpawnFromPool("MediumBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
            }
            // Small Bullets (HMG, SMG, Shotgun, Mini Gun)
            else if (saveManager.saveData.activeWeapon.name == "HMG" || saveManager.saveData.activeWeapon.name == "SMG" ||
            saveManager.saveData.activeWeapon.name == "Shotgun" || saveManager.saveData.activeWeapon.name == "Mini Gun") {
                if (saveManager.saveData.activeWeapon.name == "Shotgun") {
                    for (int i = 0; i < 10; i++) { // 10 pellets in shotgun
                        float maxSpread = 1.0f;
                        Vector3 dir = transform.forward + new Vector3(rightFirePoint.transform.forward.x + Random.Range(-maxSpread,maxSpread), rightFirePoint.transform.forward.y, rightFirePoint.transform.forward.z + Random.Range(-maxSpread,maxSpread));
                        GameObject projectile = objectPooler.SpawnFromPool("SmallBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                        projectile.GetComponent<Rigidbody>().AddForce((rightFirePoint.transform.forward + dir) * 2, ForceMode.Impulse);
                    }
                } else {
                    GameObject projectile = objectPooler.SpawnFromPool("SmallBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                    projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                }
            }
            // Flame Thrower
            else if (saveManager.saveData.activeWeapon.name == "Flame Thrower") {
                GameObject projectile = objectPooler.SpawnFromPool("FireBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
            }
            // CrossBow
            else if (saveManager.saveData.activeWeapon.name == "Cross Bow") {
                GameObject projectile = objectPooler.SpawnFromPool("Arrow", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), gameObject.transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
            }
            // RPG
            else if (saveManager.saveData.activeWeapon.name == "RPG") {
                GameObject projectile = objectPooler.SpawnFromPool("Rocket", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), gameObject.transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
            }
            // Rail Gun
            else if (saveManager.saveData.activeWeapon.name == "Rail Gun") {
                GameObject projectile = objectPooler.SpawnFromPool("RailGunBolt", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), gameObject.transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);

                GameObject boltEffect = objectPooler.SpawnFromPool("BoltEffect", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), gameObject.transform.rotation);
                // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
                StartCoroutine(RailGunEffect(boltEffect));
            }



            // Lower shots in mag
            shotsInMag -= 1;
            activeWeapon.bulletsInMag -= 1;
        } else {
            // Shoot animation
            animator.SetTrigger("Shoot");
            animator.SetBool("Idle", false);

            // spawn an object that appears for a short time and then dissapears. This object will damage enemies.
            StartCoroutine(KnifeDamageCollider());
        }
    }




    IEnumerator KnifeDamageCollider() {
        knifeDamagerCollider.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        knifeDamagerCollider.SetActive(false);
    }
    IEnumerator RailGunEffect(GameObject boltEffect) {
        yield return new WaitForSeconds(1.5f);
        boltEffect.SetActive(false);
    }
}
