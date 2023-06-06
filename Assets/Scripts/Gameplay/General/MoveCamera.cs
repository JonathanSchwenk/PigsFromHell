using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dorkbots.ServiceLocatorTools;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float Speed;
    [SerializeField] private float movementThresholdX;
    [SerializeField] private float movementThresholdZ;


    private Vector3 moveTemp;
    private bool moveCamera;
    private float xDiff;
    private float zDiff;


    private IGameManager gameManager;


/*
    private void Awake() {
        // Inits gamemanager
        gameManager = ServiceLocator.Resolve<IGameManager>();

        // null checker
        if (gameManager != null) {
            // Subscribes the action to this script
            gameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
        }
    }


    private void OnDestroy() {
        // null checker
        if (gameManager != null) {
            // Unsubscribes the action to this script
            gameManager.OnGameStateChanged -= GameManagerOnGameStateChanged;
        }
    }


    private void GameManagerOnGameStateChanged(GameState state) {
        if (state == GameState.Playing) {
            moveCamera = true;
        } else {
            moveCamera = false;
        }
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float dampening;
    [SerializeField] private Vector3 offset;

    /*
    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x > transform.position.x) {
            xDiff = player.transform.position.x - transform.position.x;
        } else {
            xDiff = transform.position.x - player.transform.position.x;
        }
        if (player.transform.position.z > transform.position.z) {
            zDiff = player.transform.position.z - transform.position.z;
        } else {
            zDiff = transform.position.z - player.transform.position.z;
        }

        if (xDiff >= movementThresholdX) {
            moveTemp.x = player.transform.position.x;
            moveTemp.y = 15;
            //transform.position = Vector3.MoveTowards(transform.position, moveTemp, Speed * Time.deltaTime);
        }
        if (zDiff >= movementThresholdZ) {
            moveTemp.z = player.transform.position.z;
            moveTemp.y = 15;
            //transform.position = Vector3.MoveTowards(transform.position, moveTemp, Speed * Time.deltaTime);
        }
    }
    */

    private void FixedUpdate() {
        Vector3 movePosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, dampening);
    }
}

