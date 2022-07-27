using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Collidable
{
    [SerializeField]
    private IntegerSO playerHealthSO;
    protected override void onCollide(Collider2D col)
    {
        if (!collided && col.name == "Player") {
            collided = true;
            // Decrease player health
            playerHealthSO.Value--;
            // GameManager.instance.displayHealth();
        }
    }
}
