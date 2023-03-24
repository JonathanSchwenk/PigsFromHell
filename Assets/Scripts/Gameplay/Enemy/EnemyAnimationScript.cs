using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAnimationScript : MonoBehaviour
{
    private GameObject target;
    [SerializeField] public  Animator animator;

    private float distanceFromTarget;
    private float pigAttackRange;
    public int animationTimesFinished; // everytime it enters collition, this gets set to the number of times the animation ran (Resetting it)

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        pigAttackRange = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = (float)Math.Sqrt((Math.Pow(target.transform.position.x - transform.position.x, 2) + Math.Pow(target.transform.position.z - transform.position.z, 2)));

        if (distanceFromTarget <= pigAttackRange) {
            // Attack
            animator.SetInteger("animation", 3);
        } else {
            // Run
            animator.SetInteger("animation", 2);
        }
    }


    public bool DealDamage() {
        // Checks to see if animation has finished, if yes then return true, else return false
        if ((int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime) - animationTimesFinished == 1) { 
            animationTimesFinished = (int)(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            return true;
        } else {
            return false;
        }
    }
}


