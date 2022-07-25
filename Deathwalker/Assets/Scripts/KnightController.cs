using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : Movement
{
    public float cooldown = 1.0f;
    public float lastUsed;
    public GameObject knight;
    public GameObject player;
    private bool lunging = false;
    private bool inRange;
    public float attackRadius;
    private Animator anim;
    public LayerMask whatIsPlayer;
    public float dashSpeed = 1.2f;
    private int step = 0;
    private float target_x;
    private float target_y;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        lastUsed = Time.time;
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
        if (inRange && !lunging) {
            // If cooldown is over, shoot
            if (Time.time - lastUsed > cooldown) {
                Debug.Log("Lunging");
                lunging = true;
                anim.SetBool("inAttackRange", inRange);
                target_x = x;
                target_y = y;
                // Lunge(x, y); 
            }
        }
        if (lunging){
            Debug.Log(step);
            if (step >= 100 && step <= 150){
                anim.SetBool("inAttackRange", false);
                UpdateMovement(new Vector3(target_x, target_y, 0).normalized * 7.0f);
            }
            else if (step >= 250){
                lunging = false;
                lastUsed = Time.time;
                step = 0;
            }
            step += 1;
        }
        // If player is not in shooting range
        if (!lunging) {
            // Debug.Log(lunged);
            UpdateMovement(new Vector3(x,y,0).normalized * 0.7f);
        }
    }
    // void Lunge(float target_x,float target_y)
    // {   
    //     // StartCoroutine(countdownToLunge(target_x,target_y));
    //     anim.SetBool("inAttackRange", false);
    //     int step = 0;
    //     while (step < 5){
    //         UpdateMovement(new Vector3(target_x, target_y, 0).normalized * dashSpeed);
    //         Debug.Log(new Vector3(target_x, target_y, 0).normalized * dashSpeed);
    //         step += 1;
    //         yield return new WaitForSeconds(0.003f);
    //     }
    //     yield return new WaitForSeconds(0.25f);
    //     // Start shooting after waiting for an initial 1.5 seconds
    //     lunging = false;
    //     lastUsed = Time.time;
    // }
    // IEnumerator countdownToLunge(float target_x, float target_y) {
    //     yield return new WaitForSeconds(0.25f);
    //     anim.SetBool("inAttackRange", false);
    //     int step = 0;
    //     while (step < 5){
    //         UpdateMovement(new Vector3(target_x, target_y, 0).normalized * dashSpeed);
    //         Debug.Log(new Vector3(target_x, target_y, 0).normalized * dashSpeed);
    //         step += 1;
    //         yield return new WaitForSeconds(0.003f);
    //     }
    //     yield return new WaitForSeconds(0.25f);
    //     // Start shooting after waiting for an initial 1.5 seconds
    //     lunging = false;
    //     lastUsed = Time.time;
    // }
}

