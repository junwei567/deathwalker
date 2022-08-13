using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_shake : MonoBehaviour
{
    public Animator camAnim;

    public void playerCamShake(){
        int rand = Random.Range(0, 3);
        if (rand == 0){
            camAnim.SetTrigger("shake");
        } else if(rand == 1){
            camAnim.SetTrigger("shake2");
        } else if(rand == 2){
            camAnim.SetTrigger("shake3");
        }

    }
    public void enemyCamShake(){
        int rand = Random.Range(0, 2);
        if (rand == 0){
            camAnim.SetTrigger("shake4");
        } else if(rand == 1){
            camAnim.SetTrigger("shake5");
        } 

    }
}
