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
        roundsValue.GetComponent<TextMeshProUGUI>().text = gameManager.RoundNum.ToString();
    }


    public void BackToMenu() {
        saveManager.saveData.coins += gameManager.RoundNum;

        saveManager.Save();

        Time.timeScale = 1; // So animation in main menu runs still

        SceneManager.LoadScene("MainMenu");
    }
}
