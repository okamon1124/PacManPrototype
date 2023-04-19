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

    //public bool Frighteneded { get; private set; } = false;

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
        yield return new WaitForSeconds(7f);
        enemyState = EnemyState.CHASE;
        yield return new WaitForSeconds(20f);
        enemyState = EnemyState.SCATTER;
        yield return new WaitForSeconds(7f);
        enemyState = EnemyState.CHASE;
        yield return new WaitForSeconds(20f);
        enemyState = EnemyState.SCATTER;
        yield return new WaitForSeconds(5f);
        enemyState = EnemyState.CHASE;
        yield return new WaitForSeconds(20f);
        enemyState = EnemyState.SCATTER;
        yield return new WaitForSeconds(5f);
        enemyState = EnemyState.CHASE;
    }
}
