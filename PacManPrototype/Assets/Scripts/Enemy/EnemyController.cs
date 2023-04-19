using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : Character
{
    [SerializeField] EnemyName enemyName = EnemyName.BLINKY;

    [SerializeField] float EnemyMovementSpeed = 2f;
    float EatenSpeedMultiplier = 2f;
    float FrightenedSpeedMultiplier = 0.5f;
    float initial_speed;

    public bool EatenByPlayer { get; private set; } = false;

    [SerializeField] float InitialWaitingTime = 5f;
    [SerializeField] private Vector3 InitialTeleportPosition = new Vector3(13.5f,0f,19f);
    [SerializeField] private Vector3 PositionInCage = new Vector3(13.5f, 0f, 17f);
    private bool EnemyInCage = true;

    private GameObject TargetPlayer;
    private GameObject Blinky;
    
    [SerializeField] Vector3 TargetScatterPostion;
    private Vector3 TargetPlayerPosition;
    private Vector3 TargetPosition = Vector3.zero;
    
    private Vector3 LastIntersectionPosition = Vector3.zero;
    private bool LeaveLastIntersectionArea = true;

    private bool ClydeIsCloseToPlayer = false;

    private MoveDirection current_direction = MoveDirection.Right;

    private bool Frightened = false;

    [SerializeField] private Material InitialMaterial;
    [SerializeField] private Material FrightendedMaterial;
    [SerializeField] private Material EatenMaterial;

    enum EnemyName
    {
        BLINKY, PINKY, INKY, CLYDE
    }

    void Start()
    {
        initial_speed = EnemyMovementSpeed;
        StartCoroutine(GetOutOfCage(InitialWaitingTime));
        TargetPlayer = GameObject.FindWithTag("Player");
        if(enemyName == EnemyName.INKY)
        {
            Blinky = GameObject.Find("Blinky");
        }
    }

    private void Update()
    {
        if (EnemyInCage)
        {
            return;
        }
        {
            SetTargetPlayerPosition();
            CheckEnemyStateAndSetTargetPosition();
            CheckHitIntersectionOrTeleportationArea(0.01f);
            MoveAndTurn(EnemyMovementSpeed, current_direction, 0.1f, 0.5f);
        }
    }

    private void SetTargetPlayerPosition()
    {
        TargetPlayerPosition = TargetPlayer.transform.position;
        var TargetPlayerDirection = TargetPlayer.GetComponent<PlayerController>().PlayerCurrentDirection;
        if (enemyName == EnemyName.PINKY)
        {
            if (TargetPlayerDirection == MoveDirection.Forward)
            {
                TargetPlayerPosition += new Vector3(0f, 0f, 4f);
            }
            else if (TargetPlayerDirection == MoveDirection.Left)
            {
                TargetPlayerPosition += new Vector3(-4f, 0f, 0f);
            }
            else if (TargetPlayerDirection == MoveDirection.Backward)
            {
                TargetPlayerPosition += new Vector3(0f, 0f, -4f);
            }
            else if (TargetPlayerDirection == MoveDirection.Right)
            {
                TargetPlayerPosition += new Vector3(4f, 0f, 0f);
            }
        }
        if (enemyName == EnemyName.INKY)
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
        if (enemyName == EnemyName.CLYDE)
        {
            //print(Mathf.Abs(Vector3.Distance(this.transform.position, TargetPlayerPosition)));

            if (Mathf.Abs(Vector3.Distance(this.transform.position, TargetPlayerPosition)) < 8f)
            {
                ClydeIsCloseToPlayer = true;
            }
            else
            {
                ClydeIsCloseToPlayer = false;
            }
        }
    }

    private void CheckEnemyStateAndSetTargetPosition()
    {
        if (EatenByPlayer)
        {
            TargetPosition = InitialTeleportPosition;
            if(Mathf.Abs(Vector3.Distance(TargetPosition, transform.position)) <= 0.2)
            {
                EnemyMovementSpeed = initial_speed;
                transform.position = PositionInCage;
                EnemyInCage = true;
                ChageMaterial(InitialMaterial);
                StartCoroutine(GetOutOfCage(5f));
            }
        }
        else if (Frightened)
        {
            TargetPosition = new Vector3(Random.Range(0f, 30f), 0f, Random.Range(0f, 30f));
        }
        else if (EnemyCommander.instance.enemyState == EnemyCommander.EnemyState.SCATTER)
        {
            TargetPosition = TargetScatterPostion;
        }
        else if (EnemyCommander.instance.enemyState == EnemyCommander.EnemyState.CHASE)
        {
            if (ClydeIsCloseToPlayer)
            {
                TargetPosition = TargetScatterPostion;
            }
            else
            {
                TargetPosition = TargetPlayerPosition;
            }
        }
    }

    private void CheckHitIntersectionOrTeleportationArea(float CheckSphereSize)
    {
        var collideList = Physics.OverlapSphere(transform.position, CheckSphereSize);
        if (CollidesContainTag(collideList, "intersection_area"))
        {
            if (LeaveLastIntersectionArea)
            {
                LeaveLastIntersectionArea = false;
                GetLastIntersectionPosition(collideList);
                FindPossibleDirections();
            }
        }
        else
        {
            LeaveLastIntersectionArea = true;

            if(CollidesContainTag(collideList, "teleportation_point"))
            {
                HitTeleportationPoint(collideList);
            }
        }
    }

    private void GetLastIntersectionPosition(Collider[] collide_list)
    {
        var current_intersection_position = Vector3.zero;
        foreach (var collide in collide_list)
        {
            if (collide.tag == "intersection_area")
            {
                current_intersection_position = collide.transform.position;
            }
        }

        if (current_intersection_position != LastIntersectionPosition)
        {
            LastIntersectionPosition = current_intersection_position;
        }
    }

    private void HitTeleportationPoint(Collider[] collide_list)
    {
        foreach (var collide in collide_list)
        {
            if (collide.tag == "teleportation_point")
            {
                if(collide.GetComponent<TeleportationPoint>().AcceptableDirection == current_direction)
                {
                    transform.position = collide.GetComponent<TeleportationPoint>().TeleportTargetPosition;
                    break;
                }
            }
        }
    }

    private void FindPossibleDirections()
    {
        var possibleNextDirections = new HashSet<MoveDirection>() { MoveDirection.Forward,  MoveDirection.Left, MoveDirection.Backward, MoveDirection.Right};
        var DirectionToRemove = new HashSet<MoveDirection>();

        if (!Frightened)
        {
            possibleNextDirections.Remove(FindOppositeDirection(current_direction));
        }

        foreach (var direction in possibleNextDirections)
        {
            if (CheckIfFacingWall(direction, 0.1f, 1f))
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

    public void EatenByEmpoweredPlayer()
    {
        //current_direction = FindOppositeDirection(current_direction);
        Frightened = false;
        EatenByPlayer = true;
        EnemyMovementSpeed = initial_speed * EatenSpeedMultiplier;
        ChageMaterial(EatenMaterial);
    }

    public void RunAwayFromPlayer(bool playerEmpowered)
    {
        if (EnemyInCage)
        {
            return;
        }
        if (playerEmpowered)
        {
            Frightened = true;
            EnemyMovementSpeed = initial_speed * FrightenedSpeedMultiplier;
            current_direction = FindOppositeDirection(current_direction);
            ChageMaterial(FrightendedMaterial);
        }
        else
        {
            if(Frightened)
            {
                Frightened = false;
                EnemyMovementSpeed = initial_speed;
                ChageMaterial(InitialMaterial);
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

    IEnumerator GetOutOfCage(float wait_time_in_cage)
    {
        yield return new WaitForSeconds(wait_time_in_cage);
        EatenByPlayer = false;
        transform.position = InitialTeleportPosition;
        EnemyInCage = false;
    }

    void ChageMaterial(Material material)
    {
        var meshes = transform.GetChild(0);
        for (int i = 0; i < 2; i++)
        {
            meshes.GetChild(i).GetComponent<Renderer>().material = material;
        }
    }
}
