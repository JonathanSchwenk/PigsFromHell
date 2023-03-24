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
    private int animationTimesFinished;


    // Start is called before the first frame update
    void Start()
    {
        magSize = 8;
        shotsInMag = 50; // change to magsize

        // reload helper
        animationTimesFinished = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Reloading") == true) {
            Reload();
        }
    }


    public void Reload() {
        animator.SetBool("Reloading", true);
        animator.SetBool("Idle", false);

        // Reload differently based on weapon
        // if weapon num == certain numbers then reload one at a time, else, reload whole mag

        // Need to do reload cancel for single bullet reload guns so you can reload 4 shots and then shoot and dont have to wait for all 8
        // for now reload one at a time

        // Checks to see if animation has finished, if yes then add a bullet
        if ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime) - animationTimesFinished == 1) { 
            shotsInMag += 1;
            print("add bullet");
            animationTimesFinished = (int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }

        // if mag == full then stop reloading animation
    }
}
