using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class InstaKillDrop : MonoBehaviour
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
            
            gameManager.UpdateDrops("InstaKill");

            // Moves the game object so its no longer visible but still active
            gameObject.transform.position = new Vector3(0, 10000, 0);

            // Start the coroutine
            StartCoroutine(TimeForInstaKill(25f));
        }
    }

    IEnumerator TimeForInstaKill(float waitTime) {
        // Wait some time
        yield return new WaitForSeconds(waitTime);

        // Stop instaKill
        gameManager.UpdateDrops("StopInstaKill");

        // deactivates the gameobject
        gameObject.SetActive(false);
    }
}
