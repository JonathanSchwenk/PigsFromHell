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
            /*
                Gets the direction by finding the displacement of the target and the player.
                Then it gets the angle in radians and changes to degrees using a custom function in Extentions.

                Note: I set the y to 0 because I am in 3D so I want to read on the x, z axis and not x, y
            */

            Vector3 toPos = gameManager.playerTasksGlobal[0].transform.position;
            Vector3 fromPos = player.transform.position;
            toPos.y = 0f;
            fromPos.y = 0f;
            Vector3 dir = (toPos - fromPos).normalized;
            float angle = Extensions.GetAngleFromVectorFloat(dir);
            angle = angle + 90;
            gameObject.transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
