using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Character
{
    MoveDirection input_direction = MoveDirection.Right;
    MoveDirection current_direction = MoveDirection.Right;
    
    private void Start()
    {
        RoundPosition();
    }

    private void Update()
    {
        ChangeInputDirection();
        ChangeDirection();
        MoveAndTurn(2f, current_direction, 0.1f, 0.5f);
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
