using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class SettingsCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject musicOnButton;
    [SerializeField] private GameObject musicOffButton;
    [SerializeField] private GameObject sfxOnButton;
    [SerializeField] private GameObject sfxOffButton;

    private ISaveManager saveManager;
    private IAudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
        audioManager = ServiceLocator.Resolve<IAudioManager>();

        if (saveManager.saveData.musicOn == true) {
            musicOnButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            musicOffButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        } else {
            musicOnButton.GetComponent<RectTransform>().localScale = new Vector3(0.625f, 0.625f, 0.625f);
            musicOffButton.GetComponent<RectTransform>().localScale = new Vector3(1.6f, 1.6f, 1.6f);
        }

        if (saveManager.saveData.SFXOn == true) {
            sfxOnButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            sfxOffButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        } else {
            sfxOnButton.GetComponent<RectTransform>().localScale = new Vector3(0.625f, 0.625f, 0.625f);
            sfxOffButton.GetComponent<RectTransform>().localScale = new Vector3(1.6f, 1.6f, 1.6f);
        }
    }

    public void MusicOnButton() {
        saveManager.saveData.musicOn = true;

        musicOnButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        musicOffButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

        audioManager.PlayMusic("MenuBackgroundMusic");

        saveManager.Save();
    }
    public void MusicOffButton() {
        saveManager.saveData.musicOn = false;

        musicOnButton.GetComponent<RectTransform>().localScale = new Vector3(0.625f, 0.625f, 0.625f);
        musicOffButton.GetComponent<RectTransform>().localScale = new Vector3(1.6f, 1.6f, 1.6f);

        audioManager.StopMusic("MenuBackgroundMusic");

        saveManager.Save();
    }

    public void SFXOnButton() {
        saveManager.saveData.SFXOn = true;

        sfxOnButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        sfxOffButton.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

        saveManager.Save();
    }
    public void SFXOffButton() {
        saveManager.saveData.SFXOn = false;

        sfxOnButton.GetComponent<RectTransform>().localScale = new Vector3(0.625f, 0.625f, 0.625f);
        sfxOffButton.GetComponent<RectTransform>().localScale = new Vector3(1.6f, 1.6f, 1.6f);

        saveManager.Save();
    }

    public void OpenSchwenkStudios() {
        Application.OpenURL("https://schwenkstudios.com");
    }

    public void OpenSchwenkStudiosPrivacy() {
        Application.OpenURL("https://schwenkstudios.com/privacy-policy/privacy-policy.html");
    }
}
