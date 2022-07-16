using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Movement
{
    public float dashSpeed = 100.0f;
    private bool dashing = false;
    public float dashTime = 1.0f;
    public float lastUsed;
    public float cooldown = 1.0f;
    public float threshold = 0.01f;
    public GameObject enemy;
    private Animator playerAnimator;
    private AudioSource playerAudio;

    [SerializeField]
    private IntegerSO playerHealthSO;

    protected override void Start()
    {
        base.Start();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector2 crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float x = crosshairPos.x - transform.position.x;
        float y = crosshairPos.y - transform.position.y;
        // Update Animator's parameters
        playerAnimator.SetFloat("xySpeed", (Mathf.Abs(x) + Mathf.Abs(y)));

        // If player clicks on dash button
        if (Input.GetMouseButtonDown(0)) {
            // If skill is NOT on cooldown
            if (Time.time - lastUsed > cooldown) {
                lastUsed = Time.time;
                dashing = true;
                playerAnimator.SetBool("dashing", dashing);
            } 
            else {
                dashing = false;
                playerAnimator.SetBool("dashing", dashing);
            }
            
        }
        // Update dashing variable to false when dash is over
        // Dashing will be true for dash time interval
        if (dashing && Time.time - lastUsed > dashTime){
            dashing = false;
            playerAnimator.SetBool("dashing", dashing);
        }

        // Stop moving the player if player is close enough to the crosshair    
        if (Mathf.Abs(x) < threshold && Mathf.Abs(y) < threshold) {
            UpdateMovement(Vector3.zero);
        } 
        // If in dash
        else if (dashing) {
            UpdateMovement(new Vector3(x, y, 0).normalized * dashSpeed);
        } 
        // If out of dash
        else {
            UpdateMovement(new Vector3(x,y,0).normalized);
        }

    }
    void PlayDashSound()
    {
        if (!playerAudio.isPlaying){
            playerAudio.PlayOneShot(playerAudio.clip);
        }
    }

    protected override void onCollide(Collider2D col)
    {
        if (!collided && col.name == "SkullEnemy" && dashing) {
            collided = true;
            Destroy(enemy);
        }
    }
}
