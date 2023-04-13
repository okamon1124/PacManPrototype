using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Character
{
    MoveDirection current_direction = MoveDirection.Right;

    EnemyState enemyState = EnemyState.SCATTER;

    private Vector3 TargetPosition;
    [SerializeField] Vector3 TargetScatterPostion;
    private Vector3 TargetPlayerPosition;

    private bool run_once = false;

    enum EnemyState
    {
        CHASE,
        SCATTER,
        FRIGHTENDED,
        EATEN
    }

    // Start is called before the first frame update
    void Start()
    {
        TargetPlayerPosition = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    

    private void Update()
    {
        if (run_once == false)
        {
            CheckIntersection(0.01f);
        }
        MoveAndTurn(2f, current_direction, 0.1f, 0.5f);
    }

    private void CheckIntersection(float CheckSphereSize)
    {
        var collideList = Physics.OverlapSphere(transform.position, CheckSphereSize);
        if (CollideChecker(collideList, "intersection_cube"))
        {
            print("call FindNextDirection() function");
            FindNextDirection();
        }
    }

    private void FindNextDirection()
    {
        var possibleNextDirections = new HashSet<MoveDirection>() { MoveDirection.Right, MoveDirection.Left, MoveDirection.Backward, MoveDirection.Forward };
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

        foreach(var direction in possibleNextDirections)
        {
            print(direction.ToString());
        }
        //print(possibleNextDirections);
        run_once= true;
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


}
