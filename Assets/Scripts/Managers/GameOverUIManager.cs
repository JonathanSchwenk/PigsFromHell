using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pointsValue;
    [SerializeField] private GameObject roundsValue;


    private ISaveManager saveManager;
    private IGameManager gameManager;
    private IAdManager adManager;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
        adManager = ServiceLocator.Resolve<IAdManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting points and round values here (Might have to move if its doesn't update and display properly)
        pointsValue.GetComponent<TextMeshProUGUI>().text = gameManager.points.ToString();
        if (roundsValue) {
            roundsValue.GetComponent<TextMeshProUGUI>().text = gameManager.RoundNum.ToString();
        }
    }


    public void BackToMenu() {
        // Updates how many coins the user has
        saveManager.saveData.coins += gameManager.RoundNum;

        saveManager.Save();

        Time.timeScale = 1; // So animation in main menu runs still

        // Need to play ad now before changing scenes
        adManager.LoadRewardedAd(false);
    }

    public void Replay() {
        // Updates how many coins the user has
        saveManager.saveData.coins += gameManager.RoundNum;

        saveManager.Save();

        Time.timeScale = 1; // So the time is reset for next round

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
