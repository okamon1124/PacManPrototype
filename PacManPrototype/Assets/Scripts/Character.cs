using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character: MonoBehaviour
{
    public enum MoveDirection
    {
        Left,
        Right,
        Forward,
        Backward
    }

    protected void MoveAndTurn(float movementSpeed, MoveDirection currentDirection,float CheckSphereSize, float CheckSpherePosition)
    {
        if (currentDirection == MoveDirection.Forward)
        {
            if (!CheckIfFacingWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(0f, 0f, movementSpeed * Time.deltaTime);
            else
                RoundPositionValues();
        }
        else if (currentDirection == MoveDirection.Left)
        {
            if (!CheckIfFacingWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(-movementSpeed * Time.deltaTime, 0f, 0f);
            else
                RoundPositionValues();
        }
        else if (currentDirection == MoveDirection.Backward)
        {
            if (!CheckIfFacingWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(0f, 0f, -movementSpeed * Time.deltaTime);
            else
                RoundPositionValues();
        }
        else if (currentDirection == MoveDirection.Right)
        {
            if (!CheckIfFacingWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);
            else
                RoundPositionValues();
        }
    }

    
    protected void RoundPositionValues()
    {
        double x_pos = Convert.ToDouble(transform.position.x);
        double y_pos = Convert.ToDouble(transform.position.y);
        double z_pos = Convert.ToDouble(transform.position.z);

        x_pos = Math.Round(x_pos);
        y_pos = Math.Round(y_pos);
        z_pos = Math.Round(z_pos);

        transform.position = new Vector3((float)x_pos, (float)y_pos, (float)z_pos);
    }

    protected bool CheckIfFacingWall(MoveDirection moveDirection, float CheckSphereSize, float CheckSpherePosition)
    {
        if (moveDirection == MoveDirection.Forward)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, CheckSpherePosition), CheckSphereSize);
            return CollidesContainTag(collideList, "wall");
        }
        else if (moveDirection == MoveDirection.Left)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(-CheckSpherePosition, 0f, 0f), CheckSphereSize);
            return CollidesContainTag(collideList, "wall");
        }
        else if (moveDirection == MoveDirection.Backward)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, -CheckSpherePosition), CheckSphereSize);
            return CollidesContainTag(collideList, "wall");
        }
        else
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(CheckSpherePosition, 0f, 0f), CheckSphereSize);
            return CollidesContainTag(collideList, "wall");
        }
    }

    protected bool CollidesContainTag(Collider[] collideList, string propose_tag)
    {
        bool found_any = false;

        foreach (var collide in collideList)
        {
            if (collide.tag == propose_tag)
            {
                found_any = true;
            }
        }
        return found_any;
    }

}
