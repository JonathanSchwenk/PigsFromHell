using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryGameOverUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pointsValue;


    private ISaveManager saveManager;
    private IGameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting points and round values here (Might have to move if its doesn't update and display properly)
        pointsValue.GetComponent<TextMeshProUGUI>().text = gameManager.points.ToString();
    }


    public void BackToMenu() {
        // Updates how many coins the user has
        saveManager.saveData.coins += 25;

        saveManager.Save();

        Time.timeScale = 1; // So animation in main menu runs still

        // Need to play ad now before changing scenes

        SceneManager.LoadScene("MainMenu");
    }

    public void Replay() {
        // Updates how many coins the user has
        saveManager.saveData.coins += 25;

        saveManager.Save();

        Time.timeScale = 1; // So the time is reset for next round

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
