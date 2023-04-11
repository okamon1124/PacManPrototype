using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Character
{
    private MoveDirection current_direction = MoveDirection.Right;
    private MoveDirection input_direction = MoveDirection.Right;
    [SerializeField] float movement_speed = 1f;


    private void Start()
    {
        RoundPosition();
    }

    private void Update()
    {
        ChangeInputDirection();
        ChangeDirection();
        Move();
    }

    private void ChangeInputDirection()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            input_direction = MoveDirection.Forward;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            input_direction = MoveDirection.Left;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            input_direction = MoveDirection.Backward;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            input_direction = MoveDirection.Right;
        }
    }

    private void ChangeDirection()
    {
        if(input_direction == MoveDirection.Forward)
        {
            if(!CheckFaceWall(MoveDirection.Forward, 0.4f, 0.5f))
            {
                if ((current_direction != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                current_direction = MoveDirection.Forward;
            }
        }
        else if (input_direction == MoveDirection.Left)
        {
            if (!CheckFaceWall(MoveDirection.Left, 0.4f, 0.5f))
            {
                if ((current_direction != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                current_direction = MoveDirection.Left;
            }
        }
        else if (input_direction == MoveDirection.Backward)
        {
            if (!CheckFaceWall(MoveDirection.Backward, 0.4f, 0.5f))
            {
                if ((current_direction != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                current_direction = MoveDirection.Backward;
            }
        }
        else if (input_direction == MoveDirection.Right)
        {
            if (!CheckFaceWall(MoveDirection.Right, 0.4f, 0.5f))
            {
                if ((current_direction != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                current_direction = MoveDirection.Right;
            }
        }
    }

    private void Move()
    {
        if(current_direction== MoveDirection.Forward)
        {
            if(!CheckFaceWall(current_direction, 0.1f, 0.5f))
                transform.position += new Vector3(0f, 0f, movement_speed * Time.deltaTime);
            else
                RoundPosition();
        }
        else if (current_direction == MoveDirection.Left)
        {
            if (!CheckFaceWall(current_direction, 0.1f, 0.5f))
                transform.position += new Vector3(-movement_speed * Time.deltaTime, 0f, 0f);
            else
                RoundPosition();
        }
        else if (current_direction == MoveDirection.Backward)
        {
            if (!CheckFaceWall(current_direction, 0.1f, 0.5f))
                transform.position += new Vector3(0f, 0f, -movement_speed * Time.deltaTime);
            else
                RoundPosition();
        }
        else if (current_direction == MoveDirection.Right)
        {
            if (!CheckFaceWall(current_direction, 0.1f, 0.5f))
                transform.position += new Vector3(movement_speed * Time.deltaTime, 0f, 0f);
            else
                RoundPosition();
        }
    }

    private void RoundPosition()
    {
        double x_pos = Convert.ToDouble(transform.position.x);
        double y_pos = Convert.ToDouble(transform.position.y);
        double z_pos = Convert.ToDouble(transform.position.z);

        x_pos = Math.Round(x_pos);
        y_pos = Math.Round(y_pos);
        z_pos = Math.Round(z_pos);

        transform.position = new Vector3((float)x_pos, (float)y_pos, (float)z_pos);
    }

    private bool CheckFaceWall(MoveDirection moveDirection, float CheckSphereSize, float CheckSpherePosition)
    {
        if(moveDirection== MoveDirection.Forward)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, CheckSpherePosition), CheckSphereSize);
            return CollideChecker(collideList);
        }
        else if(moveDirection== MoveDirection.Left)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(-CheckSpherePosition, 0f, 0f), CheckSphereSize);
            return CollideChecker(collideList);
        }
        else if (moveDirection== MoveDirection.Backward)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, -CheckSpherePosition), CheckSphereSize);
            return CollideChecker(collideList);
        }
        else
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(CheckSpherePosition, 0f, 0f), CheckSphereSize);
            return CollideChecker(collideList);
        }
    }

    private bool CollideChecker(Collider[] collideList)
    {
        bool any_wall = false;

        foreach (var collide in collideList)
        {
            if (collide.tag == "wall")
            {
                any_wall = true;
            }
        }
        return any_wall;
    }

    private bool CheckOppositeDirectionInput()
    {
        if ((current_direction == MoveDirection.Left && input_direction == MoveDirection.Right)
            || (current_direction == MoveDirection.Right && input_direction == MoveDirection.Left)
                || (current_direction == MoveDirection.Forward && input_direction == MoveDirection.Backward)
                    || (current_direction == MoveDirection.Backward && input_direction == MoveDirection.Forward))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
