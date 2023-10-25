using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;
using TMPro;
using UnityEngine.UI;

public class BuyGunsObject : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject stationBackground;
    [SerializeField] private GameObject stationText;

    private float minDist = 5;
    private float dist;
    private List<Collider> colliders = new List<Collider>();

    private IGameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }

    void Update()
    {
        // if the player is within a certain distance from the spot then pop up a UI button that allows you to buy a gun 
        dist = Vector3.Distance(target.transform.position, gameObject.transform.position);
        if(dist < minDist)
        {
            gameManager.activeBuyObject = gameObject;
            buyButton.SetActive(true);
        } else {
            if (gameManager.activeBuyObject == gameObject) {
                buyButton.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy") {
            stationBackground.GetComponent<Image>().color = new Color(
                stationBackground.GetComponent<Image>().color.r, 
                stationBackground.GetComponent<Image>().color.g, 
                stationBackground.GetComponent<Image>().color.b, 
                0.2f
            );
            stationText.GetComponent<TextMeshProUGUI>().color = new Color(
                stationText.GetComponent<TextMeshProUGUI>().color.r, 
                stationText.GetComponent<TextMeshProUGUI>().color.g, 
                stationText.GetComponent<TextMeshProUGUI>().color.b, 
                0.2f
            );

            if (!colliders.Contains(other)) { 
                colliders.Add(other); 
            }
        }
    }

    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);

        if (colliders.Count <= 0) {
            stationBackground.GetComponent<Image>().color = new Color(
                stationBackground.GetComponent<Image>().color.r, 
                stationBackground.GetComponent<Image>().color.g, 
                stationBackground.GetComponent<Image>().color.b, 
                1
            );
            stationText.GetComponent<TextMeshProUGUI>().color = new Color(
                stationText.GetComponent<TextMeshProUGUI>().color.r, 
                stationText.GetComponent<TextMeshProUGUI>().color.g, 
                stationText.GetComponent<TextMeshProUGUI>().color.b, 
                1
            );
        }
    }
}
