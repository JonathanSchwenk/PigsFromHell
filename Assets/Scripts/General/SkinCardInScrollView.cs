using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;

public class SkinCardInScrollView : MonoBehaviour
{
    [SerializeField] private GameObject skinCardPrefab;
    [SerializeField] private GameObject scrollViewContents;
    
    private RectTransform scrollViewRectTransfrom;
    private bool cardsAlreadySpawned;


    private ISaveManager saveManager;


    private void Awake() {
        saveManager = ServiceLocator.Resolve<ISaveManager>();

        if (saveManager != null) {
            saveManager.OnSave += SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }
    private void OnDestroy() {
        if (saveManager != null) {
            saveManager.OnSave -= SaveManagerOnSave;
        } else {
            print("No save manager");
        }
    }

    private void SaveManagerOnSave(int num) {
        if (cardsAlreadySpawned == true) {
            for (int i = 0; i < scrollViewContents.transform.childCount; i++) {
                UpdateSkinCards(scrollViewContents.transform.GetChild(i).gameObject, i);
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        // Refrence and set my rect transfrom from the scrollview contents
        scrollViewRectTransfrom = scrollViewContents.GetComponent<RectTransform>();

        // Change size
        scrollViewRectTransfrom.sizeDelta = new Vector2(2400.0f, 300.0f); // x = 337.5 for setting position 
        
        // Might want to load data here

        // Spawns cards
        SpawnSkinCards();
        // Changes variable to that the cards have spawned
        cardsAlreadySpawned = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Moved the spawning of weapon cards to a separate function because I want to use it more than once
    private void SpawnSkinCards() {
        // Instantiate the weaponcard prefabs
        for (int i = 0; i < saveManager.saveData.totalSkins.Length; i++) {
            // Instantiate
            GameObject go;
            go = Instantiate(skinCardPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
            go.transform.SetParent(scrollViewContents.transform);
            
            UpdateSkinCards(go, i);
        }

    } // end of function


    // Updates cards 
    private void UpdateSkinCards(GameObject go, int i) {
        // Sets weapon name and image
        // name
        go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = saveManager.saveData.totalSkins[i];
        // image
        go.transform.GetChild(2).GetComponent<Image>().sprite = go.transform.GetChild(4).Find(saveManager.saveData.totalSkins[i]).GetComponent<Image>().sprite;
        

        // Sets isSelected 
        if (saveManager.saveData.currentSkin == saveManager.saveData.totalSkins[i]) {
            go.transform.GetChild(0).gameObject.SetActive(true);
        } else {
            go.transform.GetChild(0).gameObject.SetActive(false);
        }


        // sets the background to show if its unlocked or not 
        if (!saveManager.saveData.unlockedSkins.Contains(saveManager.saveData.totalSkins[i])) {
            go.transform.GetChild(3).GetComponent<Image>().color = Color.black;
        } else {
            go.transform.GetChild(3).GetComponent<Image>().color = Color.white;
        }
        
    }
}
