using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Dorkbots.ServiceLocatorTools;

public class MoveToTargetAI : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;

    private float speed;

    private GameObject target;

    private IGameManager gameManager;

    void Awake() {
        gameManager = ServiceLocator.Resolve<IGameManager>();
    }

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
        agent.speed = gameManager.enemySpeed;
    }


    // Update is called once per frame
    void Update()
    {
        if (target) {
            agent.SetDestination(target.transform.position);
        }
    }
}
