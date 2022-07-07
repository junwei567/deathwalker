using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Collidable
{
    protected override void onCollide(Collider2D col)
    {
        // GameManager.instance.Collisions = GameManager.instance.Collisions + 1;
        // GameManager.instance.SaveState();
        if (!collided && col.name == "Player") {
            collided = true;
            GameManager.instance.ShowText("Ouch!", 25, Color.white, transform.position, Vector3.up * 10, 1.0f);
        }
    } 

}
