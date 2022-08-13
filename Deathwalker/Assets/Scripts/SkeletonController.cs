using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : Movement
{
    public float cooldown = 1.0f;
    public float lastUsed;
    public float startTime;
    public float reviveTimer;
    public GameObject skeleton;
    private GameObject player;
    private bool attack;
    private bool inRange;
    private bool revive;
    public bool dying = false;
    public bool alive = true;
    public float attackRadius = 3.0f;
    private Animator anim;
    public LayerMask whatIsPlayer;
    public float dashSpeed = 1.2f;
    public float reviveTimeout = 8.0f;
    public float deadTime;
    // private int counter = 0;
    private int deadlock_counter = 0;
    private float target_x;
    private float target_y;
    private cam_shake cam_shake;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        lastUsed = Time.time;
        deadTime = Time.time;
        attack = false;
        anim.SetBool("inRange", false);
        anim.SetBool("isDead", false);
        anim.SetBool("timeout", false);
        cam_shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<cam_shake>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Get relative direction to player
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        
        inRange = Physics2D.OverlapCircle(transform.position,attackRadius, whatIsPlayer);

        // If player is in shooting range and allowed to shoot
        if (inRange && !attack && alive) {
            // Debug.Log("atking");
            // If cooldown is over, shoot
            if (Time.time - lastUsed > cooldown) {
                attack = true;
                StartCoroutine(attackCoroutine());
            }
        }

        // resolving skeleton deadlock
        if(attack){
            // Debug.Log("atking check");
            deadlock_counter += 1;
            if (deadlock_counter > 3000){
                attack = false;
                deadlock_counter = 0;
            }
        }
        else{
            deadlock_counter = 0;
        }

        // // trying out death animation
        // if (!dead){
        //     if (counter >= 10000 && !attack){
        //         dead = true;
        //         counter = 0;
        //         StartCoroutine(deadCoroutine());
        //     }
        //     counter +=1;
        // }

        if (dying){
            Debug.Log("hit");
            attack = false;
            dying = false;
            // can only die earliest 8 seconds after latest death
            if (Time.time - deadTime > reviveTimeout && alive){
                alive = false;
                anim.SetBool("inRange", false);
                anim.SetBool("dying", true);
                anim.SetBool("isDead", true);
                Debug.Log("dying");
                StartCoroutine(deadCoroutine());
                deadTime = Time.time;
            }
        }

        // If player is not in shooting range
        if (!attack && alive) {
            // Debug.Log("moving");
            UpdateMovement(new Vector3(x,y,0).normalized * 0.3f);
        }
    }
    IEnumerator attackCoroutine() {
        if (attack){
            StartCoroutine(camShakeCoroutine());
            anim.SetBool("inRange", true);
            yield return new WaitForSeconds(2);
            anim.SetBool("inRange", false);
            attack = false;
            lastUsed = Time.time;
        }
    }

    IEnumerator camShakeCoroutine() {
        yield return new WaitForSeconds(0.7f);
        cam_shake.enemyCamShake();
    }
    
    // if enemy dead
    IEnumerator deadCoroutine() {
        yield return new WaitForSeconds(2);
        StartCoroutine(reviveCoroutine());
    }

    // revive enemy after timeout
    IEnumerator reviveCoroutine() {
        anim.SetBool("dying", false);
        anim.SetBool("isDead", false);
        anim.SetBool("timeout", true);
        yield return new WaitForSeconds(3);
        anim.SetBool("timeout", false);
        alive = true;
    }
}

