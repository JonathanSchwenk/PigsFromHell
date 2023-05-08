using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData
{
    // This is a custom weapon data structure

    // Impact and damage is set for each bullet and get on the pig colliders

    public string name;
    public Vector3 firePointPos;
    public float fireRate;
    public float movementSpeed;
    public int bulletForce;
    public int magSize;
    public int totalAmmo;
    public int impact;
    public float damage;

    public int bulletsInMag;
    public int reserveAmmo;
    public int starValue;

/*
    public WeaponData(string inputName, Vector3 inputFirePointPos, float inputFireRate, float inputMovementSpeed, int inputBulletForce, int inputMagSize, int inputTotalAmmo, int inputImpact, float inputDamage) {
        name = inputName;
        firePointPos = inputFirePointPos;
        fireRate = inputFireRate;
        movementSpeed = inputMovementSpeed;
        bulletForce = inputBulletForce;
        magSize = inputMagSize;
        totalAmmo = inputTotalAmmo;
        impact = inputImpact;
        damage = inputDamage;
    }
*/
}
