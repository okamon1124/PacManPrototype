using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommander : MonoBehaviour
{
    public static EnemyCommander instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public enum EnemyState
    {
        CHASE, SCATTER
    }

    public EnemyState enemyState { get; private set; }

    private void Start()
    {
        enemyState = EnemyState.SCATTER;
        StartCoroutine(ChangeEnemyState());
    }
    IEnumerator ChangeEnemyState()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            enemyState = EnemyState.CHASE;
            //print("enemy chasing you!");
            yield return new WaitForSeconds(10f);
            enemyState = EnemyState.SCATTER;
            //print("enemy patrolling...");
        }
    }
}
