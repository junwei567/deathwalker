using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Collidable
{
    protected override void onCollide(Collider2D col)
    {
        GameManager.instance.Collisions = GameManager.instance.Collisions + 1;
        GameManager.instance.SaveState();
        Debug.Log("box collided");
    }

}
