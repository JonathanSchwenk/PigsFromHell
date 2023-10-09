using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class SpeedDrop : MonoBehaviour
{

    private IGameManager gameManager;
    private IAudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
        audioManager = ServiceLocator.Resolve<IAudioManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            audioManager.PlaySFX("PickupDrop");

            gameManager.UpdateDrops("Speed");

            gameObject.SetActive(false);
        }
    }
}