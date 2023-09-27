using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;

public class WeaponCardInScrollView : MonoBehaviour
{
    [SerializeField] private GameObject weaponCardPrefab;
    [SerializeField] private GameObject scrollViewContents;
    [SerializeField] private bool isNormal;
    [SerializeField] private int primaryOrSecondary;
    
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
            if (isNormal == true) {
                for (int i = 0; i < scrollViewContents.transform.childCount; i++) {
                    UpdateWeaponCards(true, scrollViewContents.transform.GetChild(i).gameObject, i);
                }
            } else {
                for (int i = 0; i < scrollViewContents.transform.childCount; i++) {
                    UpdateWeaponCards(false, scrollViewContents.transform.GetChild(i).gameObject, i);
                }
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        // Refrence and set my rect transfrom from the scrollview contents
        scrollViewRectTransfrom = scrollViewContents.GetComponent<RectTransform>();

        // Change size
        if (isNormal == true) {
            scrollViewRectTransfrom.sizeDelta = new Vector2(1200.0f, 300.0f); // x = 367.5 for setting position
        } else {
            scrollViewRectTransfrom.sizeDelta = new Vector2(1720.0f, 300.0f); // x = 600 for setting position
        }
        
        // Might want to load data here

        // Spawns cards
        SpawnWeaponCards();
        // Changes variable to that the cards have spawned
        cardsAlreadySpawned = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Moved the spawning of weapon cards to a separate function because I want to use it more than once
    private void SpawnWeaponCards() {
        if (isNormal == true) {
            // Instantiate the weaponcard prefabs
            for (int i = 0; i < saveManager.saveData.totalNormalWeapons.Length - 1; i++) { // -1 for normal guns because I dont want to include the knife
                // Instantiate
                GameObject go;
                go = Instantiate(weaponCardPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                go.transform.SetParent(scrollViewContents.transform);
                
                UpdateWeaponCards(true, go, i);
            }
        } else {
            // Instantiate the weaponcard prefabs
            for (int i = 0; i < saveManager.saveData.totalSpecialWeapons.Length; i++) {
                // Instantiate
                GameObject go;
                go = Instantiate(weaponCardPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
                go.transform.SetParent(scrollViewContents.transform);

                UpdateWeaponCards(false, go, i);
            }
        }
    } // end of function



    /*

        This can stay the same but it will end up being saveManager.saveData.totalNormalWeapons[i].name bc it is a data structure 

    */

    // Updates cards 
    private void UpdateWeaponCards(bool isNormalOrSpecial, GameObject go, int i) {
        // if normal else special
        if (isNormalOrSpecial == true) {
            // Sets weapon name and image
            // name
            go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = saveManager.saveData.totalNormalWeapons[i].name;
            // image
            go.transform.GetChild(1).GetComponent<Image>().sprite = go.transform.GetChild(4).Find(saveManager.saveData.totalNormalWeapons[i].name).GetComponent<Image>().sprite;
            

            // Sets isSelected 
            if (primaryOrSecondary == 0) {
                if (saveManager.saveData.currentWeapons[0].name == saveManager.saveData.totalNormalWeapons[i].name) {
                    go.transform.GetChild(0).gameObject.SetActive(true);
                } else {
                    go.transform.GetChild(0).gameObject.SetActive(false);
                }
            } else {
                if (saveManager.saveData.currentWeapons[1].name == saveManager.saveData.totalNormalWeapons[i].name) {
                    go.transform.GetChild(0).gameObject.SetActive(true);
                } else {
                    go.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            // sets the background to show if its unlocked or not 
            for (int j = 0; j < saveManager.saveData.unlockedWeapons.Count; j++) {
                if (saveManager.saveData.unlockedWeapons[j].name == saveManager.saveData.totalNormalWeapons[i].name) {
                    go.transform.GetChild(2).GetComponent<Image>().color = Color.white;
                    break;
                } else {
                    go.transform.GetChild(2).GetComponent<Image>().color = Color.black;
                }
            }
            

            // Change primaryOrSecondary in each weapon card
            go.GetComponent<WeaponCardButton>().primaryOrSecondary = primaryOrSecondary;
        } else {
            // Sets weapon name and image
            // name
            go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = saveManager.saveData.totalSpecialWeapons[i].name;
            print(go.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);
            // image
            go.transform.GetChild(1).GetComponent<Image>().sprite = go.transform.GetChild(4).Find(saveManager.saveData.totalSpecialWeapons[i].name).GetComponent<Image>().sprite;
            

            // Sets isSelected 
            if (primaryOrSecondary == 0) {
                if (saveManager.saveData.currentWeapons[0].name == saveManager.saveData.totalSpecialWeapons[i].name) {
                    go.transform.GetChild(0).gameObject.SetActive(true);
                } else {
                    go.transform.GetChild(0).gameObject.SetActive(false);
                }
            } else {
                if (saveManager.saveData.currentWeapons[1].name == saveManager.saveData.totalSpecialWeapons[i].name) {
                    go.transform.GetChild(0).gameObject.SetActive(true);
                } else {
                    go.transform.GetChild(0).gameObject.SetActive(false);
                }
            }

            // sets the background to show if its unlocked or not 
            for (int j = 0; j < saveManager.saveData.unlockedWeapons.Count; j++) {
                if (saveManager.saveData.unlockedWeapons[j].name == saveManager.saveData.totalSpecialWeapons[i].name) {
                    go.transform.GetChild(2).GetComponent<Image>().color = Color.white;
                    break;
                } else {
                    go.transform.GetChild(2).GetComponent<Image>().color = Color.black;
                }
            }

            // Change primaryOrSecondary in each weapon card
            go.GetComponent<WeaponCardButton>().primaryOrSecondary = primaryOrSecondary;
        }
    }

}
