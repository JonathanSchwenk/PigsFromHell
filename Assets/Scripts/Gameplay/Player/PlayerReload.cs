using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReload : MonoBehaviour
{
    [SerializeField] Animator animator;


    private int totalAmmo; // how many bullet the player has currently
    private int ammoCap; // max bullets the player can hold 
    private int magSize;
    private int shotsInMag;
    private string currentWeaponRight;


    // Start is called before the first frame update
    void Start()
    {
        magSize = 8;
        shotsInMag = 7;


        //currentWeaponRight = "Knife";
        // Get this from save
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void Reload() {
        // if the current weapon isn't a knife then reload the gun, else do nothing
        // And check to make sure that the mag isnt full before reloading
        if (currentWeaponRight != "Knife" && shotsInMag < magSize) {
            shotsInMag = magSize;
            print("Reloaded");
            print(shotsInMag);
        } 
    }
}
