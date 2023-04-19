using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : Character
{
    [SerializeField] float PlayerSpeed = 2f;
    
    MoveDirection input_direction = MoveDirection.Forward;
    public MoveDirection PlayerCurrentDirection { get; private set; } = MoveDirection.Forward;

    private void Update()
    {
        ChangeInputDirection();
        ChangeDirection();
        MoveAndTurn(PlayerSpeed, PlayerCurrentDirection, 0.1f, 0.5f);

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.instance.ReloadScene();
        }
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
            if(!CheckIfFacingWall(MoveDirection.Forward, 0.4f, 0.5f))
            {
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPositionValues();
                PlayerCurrentDirection = MoveDirection.Forward;
            }
        }
        else if (input_direction == MoveDirection.Left)
        {
            if (!CheckIfFacingWall(MoveDirection.Left, 0.4f, 0.5f))
            {
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPositionValues();
                PlayerCurrentDirection = MoveDirection.Left;
            }
        }
        else if (input_direction == MoveDirection.Backward)
        {
            if (!CheckIfFacingWall(MoveDirection.Backward, 0.4f, 0.5f))
            {
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPositionValues();
                PlayerCurrentDirection = MoveDirection.Backward;
            }
        }
        else if (input_direction == MoveDirection.Right)
        {
            if (!CheckIfFacingWall(MoveDirection.Right, 0.4f, 0.5f))
            {
                if ((PlayerCurrentDirection != input_direction) && (!CheckOppositeDirectionInput()))
                    RoundPositionValues();
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
