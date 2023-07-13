using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class Task_HoldPosition : MonoBehaviour
{
    [SerializeField] private GameObject particleBeam1;
    [SerializeField] private GameObject particleBeam2;
    [SerializeField] private GameObject particleBeam3;
    [SerializeField] private GameObject particleBeam4;

    private IGameManager gameManager;

    private float totalCounterNeeded;
    private float playersCurrentCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();

        totalCounterNeeded = 15.0f;
        playersCurrentCounter = 0;
    }

    private void OnTriggerStay(Collider other) {
        // Try printing out to see how fast the counter goes up (Might need time.deltatime)
        if (other.gameObject.tag == "Player") {
            playersCurrentCounter += (1 * Time.deltaTime);

            if (playersCurrentCounter >= 3 && particleBeam1.activeSelf == false) {
                particleBeam1.SetActive(true);
            }
            if (playersCurrentCounter >= 6 && particleBeam2.activeSelf == false) {
                particleBeam2.SetActive(true);
            }
            if (playersCurrentCounter >= 9 && particleBeam3.activeSelf == false) {
                particleBeam3.SetActive(true);
            }
            if (playersCurrentCounter >= 12 && particleBeam4.activeSelf == false) {
                particleBeam4.SetActive(true);
            }

            if (playersCurrentCounter >= totalCounterNeeded) {
                gameManager.UpdateTasks(gameObject);
            }

        }
    }
}
