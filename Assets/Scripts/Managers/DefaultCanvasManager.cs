using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;

public class DefaultCanvasManager : MonoBehaviour
{

    [SerializeField] private GameObject coinsValueTextGO;

    [SerializeField] private GameObject storyButton;
    [SerializeField] private GameObject survivalButton;
    [SerializeField] private GameObject characterButton;
    [SerializeField] private GameObject settingsButton;

    [SerializeField] private GameObject storyCanvas;
    [SerializeField] private GameObject survivalCanvas;
    [SerializeField] private GameObject characterCanvas;
    [SerializeField] private GameObject settingsCanvas;

    [SerializeField] private GameObject skyButtonImage;
    [SerializeField] private GameObject blueButtonImage;

    [SerializeField] private GameObject buyingGunsCanvas;
    [SerializeField] private GameObject buyingSkinsBeforeCanvas;
    [SerializeField] private GameObject buyingSkinsAfterCanvas;
    [SerializeField] private GameObject getCoinsCanvas;



    private ISaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Put in update for now until I add this to the subscribed actions
        coinsValueTextGO.GetComponent<TextMeshProUGUI>().text = saveManager.saveData.coins.ToString();
    }



    public void GoToSampleScene() {
        SceneManager.LoadScene("TestCityScene");
    }


    public void ChangeTab(string tabName) {
        if (tabName == "Story") {
            // Change canvas
            storyCanvas.SetActive(true);
            survivalCanvas.SetActive(false);
            characterCanvas.SetActive(false);
            settingsCanvas.SetActive(false);

            buyingGunsCanvas.SetActive(false);
            buyingSkinsBeforeCanvas.SetActive(false);
            buyingSkinsAfterCanvas.SetActive(false);
            getCoinsCanvas.SetActive(false);

            // change button color
            storyButton.GetComponent<Image>().sprite = skyButtonImage.GetComponent<Image>().sprite;
            survivalButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
            characterButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
            settingsButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
        } else if (tabName == "Survival") {
            // Change canvas
            storyCanvas.SetActive(false);
            survivalCanvas.SetActive(true);
            characterCanvas.SetActive(false);
            settingsCanvas.SetActive(false);

            buyingGunsCanvas.SetActive(false);
            buyingSkinsBeforeCanvas.SetActive(false);
            buyingSkinsAfterCanvas.SetActive(false);
            getCoinsCanvas.SetActive(false);

            // change button color
            storyButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
            survivalButton.GetComponent<Image>().sprite = skyButtonImage.GetComponent<Image>().sprite;
            characterButton.GetComponent<Image>().sprite= blueButtonImage.GetComponent<Image>().sprite;
            settingsButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
        } else if (tabName == "Character") {
            // Change canvas
            storyCanvas.SetActive(false);
            survivalCanvas.SetActive(false);
            characterCanvas.SetActive(true);
            settingsCanvas.SetActive(false);

            buyingGunsCanvas.SetActive(false);
            buyingSkinsBeforeCanvas.SetActive(false);
            buyingSkinsAfterCanvas.SetActive(false);
            getCoinsCanvas.SetActive(false);

            // change button color
            storyButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
            survivalButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
            characterButton.GetComponent<Image>().sprite= skyButtonImage.GetComponent<Image>().sprite;
            settingsButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
        } else if (tabName == "Settings") {
            // Change canvas
            storyCanvas.SetActive(false);
            survivalCanvas.SetActive(false);
            characterCanvas.SetActive(false);
            settingsCanvas.SetActive(true);

            buyingGunsCanvas.SetActive(false);
            buyingSkinsBeforeCanvas.SetActive(false);
            buyingSkinsAfterCanvas.SetActive(false);
            getCoinsCanvas.SetActive(false);

            // change button color
            storyButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
            survivalButton.GetComponent<Image>().sprite = blueButtonImage.GetComponent<Image>().sprite;
            characterButton.GetComponent<Image>().sprite= blueButtonImage.GetComponent<Image>().sprite;
            settingsButton.GetComponent<Image>().sprite = skyButtonImage.GetComponent<Image>().sprite;
        }
    }


    public void VidForCoinsButton() {
        storyCanvas.SetActive(false);
        survivalCanvas.SetActive(false);
        characterCanvas.SetActive(false);
        settingsCanvas.SetActive(false);

        buyingGunsCanvas.SetActive(false);
        buyingSkinsBeforeCanvas.SetActive(false);
        buyingSkinsAfterCanvas.SetActive(false);

        getCoinsCanvas.SetActive(true);
    }
}
