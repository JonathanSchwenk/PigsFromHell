using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HideInstructions : MonoBehaviour
{

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject stationText;

    private List<Collider> colliders = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemy") {
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
            stationText.GetComponent<TextMeshProUGUI>().color = new Color(
                stationText.GetComponent<TextMeshProUGUI>().color.r, 
                stationText.GetComponent<TextMeshProUGUI>().color.g, 
                stationText.GetComponent<TextMeshProUGUI>().color.b, 
                1
            );
        }
    }
}
