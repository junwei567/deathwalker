using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Movement
{
    public float dashSpeed = 100.0f;
    private bool dashing = false;
    public float dashTime = 0.1f;
    public float lastUsed;
    public float cooldown = 1.0f;
    public float threshold = 0.01f;
    public float immuneTime = 2.0f;
    public bool doubleDash = false;
    private int dashCount = 0;
    private Animator playerAnimator;
    private AudioSource playerAudio;
    private SpriteRenderer playerRenderer;

    [SerializeField]
    private IntegerSO playerHealthSO;
 
    protected override void Start()
    {
        base.Start();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerRenderer = GetComponent<SpriteRenderer>();
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
            // DOUBLE DASH ACTIVATED and not currently dashing
            if (doubleDash && !dashing) {
                // Add to dash counter
                dashCount++;
                Debug.Log(dashCount);
                dashing = true;
                playerAnimator.SetBool("dashing", dashing);
                // Add cooldown since player already dashed twice
                if (dashCount == 2) {
                   lastUsed = Time.time;
                   // Reset dash count
                   dashCount = 0;
                }
            } 
            // NO DOUBLE DASH
            if (!doubleDash) {
                // If skill is NOT on cooldown
                if (Time.time - lastUsed > cooldown) {
                    lastUsed = Time.time;
                    dashing = true;
                    playerAnimator.SetBool("dashing", dashing);
                } 
            }
            // else {
            //     dashing = false;
            //     playerAnimator.SetBool("dashing", dashing);
            // }
            
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
    void activateLongDash()
    {
        dashSpeed = 150.0f;
    }
    void activateDoubleDash()
    {
        doubleDash = true;
    }
    void PlayDashSound()
    {
        if (!playerAudio.isPlaying){
            playerAudio.PlayOneShot(playerAudio.clip);
        }
    }
    void TakeDamage()
    {
        collided = true;
        playerHealthSO.Value--;
        GameManager.instance.displayHealth(playerHealthSO.Value);
        StartCoroutine(StartImmunity());
    }

    IEnumerator StartImmunity()
    {
        var endTime = Time.time + immuneTime;
        while(Time.time < endTime) {
            playerRenderer.sortingLayerID = SortingLayer.NameToID("Hidden");
            yield return new WaitForSeconds(0.2f);
            playerRenderer.sortingLayerID = SortingLayer.NameToID("Default");
            yield return new WaitForSeconds(0.2f);
        }
        playerRenderer.sortingLayerID = SortingLayer.NameToID("Default");
        collided = false;
    }
 
    protected override void onCollide(Collider2D col)
    {
        // =============
        // DASHING
        // =============
        if (dashing) {
            // Destroy enemy gameObject if collides with enemy
            if (col.tag == "Enemy") {
                col.gameObject.SetActive(false);
            }
        } 
        // =============
        // NOT DASHING
        // =============
        else {
            // Decrease player health upon enemy or damage object (e.g arrow) collision 
            if ((col.tag == "Enemy" || col.tag == "DamageObject") && !collided) {
                TakeDamage();
            }
            // Special condition for collision with spell
            if (col.tag == "Spell" && !collided) {
                // Get one of the fire child game object
                GameObject fire = col.gameObject.transform.GetChild(0).gameObject;
                SpriteRenderer sprite = fire.GetComponent<SpriteRenderer>();
                if (sprite.color.a == 1f) {
                    TakeDamage();
                }
            }
        }
    }
}
