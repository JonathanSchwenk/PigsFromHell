using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using UnityEngine.SceneManagement;
using TMPro;

// mapSelected variable thats a string that gets set when a map button gets pressed. this variable tells which map should have the selected 
// background vs the non-selected. This variable also will let the play button know which scene to load up.

// SerializedField for a list of game objects that will be filled with the buttons so i can loop through this list and 
// set the selected or not. 


public class SurvivalCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject[] mapList;
    [SerializeField] private TextMeshProUGUI currentMapRecord;

    private ISaveManager saveManager;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        // Sets the record to show for the selected map
        for (int i = 0; i < saveManager.saveData.survivalLevelRecordsKeys.Length; i++) {
            if (saveManager.saveData.survivalMapSelected == saveManager.saveData.survivalLevelRecordsKeys[i]) {
                currentMapRecord.text = saveManager.saveData.survivalLevelRecordsValues[i].ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PressedNeighborhood() {
        saveManager.saveData.survivalMapSelected = "Neighborhood";
        SetSelectedMap();
    }
    public void PressedHighway() {
        saveManager.saveData.survivalMapSelected = "Highway";
        SetSelectedMap();
    }
    public void PressedDowntown() {
        saveManager.saveData.survivalMapSelected = "Downtown";
        SetSelectedMap();
    }
    public void PressedBeach() {
        saveManager.saveData.survivalMapSelected = "Beach";
        SetSelectedMap();
    }
    public void PressedShoppingCenter() {
        saveManager.saveData.survivalMapSelected = "ShoppingCenter";
        SetSelectedMap();
    }

    public void PlayButton() {
        if (saveManager.saveData.survivalMapSelected == "Neighborhood") {
            SceneManager.LoadScene("Survival_Neighborhood");
        }
        if (saveManager.saveData.survivalMapSelected == "Highway") {
            SceneManager.LoadScene("Survival_Highway");
        }
        if (saveManager.saveData.survivalMapSelected == "Downtown") {
            SceneManager.LoadScene("Survival_Downtown");
        }
        if (saveManager.saveData.survivalMapSelected == "Beach") {
            SceneManager.LoadScene("Survival_Beach");
        }
        if (saveManager.saveData.survivalMapSelected == "ShoppingCenter") {
            SceneManager.LoadScene("Survival_ShoppingCenter");
        }
    }


    private void SetSelectedMap() {
        for (int i = 0; i < mapList.Length; i++) {
            if (saveManager.saveData.survivalMapSelected == mapList[i].name) {
                mapList[i].transform.GetChild(1).gameObject.SetActive(true);
                mapList[i].transform.GetChild(2).gameObject.SetActive(false);
            } else {
                mapList[i].transform.GetChild(1).gameObject.SetActive(false);
                mapList[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        // Sets the record to show for the selected map
        for (int i = 0; i < saveManager.saveData.survivalLevelRecordsKeys.Length; i++) {
            if (saveManager.saveData.survivalMapSelected == saveManager.saveData.survivalLevelRecordsKeys[i]) {
                currentMapRecord.text = saveManager.saveData.survivalLevelRecordsValues[i].ToString();
            }
        }
    }
}
