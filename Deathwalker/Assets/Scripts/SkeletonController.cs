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
    public GameObject player;
    private bool attack = false;
    private bool inRange;
    private bool dead = false;
    private bool revive = false;
    public float attackRadius = 3.0f;
    private Animator anim;
    public LayerMask whatIsPlayer;
    public float dashSpeed = 1.2f;
    public float reviveTimeout = 5.0f;
    private int counter = 0;
    private float target_x;
    private float target_y;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        lastUsed = Time.time;
        anim.SetBool("inRange", false);
        anim.SetBool("isDead", false);
        anim.SetBool("timeout", false);
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
        if (inRange && !attack && !dead) {
            // If cooldown is over, shoot
            if (Time.time - lastUsed > cooldown) {
                Debug.Log("Attack");
                attack = true;
                StartCoroutine(attackCoroutine());
            }
        }

        // trying out death animation
        if (!dead){
            if (counter >=2000 && !attack){
                Debug.Log("revive timer");
                dead = true;
                counter = 0;
                StartCoroutine(deadCoroutine());
            }
            counter +=1;
        }

        // If player is not in shooting range
        if (!attack && !dead) {
            UpdateMovement(new Vector3(x,y,0).normalized * 0.3f);
        }
    }
    IEnumerator attackCoroutine() {
        anim.SetBool("inRange", true);
        Debug.Log("wait 2 secs");
        yield return new WaitForSeconds(2);
        Debug.Log("finish waiting 2 secs");
        anim.SetBool("inRange", false);
        attack = false;
        lastUsed = Time.time;
    }
    
    // if enemy dead
    IEnumerator deadCoroutine() {
        Debug.Log("dead");
        anim.SetBool("isDead", true);
        Debug.Log("wait 10 secs");
        yield return new WaitForSeconds(10);
        Debug.Log("finish waiting 10 secs");
        StartCoroutine(reviveCoroutine());
    }

    // revive enemy after timeout
    IEnumerator reviveCoroutine() {
        Debug.Log("revive");
        anim.SetBool("timeout", true);
        Debug.Log("wait 3 secs");
        yield return new WaitForSeconds(3);
        Debug.Log("finish waiting 3 secs");
        anim.SetBool("timeout", false);
        anim.SetBool("isDead", false);
        dead = false;
    }
}

