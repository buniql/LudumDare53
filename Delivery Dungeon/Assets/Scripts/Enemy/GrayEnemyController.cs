using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayEnemyController : EnemyController
{
    public override void Move()
    {
        _rigidbody2D.MovePosition(transform.position - direction * MovementSpeed * Time.fixedDeltaTime);
    }
}
