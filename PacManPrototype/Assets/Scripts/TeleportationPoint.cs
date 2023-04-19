using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationPoint : Character
{
    [SerializeField] public MoveDirection AcceptableDirection = MoveDirection.Right;

    [SerializeField] public Vector3 TeleportTargetPosition;


}
