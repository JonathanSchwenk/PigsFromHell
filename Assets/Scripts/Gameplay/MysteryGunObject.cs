using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class MysteryGunObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject buttonParentObject;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject takeGunButton;

    private float minDist = 5;
    private float dist;

    private IGameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }

    void Update()
    {
        // if the player is within a certain distance from the spot then pop up a UI button that allows you to buy a gun 
        dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if(dist < minDist)
        {
            gameManager.activeBuyObject = gameObject;
            buttonParentObject.SetActive(true);

            if (takeGunButton.activeSelf == true) {
                buyButton.SetActive(false);
            } else {
                buyButton.SetActive(true);
            }
        } else {
            buttonParentObject.SetActive(false);
        }
    }
}
