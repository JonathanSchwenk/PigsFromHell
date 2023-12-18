using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pointsValue;
    [SerializeField] private GameObject pointsValueLost;
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
    void Update() // Could do this in the game over state so it only happens once but whatever
    {
        if (gameManager.playerWon == false && saveManager.saveData.gameMode == "Story") {
            // Setting points and round values here (Might have to move if its doesn't update and display properly)
            if (pointsValueLost) {
                pointsValueLost.GetComponent<TextMeshProUGUI>().text = gameManager.points.ToString();
            }
        } else {
            // Setting points and round values here (Might have to move if its doesn't update and display properly)
            pointsValue.GetComponent<TextMeshProUGUI>().text = gameManager.points.ToString();
            if (roundsValue) {
                roundsValue.GetComponent<TextMeshProUGUI>().text = gameManager.RoundNum.ToString();
            }
        }
    }


    public void BackToMenu() {
        // Pretty sure I dont need this conditional because the story game over calls a different function below
        if (saveManager.saveData.gameMode == "Story") {
            // Updates how many coins the user has
            saveManager.saveData.coins += 25;
        } else {
            // Updates how many coins the user has
            saveManager.saveData.coins += gameManager.RoundNum;
        }

        saveManager.Save();

        Time.timeScale = 1; // So animation in main menu runs still

        // Not loading ad and just returning because ads arent working yet.
        // Need to play ad now before changing scenes
        adManager.LoadRewardedAd(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenuStoryWon() {
        saveManager.saveData.coins += 25;

        saveManager.saveData.levelsCompleted = saveManager.saveData.storyLevelSelected;

        saveManager.Save();

        Time.timeScale = 1; // So animation in main menu runs still

        // Need to play ad now before changing scenes

        // Not loading ad and just returning because ads arent working yet.
        adManager.LoadRewardedAd(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToMenuStoryLost() {

        saveManager.Save();

        Time.timeScale = 1; // So animation in main menu runs still

        SceneManager.LoadScene("MainMenu");
    }

    public void Replay() {
        // Updates how many coins the user has
        saveManager.saveData.coins += gameManager.RoundNum;

        saveManager.Save();

        Time.timeScale = 1; // So the time is reset for next round

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
