using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : Movement
{
    public float cooldown = 1.0f;
    public float lastUsed;
    public float startTime;
    public float knightSpeed = 0.55f;
    public GameObject knight;
    private GameObject player;
    private bool lunging = false;
    private bool inRange;
    private bool charge;
    public float attackRadius;
    private Animator anim;
    public LayerMask whatIsPlayer;
    public float dashSpeed = 1.2f;
    private float target_x;
    private float target_y;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
        lastUsed = Time.time;
        lunging = false;
        charge = false;
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
                lunging = true;
                anim.SetBool("inAttackRange", true);
                target_x = x;
                target_y = y;
                Debug.Log("winding");
                startTime = Time.time;
                StartCoroutine(Lunge(target_x,target_y));
                // Lunge(x, y); 
            }
        }
        if (charge){
            UpdateMovement(new Vector3(target_x, target_y, 0).normalized * 5.0f);
        }
        // if (lunging){
        //     if (step == 300){
        //         Debug.Log("lunging");
        //         Debug.Log(Time.time-startTime);
        //         startTime = Time.time;
        //         anim.SetBool("inAttackRange", false);
        //     }

        //     if (step == 340){
        //         Debug.Log("lunge finish");
        //         Debug.Log(Time.time-startTime);
        //         startTime = Time.time;
        //     }

        //     if (step >= 300 && step <= 360){
        //         UpdateMovement(new Vector3(target_x, target_y, 0).normalized * 5.0f);
        //     }

        //     if (step == 660){
        //         Debug.Log("move");
        //         Debug.Log(Time.time-startTime);
        //         startTime = Time.time;
        //     }

        //     if (step >= 660){
        //         Debug.Log(Time.time-startTime);
        //         startTime = Time.time;
        //         lunging = false;
        //         lastUsed = Time.time;
        //         step = -1;
        //     }

        //     step += 1;
        // }
        // If player is not in shooting range
        if (!lunging) {
            UpdateMovement(new Vector3(x,y,0).normalized * knightSpeed);
        }
    }

        IEnumerator Lunge(float target_x, float target_y) 
        {
            yield return new WaitForSeconds(0.45f);
            anim.SetBool("inAttackRange", false);
            charge = true;
            StartCoroutine(Dash());
            yield return new WaitForSeconds(0.55f);
            lunging = false;
            lastUsed = Time.time;
        }
        IEnumerator Dash() 
        {
            yield return new WaitForSeconds(0.15f);
            charge = false;
        }

}

