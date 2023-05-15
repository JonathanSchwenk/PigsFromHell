using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class BuySkinInMenu : MonoBehaviour
{
    [SerializeField] private GameObject skinPool;
    [SerializeField] private GameObject buySkinsCanvas;
    [SerializeField] private GameObject characterCanvas;
    [SerializeField] private GameObject sellSkinsCanvas;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject buyButton;
    

    private int randomSkin;

    private ISaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cancel() {
        buySkinsCanvas.SetActive(false);
        characterCanvas.SetActive(true);
    }

    // Make a function that opens the buying skin UI for when the chest button is clicked


    // Function for actually picking skin
    public void GetRandomSkin() {
        // Putting cost here but we can take it in as a parameter
        int cost = 10;

        if (saveManager.saveData.coins <= cost) { // Change back to >=
            // Disable buttons so you cant spam it
            buyButton.SetActive(false);
            cancelButton.SetActive(false);

            // Start coroutine
            StartCoroutine(WaitForNextSkin(0.2f, 0));

            // subtract coins
            saveManager.saveData.coins -= cost;
        }
        
    }


    IEnumerator WaitForNextSkin(float waitTime, int counter) {
        // Change gun
        if (counter < 25) {
            randomSkin = Random.Range(0, saveManager.saveData.totalSkins.Length);
            for (int i = 0; i < skinPool.transform.childCount; i++) {
                if (saveManager.saveData.totalSkins[randomSkin] == skinPool.transform.GetChild(i).name) {
                    skinPool.transform.GetChild(i).gameObject.SetActive(true);
                } else {
                    skinPool.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        } else {
            // Stop on that skin and get it

            // Disable buttons so you cant spam it
            buyButton.SetActive(false);
            cancelButton.SetActive(false);


            // First, check, if this skin it stops on is already in the unlocked skins then open new UI and don't do the rest
            if (saveManager.saveData.unlockedSkins.Contains(saveManager.saveData.totalSkins[randomSkin])) {
                // open and close canvases
                buySkinsCanvas.SetActive(false);
                sellSkinsCanvas.SetActive(true);

                // set the curretn and active skin to the new one
                saveManager.saveData.currentSkin = saveManager.saveData.totalSkins[randomSkin];

                // Save
                saveManager.Save();

                // Re-enables buttons and change canvases
                buyButton.SetActive(true);
                cancelButton.SetActive(true);
            } else {
                // Add skin to unlocks
                saveManager.saveData.unlockedSkins.Add(saveManager.saveData.totalSkins[randomSkin]);

                // set the curretn and active skin to the new one
                saveManager.saveData.currentSkin = saveManager.saveData.totalSkins[randomSkin];

                // Save
                saveManager.Save();

                // Re-enables buttons and change canvases
                buyButton.SetActive(true);
                cancelButton.SetActive(true);

                buySkinsCanvas.SetActive(false);
                characterCanvas.SetActive(true);
            }
            
        }
        // wait
        yield return new WaitForSeconds(waitTime);

        if (counter < 25) {
            StartCoroutine(WaitForNextSkin(waitTime, counter+=1)); 
        }
    }



    public void SellBackSkin() {
        // give user 2 coins back
        saveManager.saveData.coins += 2;

        // change canvases
        characterCanvas.SetActive(true);
        sellSkinsCanvas.SetActive(false);
    }
}
