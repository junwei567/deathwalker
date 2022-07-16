using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : Movement
{
    public float cooldown = 3.0f;
    public float lastUsed;
    public GameObject knight;
    public GameObject player;
    private bool inRange = false;
    private bool firstLunge = true;
    private bool lunged = false;
    public float attackRadius;
    private Animator anim;
    public LayerMask whatIsPlayer;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Get relative direction to player
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        Vector2 dirToPlayer = new Vector2(x, y).normalized;
        
        inRange = Physics2D.OverlapCircle(transform.position,attackRadius, whatIsPlayer);
        anim.SetBool("inAttackRange", inRange);

        // If player is in shooting range and allowed to shoot
        if (inRange) {
            // If first arrow is not shot
            if (firstLunge) {
                Debug.Log("Lunge");
                Lunge();
            } 
            else {
                // If cooldown is over, shoot
                if (Time.time - lastUsed > cooldown) {
                    Debug.Log("Lunge");
                    Lunge(); 
                }
            }
        }
        // If player is not in shooting range
        if (!lunged) {
            // Debug.Log(lunged);
            UpdateMovement(new Vector3(x,y,0));
        }
    }
    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.tag == "Player") {
            inRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player") {
            inRange = false;
        }
    }
    // Wait 1.5 seconds before archer is allowed to shoot when it is spawned
    IEnumerator countdownToLunge() {
        if (lunged){

            yield return new WaitForSeconds(2);
            // Start shooting after waiting for an initial 1.5 seconds
            lunged = false;
        }
    }
    void Lunge()
    {   
        firstLunge = false;
        lunged = true;
        StartCoroutine(countdownToLunge());
        lastUsed = Time.time;
    }
}

