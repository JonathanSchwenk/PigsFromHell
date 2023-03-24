using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetAI : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    private GameObject target;

    private void Start() {
        target = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.transform.position);
    }
}
