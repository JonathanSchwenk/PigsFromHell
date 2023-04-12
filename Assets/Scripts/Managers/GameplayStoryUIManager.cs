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


    private WeaponData activeWeapon;


    private ISaveManager saveManager;


    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        } else {
            print("No save manager");
        }

        activeWeapon = saveManager.saveData.activeWeapon;
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }

    private void SaveManagerOnSave(int num) {
        bulletsInMagText.GetComponent<TextMeshProUGUI>().text = activeWeapon.bulletsInMag.ToString();
        totalReserveAmmoText.GetComponent<TextMeshProUGUI>().text = activeWeapon.reserveAmmo.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        // Unpauses the game if it was paused before starting. 
        // This can prob be in the game manager or something else
        Time.timeScale = 1;

        bulletsInMagText.GetComponent<TextMeshProUGUI>().text = activeWeapon.bulletsInMag.ToString();
        totalReserveAmmoText.GetComponent<TextMeshProUGUI>().text = activeWeapon.reserveAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        bulletsInMagText.GetComponent<TextMeshProUGUI>().text = activeWeapon.bulletsInMag.ToString();
        totalReserveAmmoText.GetComponent<TextMeshProUGUI>().text = activeWeapon.reserveAmmo.ToString();
    }



    public void BackToMainMenu() {
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
