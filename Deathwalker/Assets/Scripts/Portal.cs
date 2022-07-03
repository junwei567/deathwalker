using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    protected override void onCollide(Collider2D col)
    {
        if (col.name == "Player") {
            SceneManager.LoadScene("Dungeon1");
        }
    }
}
