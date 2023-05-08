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
    private IGameManager gameManager;


    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();

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

        activeWeapon = gameManager.activeWeapon;

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

        activeWeapon = gameManager.activeWeapon;

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
                if (shotsInMag > 0 || shotsInMag < 0) {
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
        if (gameManager.activeWeapon.name != "Knife") {
            // Make the FirePoint the last child in each weapon (Try this first, if it doesn't work then use find i guess)

            // Shoot animation
            if (movementJoystick.Horizontal == 0 || movementJoystick.Vertical == 0) { 
                animator.SetTrigger("Shoot");
                animator.SetBool("Idle", false);
            }


            // Spawn Bullets

            // Large Bullets (Hunting Rifle, Sniper)
            if (gameManager.activeWeapon.name == "Hunting Rifle" || gameManager.activeWeapon.name == "Sniper") { 
                GameObject projectile = objectPooler.SpawnFromPool("LargeBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                AccessBullets(projectile, "Bullet");
            }
            // Medium bullets (Pistol, Assault Rifle)
            else if (gameManager.activeWeapon.name == "Pistol" || gameManager.activeWeapon.name == "Assault Rifle") {
                GameObject projectile = objectPooler.SpawnFromPool("MediumBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                AccessBullets(projectile, "Bullet");
            }
            // Small Bullets (HMG, SMG, Shotgun, Mini Gun)
            else if (gameManager.activeWeapon.name == "HMG" || gameManager.activeWeapon.name == "SMG" ||
            gameManager.activeWeapon.name == "Shotgun" || gameManager.activeWeapon.name == "Mini Gun") {
                if (gameManager.activeWeapon.name == "Shotgun") {
                    for (int i = 0; i < 10; i++) { // 10 pellets in shotgun
                        float maxSpread = 1.0f;
                        Vector3 dir = transform.forward + new Vector3(rightFirePoint.transform.forward.x + Random.Range(-maxSpread,maxSpread), rightFirePoint.transform.forward.y, rightFirePoint.transform.forward.z + Random.Range(-maxSpread,maxSpread));
                        GameObject projectile = objectPooler.SpawnFromPool("SmallBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                        projectile.GetComponent<Rigidbody>().AddForce((rightFirePoint.transform.forward + dir) * 2, ForceMode.Impulse);
                        AccessBullets(projectile, "Bullet");
                    }
                } else {
                    GameObject projectile = objectPooler.SpawnFromPool("SmallBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                    projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                    AccessBullets(projectile, "Bullet");
                }
            }
            // Flame Thrower
            else if (gameManager.activeWeapon.name == "Flame Thrower") {
                GameObject projectile = objectPooler.SpawnFromPool("FireBullet", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                AccessBullets(projectile, "Fire");
            }
            // CrossBow
            else if (gameManager.activeWeapon.name == "Cross Bow") {
                GameObject projectile = objectPooler.SpawnFromPool("Arrow", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), gameObject.transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                AccessBullets(projectile, "Arrow");
            }
            // RPG
            else if (gameManager.activeWeapon.name == "RPG") {
                GameObject projectile = objectPooler.SpawnFromPool("Rocket", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), gameObject.transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                AccessBullets(projectile, "Rocket");
            }
            // Rail Gun
            else if (gameManager.activeWeapon.name == "Rail Gun") {
                GameObject projectile = objectPooler.SpawnFromPool("RailGunBolt", new Vector3(rightFirePoint.transform.position.x, rightFirePoint.transform.position.y, rightFirePoint.transform.position.z), gameObject.transform.rotation);
                projectile.GetComponent<Rigidbody>().AddForce(rightFirePoint.transform.forward * bulletForce, ForceMode.Impulse);
                AccessBullets(projectile, "RailGunBolt");

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


    // use this function above when spawning bullets and then go into each pigs script and change how they get damaged then test
    private void AccessBullets(GameObject go, string projectileType) {
        // repeat for all bullets
        if (projectileType == "Bullet") {
            go.GetComponent<Bullet>().damage = gameManager.activeWeapon.damage * gameManager.activeWeapon.starValue;
            go.GetComponent<Bullet>().impact = gameManager.activeWeapon.impact;
        } else if (projectileType == "Fire") {
            go.GetComponent<FireBullet>().damage = gameManager.activeWeapon.damage * gameManager.activeWeapon.starValue;
            go.GetComponent<FireBullet>().impact = gameManager.activeWeapon.impact;
        } else if (projectileType == "Arrow") {
            go.GetComponent<Arrow>().damage = gameManager.activeWeapon.damage * gameManager.activeWeapon.starValue;
            go.GetComponent<Arrow>().impact = gameManager.activeWeapon.impact;
        } else if (projectileType == "Rocket") {
            go.GetComponent<Rocket>().damage = gameManager.activeWeapon.damage * gameManager.activeWeapon.starValue;
            go.GetComponent<Rocket>().impact = gameManager.activeWeapon.impact;
        } else if (projectileType == "RailGunBolt") {
            go.GetComponent<RailGunBolt>().damage = gameManager.activeWeapon.damage * gameManager.activeWeapon.starValue;
            go.GetComponent<RailGunBolt>().impact = gameManager.activeWeapon.impact;
        }
        
    }
}
