using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingSkinChestButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenBuyGunsCanvas() {
        // Getting canvases
        GameObject buySkinsCanvas = GameObject.Find("Canvases").gameObject.transform.Find("BuySkinsCanvas").gameObject.transform.GetChild(0).gameObject; 
        GameObject characterCanvas = GameObject.Find("CharacterCanvas").gameObject;

        // Setting buy canvas to active
        buySkinsCanvas.SetActive(true);
        characterCanvas.SetActive(false);
    }
}
