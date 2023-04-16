using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Character
{
    MoveDirection input_direction = MoveDirection.Right;
    public MoveDirection PlayerCurrentDirection = MoveDirection.Right;
    
    private void Start()
    {
        RoundPosition();
    }

    private void Update()
    {
        ChangeInputDirection();
        ChangeDirection();
        MoveAndTurn(2f, PlayerCurrentDirection, 0.1f, 0.5f);
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
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                PlayerCurrentDirection = MoveDirection.Forward;
            }
        }
        else if (input_direction == MoveDirection.Left)
        {
            if (!CheckFaceWall(MoveDirection.Left, 0.4f, 0.5f))
            {
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                PlayerCurrentDirection = MoveDirection.Left;
            }
        }
        else if (input_direction == MoveDirection.Backward)
        {
            if (!CheckFaceWall(MoveDirection.Backward, 0.4f, 0.5f))
            {
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                PlayerCurrentDirection = MoveDirection.Backward;
            }
        }
        else if (input_direction == MoveDirection.Right)
        {
            if (!CheckFaceWall(MoveDirection.Right, 0.4f, 0.5f))
            {
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPosition();
                PlayerCurrentDirection = MoveDirection.Right;
            }
        }
    }

    private bool CheckOppositeDirectionInput()
    {
        if ((PlayerCurrentDirection == MoveDirection.Left && input_direction == MoveDirection.Right)
            || (PlayerCurrentDirection == MoveDirection.Right && input_direction == MoveDirection.Left)
                || (PlayerCurrentDirection == MoveDirection.Forward && input_direction == MoveDirection.Backward)
                    || (PlayerCurrentDirection == MoveDirection.Backward && input_direction == MoveDirection.Forward))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
