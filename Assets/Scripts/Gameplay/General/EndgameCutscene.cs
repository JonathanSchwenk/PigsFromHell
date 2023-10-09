using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class EndgameCutscene : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject gameControlsUI;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject cutsceneCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject nukeAnimation;

    private IAudioManager audioManager;

    private void Awake() {
        audioManager = ServiceLocator.Resolve<IAudioManager>();
    }



    // Start is called before the first frame update
    void Start()
    {
        mainCamera.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        gameplayUI.gameObject.SetActive(false);
        gameControlsUI.gameObject.SetActive(false);

        cutsceneCamera.gameObject.SetActive(true);
        nukeAnimation.gameObject.SetActive(true);


        StartCoroutine(WaitForCutscene());
    }

    IEnumerator WaitForCutscene() {
        yield return new WaitForSeconds(10f);
        gameOverUI.gameObject.SetActive(true);

        // Stops the time
        Time.timeScale = 0;
    }

    IEnumerator WaitForSound() {
        yield return new WaitForSeconds(3.5f);
        audioManager.PlaySFX("Nuke");
    }
}
