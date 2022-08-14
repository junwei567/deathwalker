using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_shake : MonoBehaviour
{
    public Animator camAnim;
    public AudioSource audioSource;
    public AudioClip hitAudio1;
    public AudioClip hitAudio2;
    public AudioClip slashAudio1;
    public AudioClip slashAudio2;
    public AudioClip killAudio1;
    public AudioClip killAudio2;
    public AudioClip killAudio3;
    public AudioClip killAudio4;

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
    public void hitAudio(){
        int rand = Random.Range(0, 2);
        if (rand == 0){
            audioSource.clip = hitAudio1;
            audioSource.Play();
        } else if(rand == 1){
            audioSource.clip = hitAudio2;
            audioSource.Play();
        }

    }
    public void slashAudio(){
        int rand = Random.Range(0, 2);
        if (rand == 0){
            audioSource.clip = slashAudio1;
            audioSource.Play();
        } else if(rand == 1){
            audioSource.clip = slashAudio2;
            audioSource.Play();
        }

    }
    public void killAudio(){
        int rand = Random.Range(0, 4);
        if (rand == 0){
            audioSource.clip = killAudio1;
            audioSource.Play();
        } else if(rand == 1){
            audioSource.clip = killAudio2;
            audioSource.Play();
        } else if(rand == 2){
            audioSource.clip = killAudio3;
            audioSource.Play();
        } else if(rand == 3){
            audioSource.clip = killAudio4;
            audioSource.Play();
        }

    }
}
