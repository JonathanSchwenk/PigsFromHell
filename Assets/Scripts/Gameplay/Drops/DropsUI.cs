using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using System;
using UnityEngine.UI;

public class DropsUI : MonoBehaviour
{

    // Redo game manager to make a list of strings for the drops, when the drop is collected it adds to the list and at the end of the round it empties the list

    // For action relating to the updating drops, it takes in the drop in question and its up to the situation to do it for each script

    // Now loops through this list and activate the number of drops and change the image accordingly
    // This loop gets called when something in the drops list changes (called my an action)

    [SerializeField] private GameObject dropsParentGameObject;
    [SerializeField] private GameObject drop1;
    [SerializeField] private GameObject drop2;
    [SerializeField] private GameObject drop3;
    [SerializeField] private GameObject drop4;
    [SerializeField] private GameObject dropPool;


    private IGameManager gameManager;


    void Awake() {
        gameManager = ServiceLocator.Resolve<IGameManager>();
        //saveManager = ServiceLocator.Resolve<ISaveManager>();
        

        // Subscribes to gamemanagers actions
        if (gameManager != null) {
            gameManager.OnDropChanged += GameManagerOnDropChanged;

        }
    }


    private void GameManagerOnDropChanged(string drop) { 
        if (gameManager.dropsList.Count > 4) {
            print("Too mane drops in list, something is wrong");
        } else {
            for (int i = 0; i < 4; i++) { // 4 because thats the max length of the dropList
                // Set all to false so that the correct ones can be set to true
                dropsParentGameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            for (int i = 0; i < gameManager.dropsList.Count; i++) {

                dropsParentGameObject.transform.GetChild(i).gameObject.SetActive(true);

                if (gameManager.dropsList[i] == "Health") {
                    dropsParentGameObject.transform.GetChild(i).GetComponent<Image>().sprite = dropPool.transform.GetChild(0).GetComponent<Image>().sprite;
                } else if (gameManager.dropsList[i] == "Speed") {
                    dropsParentGameObject.transform.GetChild(i).GetComponent<Image>().sprite = dropPool.transform.GetChild(1).GetComponent<Image>().sprite;
                } else if (gameManager.dropsList[i] == "Impact") {
                    dropsParentGameObject.transform.GetChild(i).GetComponent<Image>().sprite = dropPool.transform.GetChild(2).GetComponent<Image>().sprite;
                } else if (gameManager.dropsList[i] == "InstaKill") {
                    dropsParentGameObject.transform.GetChild(i).GetComponent<Image>().sprite = dropPool.transform.GetChild(3).GetComponent<Image>().sprite;
                }
            }
        }
    }

    
}
