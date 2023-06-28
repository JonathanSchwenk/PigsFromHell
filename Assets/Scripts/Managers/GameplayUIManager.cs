using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dorkbots.ServiceLocatorTools;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{

    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject playerControlsUI;
    [SerializeField] private GameObject gameInfoUI;

    [SerializeField] private GameObject bulletsInMagText;
    [SerializeField] private GameObject totalReserveAmmoText;

    [SerializeField] private GameObject roundTextPlaying;
    [SerializeField] private GameObject pointsTextPlaying;
    [SerializeField] private GameObject roundTextPaused;
    [SerializeField] private GameObject pointsTextPaused;
    [SerializeField] private GameObject starValue;



    private ISaveManager saveManager;
    private IGameManager gameManager;


    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        gameManager = ServiceLocator.Resolve<IGameManager>();

        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        } else {
            print("No save manager");
        }

        gameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        } else {
            print("No save manager");
        }

        gameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
    }

    private void SaveManagerOnSave(int num) {
        // Not sure if I need this since its in the update 
        bulletsInMagText.GetComponent<TextMeshProUGUI>().text = gameManager.activeWeapon.bulletsInMag.ToString();
        totalReserveAmmoText.GetComponent<TextMeshProUGUI>().text = gameManager.activeWeapon.reserveAmmo.ToString();

        if (gameManager.activeWeapon.starValue == 1) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(false);
            starValue.transform.GetChild(2).gameObject.SetActive(false);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else if (gameManager.activeWeapon.starValue == 2) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(false);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else if (gameManager.activeWeapon.starValue == 3) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(true);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(true);
            starValue.transform.GetChild(3).gameObject.SetActive(true);
        }

    }

    private void GameManagerOnGameStateChanged(GameState state) { 
        if (state == GameState.GameOver) {
            // This will be changed to a game over UI and not pause UI
            //Time.timeScale = 0;
            //pauseUI.SetActive(true);
            //playerControlsUI.SetActive(false);
            //gameInfoUI.SetActive(false);
        } 
    }


    // Start is called before the first frame update
    void Start()
    {
        // Unpauses the game if it was paused before starting. 
        // This can prob be in the game manager or something else
        Time.timeScale = 1;

        bulletsInMagText.GetComponent<TextMeshProUGUI>().text = gameManager.activeWeapon.bulletsInMag.ToString();
        totalReserveAmmoText.GetComponent<TextMeshProUGUI>().text = gameManager.activeWeapon.reserveAmmo.ToString();

        if (gameManager.activeWeapon.starValue == 1) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(false);
            starValue.transform.GetChild(2).gameObject.SetActive(false);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else if (gameManager.activeWeapon.starValue == 2) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(false);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else if (gameManager.activeWeapon.starValue == 3) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(true);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(true);
            starValue.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        bulletsInMagText.GetComponent<TextMeshProUGUI>().text = gameManager.activeWeapon.bulletsInMag.ToString();
        totalReserveAmmoText.GetComponent<TextMeshProUGUI>().text = gameManager.activeWeapon.reserveAmmo.ToString();

        roundTextPlaying.GetComponent<TextMeshProUGUI>().text = gameManager.RoundNum.ToString();
        pointsTextPlaying.GetComponent<TextMeshProUGUI>().text = gameManager.points.ToString();
        roundTextPaused.GetComponent<TextMeshProUGUI>().text = gameManager.RoundNum.ToString();
        pointsTextPaused.GetComponent<TextMeshProUGUI>().text = gameManager.points.ToString();

        if (gameManager.activeWeapon.starValue == 1) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(false);
            starValue.transform.GetChild(2).gameObject.SetActive(false);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else if (gameManager.activeWeapon.starValue == 2) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(false);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else if (gameManager.activeWeapon.starValue == 3) {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(true);
            starValue.transform.GetChild(3).gameObject.SetActive(false);
        } else {
            starValue.transform.GetChild(0).gameObject.SetActive(true);
            starValue.transform.GetChild(1).gameObject.SetActive(true);
            starValue.transform.GetChild(2).gameObject.SetActive(true);
            starValue.transform.GetChild(3).gameObject.SetActive(true);
        }
    }



    public void BackToMainMenu() {
        Time.timeScale = 1; // So animation in main menu runs still
        SceneManager.LoadScene("MainMenu");
    }
    public void Pause() {
        if (Time.timeScale != 0) {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            playerControlsUI.SetActive(false);
            gameInfoUI.SetActive(false);
        } else {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            playerControlsUI.SetActive(true);
            gameInfoUI.SetActive(true);
        }
    }
}
