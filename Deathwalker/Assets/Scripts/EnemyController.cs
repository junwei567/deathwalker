using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Movement
{
    public Transform playerPos;
    protected override void Update()
    {
        base.Update();
        UpdateMovement(new Vector3(playerPos.transform.position.x - transform.position.x, playerPos.transform.position.y - transform.position.y, 0));
    }
}
