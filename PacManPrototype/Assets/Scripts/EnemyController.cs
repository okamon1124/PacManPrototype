using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private GameObject TargetPlayer;
    private NavMeshAgent EnemyAgent;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAgent= GetComponent<NavMeshAgent>();
        TargetPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAgent.SetDestination(TargetPlayer.transform.position);
    }

}
