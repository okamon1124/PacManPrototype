using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : Character
{
    [SerializeField] EnemyName enemyName = EnemyName.BLINKY;
    [SerializeField] Vector3 TargetScatterPostion;
    private GameObject TargetPlayer;
    private GameObject Blinky;
    private Vector3 TargetPlayerPosition;
    private Vector3 TargetPosition = Vector3.zero;
    private Vector3 LastIntersectionPosition = Vector3.zero;
    private bool ClydeIsClose = false;

    MoveDirection current_direction = MoveDirection.Right;
    EnemyState enemyState = EnemyState.SCATTER;

    enum EnemyState
    {
        CHASE, SCATTER, FRIGHTENDED, EATEN
    }

    enum EnemyName
    {
        BLINKY, PINKY, INKY, CLYDE
    }

    void Start()
    {
        TargetPlayer = GameObject.FindWithTag("Player");
        if(enemyName == EnemyName.INKY)
        {
            Blinky = GameObject.Find("Blinky");
        }

        enemyState = EnemyState.SCATTER;
        StartCoroutine(ChangeEnemyState());
    }

    private void Update()
    {
        TargetPlayerPosition = TargetPlayer.transform.position;
        SetTargetPlayerPosition();
        CheckEnemyState();
        CheckIntersection(0.01f);
        MoveAndTurn(2f, current_direction, 0.1f, 0.5f);
    }

    private void CheckIntersection(float CheckSphereSize)
    {
        var collideList = Physics.OverlapSphere(transform.position, CheckSphereSize);
        if (CollideChecker(collideList, "intersection_cube"))
        {
            var current_intersection_position = Vector3.zero;
            foreach (var collide in collideList)
            {
                if (collide.tag == "intersection_cube")
                {
                    current_intersection_position = collide.transform.position;
                }
            }

            if(current_intersection_position != LastIntersectionPosition)
            {
                LastIntersectionPosition= current_intersection_position;
                //print($"call FindNextDirection() function, lastIntersection: {LastIntersectionPosition.ToString()}");
                FindNextDirection();
            }
        }
    }

    private void FindNextDirection()
    {
        var possibleNextDirections = new HashSet<MoveDirection>() { MoveDirection.Forward,  MoveDirection.Left, MoveDirection.Backward, MoveDirection.Right};
        var DirectionToRemove = new HashSet<MoveDirection>();
        possibleNextDirections.Remove(FindOppositeDirection(current_direction));

        foreach (var direction in possibleNextDirections)
        {
            if (CheckFaceWall(direction, 0.1f, 1f))
            {
                DirectionToRemove.Add(direction);
            }
        }

        foreach (var direction in DirectionToRemove)
        {
            possibleNextDirections.Remove(direction);
        }
        ChooseShortestDirection(possibleNextDirections);
    }

    private MoveDirection FindOppositeDirection(MoveDirection moveDirection)
    {
        if(moveDirection == MoveDirection.Right)
        {
            return MoveDirection.Left;
        }
        else if(moveDirection == MoveDirection.Left)
        {
            return MoveDirection.Right;
        }
        else if (moveDirection == MoveDirection.Forward)
        {
            return MoveDirection.Backward;
        }
        else
        {
            return MoveDirection.Forward;
        }
    }

    private void ChooseShortestDirection(HashSet<MoveDirection> directions)
    {
        var current_distance = 0f;
        var current_min_distance = 100000f;
        var current_best_direction = MoveDirection.Right;

        foreach (var direction in directions)
        {
            if (direction == MoveDirection.Forward)
            {
                current_distance = Mathf.Abs(Vector3.Distance(LastIntersectionPosition + new Vector3(0f, 0f, 1f), TargetPosition));
                if (current_distance <= current_min_distance)
                {
                    current_min_distance = current_distance;
                    current_best_direction = MoveDirection.Forward;
                }
            }
            else if (direction == MoveDirection.Left)
            {
                current_distance = Mathf.Abs(Vector3.Distance(LastIntersectionPosition + new Vector3(-1f, 0f, 0f), TargetPosition));
                if (current_distance <= current_min_distance)
                {
                    current_min_distance = current_distance;
                    current_best_direction = MoveDirection.Left;
                }
            }
            else if (direction == MoveDirection.Backward)
            {
                current_distance = Mathf.Abs(Vector3.Distance(LastIntersectionPosition + new Vector3(0f, 0f, -1f), TargetPosition));
                if (current_distance <= current_min_distance)
                {
                    current_min_distance = current_distance;
                    current_best_direction = MoveDirection.Backward;
                }
            }
            else if (direction == MoveDirection.Right)
            {
                current_distance = Mathf.Abs(Vector3.Distance(LastIntersectionPosition + new Vector3(1f, 0f, 0f), TargetPosition));
                if (current_distance <= current_min_distance)
                {
                    current_min_distance = current_distance;
                    current_best_direction = MoveDirection.Right;
                }
            }
            current_direction = current_best_direction;
        }
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

    private void CheckEnemyState()
    {
        if(enemyState == EnemyState.SCATTER)
        {
            TargetPosition = TargetScatterPostion;
        }
        else if(enemyState == EnemyState.CHASE)
        {
            if(ClydeIsClose)
            {
                TargetPosition = TargetScatterPostion;
            }
            else
            {
                TargetPosition = TargetPlayerPosition;
            }
        }
    }

    private void SetTargetPlayerPosition()
    {
        var TargetPlayerDirection = TargetPlayer.GetComponent<PlayerController>().PlayerCurrentDirection;
        if(enemyName == EnemyName.PINKY)
        {
            if(TargetPlayerDirection== MoveDirection.Forward)
            {
                TargetPlayerPosition += new Vector3(0f, 0f, 4f);
            }
            else if (TargetPlayerDirection== MoveDirection.Left)
            {
                TargetPlayerPosition += new Vector3(-4f, 0f, 0f);
            }
            else if (TargetPlayerDirection == MoveDirection.Backward)
            {
                TargetPlayerPosition += new Vector3(0f, 0f, -4f);
            }
            else if (TargetPlayerDirection== MoveDirection.Right)
            {
                TargetPlayerPosition += new Vector3(4f, 0f, 0f);
            }
        }
        if(enemyName == EnemyName.INKY)
        {
            var BlinkyPosition = Blinky.transform.position;
            
            if (TargetPlayerDirection == MoveDirection.Forward)
            {
                TargetPlayerPosition += new Vector3(0f, 0f, 4f);
                TargetPlayerPosition += new Vector3(TargetPlayerPosition.x - BlinkyPosition.x, 0f, TargetPlayerPosition.z - BlinkyPosition.z);
            }
            else if (TargetPlayerDirection == MoveDirection.Left)
            {
                TargetPlayerPosition += new Vector3(-4f, 0f, 0f);
                TargetPlayerPosition += new Vector3(TargetPlayerPosition.x - BlinkyPosition.x, 0f, TargetPlayerPosition.z - BlinkyPosition.z);
            }
            else if (TargetPlayerDirection == MoveDirection.Backward)
            {
                TargetPlayerPosition += new Vector3(0f, 0f, -4f);
                TargetPlayerPosition += new Vector3(TargetPlayerPosition.x - BlinkyPosition.x, 0f, TargetPlayerPosition.z - BlinkyPosition.z);
            }
            else if (TargetPlayerDirection == MoveDirection.Right)
            {
                TargetPlayerPosition += new Vector3(4f, 0f, 0f);
                TargetPlayerPosition += new Vector3(TargetPlayerPosition.x - BlinkyPosition.x, 0f, TargetPlayerPosition.z - BlinkyPosition.z);
            }
            //print($"Inky target {TargetPlayerPosition.ToString()}");

        }
        if(enemyName == EnemyName.CLYDE)
        {
            //print(Mathf.Abs(Vector3.Distance(this.transform.position, TargetPlayerPosition)));
            
            if(Mathf.Abs(Vector3.Distance(this.transform.position, TargetPlayerPosition)) < 8f)
            {
                ClydeIsClose= true;
            }
            else
            {
                ClydeIsClose= false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (enemyName == EnemyName.BLINKY)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(TargetPosition, 0.2f);
            Gizmos.DrawLine(this.transform.position, TargetPosition);
        }
        else if (enemyName == EnemyName.PINKY)
        {
            Gizmos.color = new Color(0.8f, 0.5f, 0.8f, 1.0f); ;
            Gizmos.DrawSphere(TargetPosition, 0.2f);
            Gizmos.DrawLine(this.transform.position, TargetPosition);
        }

        else if (enemyName == EnemyName.INKY)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(TargetPosition, 0.2f);
            Gizmos.DrawLine(this.transform.position, TargetPosition);
        }
        else if(enemyName == EnemyName.CLYDE)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 8f);
            Gizmos.DrawSphere(TargetPosition, 0.2f);
            Gizmos.DrawLine(this.transform.position, TargetPosition);
        }
    }
}
