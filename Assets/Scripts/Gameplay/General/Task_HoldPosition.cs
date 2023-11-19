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
    private IAudioManager audioManager;

    private float totalCounterNeeded;
    private float playersCurrentCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
        audioManager = ServiceLocator.Resolve<IAudioManager>();

        totalCounterNeeded = 10.0f;
        playersCurrentCounter = 0;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            audioManager.PlaySFX("HoldingPosition");
        }
    }

    private void OnTriggerStay(Collider other) {
        // Try printing out to see how fast the counter goes up (Might need time.deltatime)
        if (other.gameObject.tag == "Player") {
            playersCurrentCounter += (1 * Time.deltaTime);

            if (playersCurrentCounter >= 2 && particleBeam1.activeSelf == false) {
                particleBeam1.SetActive(true);
                audioManager.PlaySFX("HoldPosThreshold");
            }
            if (playersCurrentCounter >= 4 && particleBeam2.activeSelf == false) {
                particleBeam2.SetActive(true);
                audioManager.PlaySFX("HoldPosThreshold");
            }
            if (playersCurrentCounter >= 6 && particleBeam3.activeSelf == false) {
                particleBeam3.SetActive(true);
                audioManager.PlaySFX("HoldPosThreshold");
            }
            if (playersCurrentCounter >= 8 && particleBeam4.activeSelf == false) {
                particleBeam4.SetActive(true);
                audioManager.PlaySFX("HoldPosThreshold");
            }

            if (playersCurrentCounter >= totalCounterNeeded) {
                audioManager.StopSFX("HoldingPosition");
                gameManager.UpdateTasks(gameObject);
            }

        }
    }
}
