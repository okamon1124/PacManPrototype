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
            if (!CheckFaceWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(0f, 0f, movementSpeed * Time.deltaTime);
            else
                RoundPosition();
        }
        else if (currentDirection == MoveDirection.Left)
        {
            if (!CheckFaceWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(-movementSpeed * Time.deltaTime, 0f, 0f);
            else
                RoundPosition();
        }
        else if (currentDirection == MoveDirection.Backward)
        {
            if (!CheckFaceWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(0f, 0f, -movementSpeed * Time.deltaTime);
            else
                RoundPosition();
        }
        else if (currentDirection == MoveDirection.Right)
        {
            if (!CheckFaceWall(currentDirection, CheckSphereSize, CheckSpherePosition))
                transform.position += new Vector3(movementSpeed * Time.deltaTime, 0f, 0f);
            else
                RoundPosition();
        }
    }

    
    protected void RoundPosition()
    {
        double x_pos = Convert.ToDouble(transform.position.x);
        double y_pos = Convert.ToDouble(transform.position.y);
        double z_pos = Convert.ToDouble(transform.position.z);

        x_pos = Math.Round(x_pos);
        y_pos = Math.Round(y_pos);
        z_pos = Math.Round(z_pos);

        transform.position = new Vector3((float)x_pos, (float)y_pos, (float)z_pos);
    }

    protected bool CheckFaceWall(MoveDirection moveDirection, float CheckSphereSize, float CheckSpherePosition)
    {
        if (moveDirection == MoveDirection.Forward)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, CheckSpherePosition), CheckSphereSize);
            return CollideChecker(collideList, "wall");
        }
        else if (moveDirection == MoveDirection.Left)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(-CheckSpherePosition, 0f, 0f), CheckSphereSize);
            return CollideChecker(collideList, "wall");
        }
        else if (moveDirection == MoveDirection.Backward)
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(0f, 0f, -CheckSpherePosition), CheckSphereSize);
            return CollideChecker(collideList, "wall");
        }
        else
        {
            var collideList = Physics.OverlapSphere(transform.position + new Vector3(CheckSpherePosition, 0f, 0f), CheckSphereSize);
            return CollideChecker(collideList, "wall");
        }
    }

    protected bool CollideChecker(Collider[] collideList, string propose_tag)
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
