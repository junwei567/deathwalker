using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : Movement
{
    public float cooldown = 2.0f;
    public float archerSpeed = 0.4f;
    public float lastUsed;
    public float arrowSpeed = 2.0f;
    private GameObject player;
    public GameObject arrowPrefab;
    private bool startShooting = false;
    private bool firstShotFired = false;
    private bool inRange = false;
    private Animator archerAnimator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(countdownToShoot());
        archerAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Get relative direction to player
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        // Normalize to get general direction
        Vector2 dirToPlayer = new Vector2(x, y).normalized;
        // If player is in shooting range and allowed to shoot
        if (startShooting && inRange) {
            // Update animation to activate idle
            archerAnimator.SetFloat("xySpeed", 0);
            // If first arrow is not shot
            if (!firstShotFired) {
                Shoot(dirToPlayer);
            } 
            else {
                // If cooldown is over, shoot
                if (Time.time - lastUsed > cooldown) {
                    Shoot(dirToPlayer); 
                }
            }
        }
        // If player is not in shooting range
        if (!inRange) {
            archerAnimator.SetFloat("xySpeed", (Mathf.Abs(x) + Mathf.Abs(y)));
            UpdateMovement(new Vector3(x,y,0).normalized * archerSpeed);
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
    IEnumerator countdownToShoot() 
    {
        yield return new WaitForSeconds(0.5f);
        // Start shooting after waiting for an initial 1.5 seconds
        startShooting = true;
    }
    IEnumerator startShootAnimation(Vector2 shootingDir) 
    {
        yield return new WaitForSeconds(0.35f);
        // Spawn arrow at the end of 0.35 seconds
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = shootingDir * arrowSpeed;
        arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg);
        // Destroy arrow gameObject after 2 seconds
        Destroy(arrow, 2.0f);
        // Set shooting to false
        archerAnimator.SetBool("shooting", false);
    }
    void Shoot(Vector2 shootingDir)
    {   
        archerAnimator.SetBool("shooting", true);
        firstShotFired = true;
        // Update last time arrow was shot
        lastUsed = Time.time;
        // Start shoot animation and instantiate arrow prefab
        StartCoroutine(startShootAnimation(shootingDir));
    }
}
