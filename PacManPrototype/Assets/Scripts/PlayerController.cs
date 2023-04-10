using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    private MoveDirection current_direction = MoveDirection.Right;
    private MoveDirection input_direction = MoveDirection.Right;
    [SerializeField] float movement_speed = 1f;

    private void Start()
    {
        
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
            if(!Physics.CheckSphere(transform.position + new Vector3(0f, 0f, 1f), 0.4f))
            {
                current_direction = MoveDirection.Forward;
            }
        }
        else if (input_direction == MoveDirection.Left)
        {
            if (!Physics.CheckSphere(transform.position + new Vector3(-1f, 0f, 0f), 0.4f))
            {
                current_direction = MoveDirection.Left;
            }
        }
        else if (input_direction == MoveDirection.Backward)
        {
            if (!Physics.CheckSphere(transform.position + new Vector3(0f, 0f, -1f), 0.4f))
            {
                current_direction = MoveDirection.Backward;
            }
        }
        else if (input_direction == MoveDirection.Right)
        {
            if (!Physics.CheckSphere(transform.position + new Vector3(1f, 0f, 0f), 0.4f))
            {
                current_direction = MoveDirection.Right;
            }
        }
    }

    private void Move()
    {
        if(current_direction== MoveDirection.Forward)
        {
            transform.position += new Vector3(0f, 0f, movement_speed * Time.deltaTime);
        }
        else if (current_direction == MoveDirection.Left)
        {
            transform.position += new Vector3(-movement_speed * Time.deltaTime, 0f, 0f);
        }
        else if (current_direction == MoveDirection.Backward)
        {
            transform.position += new Vector3(0f, 0f, -movement_speed * Time.deltaTime);
        }
        else if (current_direction == MoveDirection.Right)
        {
            transform.position += new Vector3(movement_speed * Time.deltaTime, 0f, 0f);
        }
    }
}
