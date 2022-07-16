using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : Movement
{
    public float cooldown = 2.0f;
    public float lastUsed;
    public float arrowSpeed = 4.0f;
    public GameObject player;
    public GameObject arrowPrefab;
    private bool startShooting = false;
    private bool firstShotFired = false;
    private bool inRange = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(countdownToShoot());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Get relative direction to player
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;
        Vector2 dirToPlayer = new Vector2(x, y).normalized;
        // If player is in shooting range and allowed to shoot
        if (startShooting && inRange) {
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
    IEnumerator countdownToShoot() {
        yield return new WaitForSeconds(1.5f);
        // Start shooting after waiting for an initial 1.5 seconds
        startShooting = true;
    }
    void Shoot(Vector2 shootingDir)
    {   
        firstShotFired = true;
        lastUsed = Time.time;
        // Spawn arrow
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = shootingDir * arrowSpeed;
        arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(shootingDir.y, shootingDir.x) * Mathf.Rad2Deg);
    }
}
