using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using System;
using TMPro;

public class GetCoinsCanvasManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI numVidsWatchedText;

    private ISaveManager saveManager;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = ServiceLocator.Resolve<ISaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (saveManager.saveData.vidAdDate.ToString() == "1/1/0001 12:00:00 AM" || Extensions.MidnightsBetween(saveManager.saveData.vidAdDate, DateTime.Now) >= 1) {
            saveManager.saveData.numVidsWatchedToday = 0;
        }

        numVidsWatchedText.text = saveManager.saveData.numVidsWatchedToday.ToString();
    }
}
