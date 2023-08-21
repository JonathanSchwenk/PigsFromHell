using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;


public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private IGameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.playerTasksGlobal.Count > 0) {
            // gameObject.transform.rotation = Quaternion.LookRotation((gameManager.playerTasksGlobal[0].transform.position - player.transform.position).normalized);

            Vector3 direction = (gameManager.playerTasksGlobal[0].transform.position - player.transform.position).normalized;
            // print("Direction " + direction);
            float angle = Vector3.Angle(player.transform.position, direction);

            if (direction.z < 0) {
                angle = angle * -1;
            }
            // print("Angle " + angle);
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, (angle + 90));
            
            gameObject.transform.rotation = targetRotation;
        }
    }
}
