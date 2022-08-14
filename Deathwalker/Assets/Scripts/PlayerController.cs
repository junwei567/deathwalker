using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Movement
{
    public float dashSpeed = 2.0f;
    private bool dashing = false;
    public float dashTime = 0.3f;
    public float lastUsed;
    public float cooldown = 1.0f;
    public float threshold = 0.01f;
    public float immuneTime = 2.0f;
    public bool doubleDash = false;
    // private bool dead;
    private int dashCount = 0;
    private Animator playerAnimator;
    private AudioSource playerAudio;
    private SpriteRenderer playerRenderer;
    private SkeletonController skeletonController;
    private bool killeffect_ready;

    private cam_shake cam_shake;
    public GameObject deathEffect;
    public GameObject bloodPool1;
    public GameObject bloodPool2;
    public GameObject bloodPool3;
    public GameObject bloodPool4;

    [SerializeField]
    public IntegerSO playerHealthSO;
 
    protected override void Start()
    {
        base.Start();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerRenderer = GetComponent<SpriteRenderer>();
        cam_shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<cam_shake>();
        dashCount = 0;
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
            if (doubleDash && !dashing && dashCount < 2) {
                // Add to dash counter
                dashCount++;
                Debug.Log(dashCount);
                dashing = true;
                playerAnimator.SetBool("dashing", dashing);
                lastUsed = Time.time;
                // to ensure cam shake once per dash
                killeffect_ready = true;

                // Add cooldown since player already dashed twice
                // if (dashCount == 2) {
                //    lastUsed = Time.time;
                // }
            } 
            // NO DOUBLE DASH
            if (!doubleDash) {
                // If skill is NOT on cooldown
                if (Time.time - lastUsed > cooldown) {
                    // to ensure cam shake once per dash
                    killeffect_ready = true;
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
        if (dashCount == 2) {
            if (Time.time - lastUsed > cooldown) {
                dashCount = 0;
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
    public void activateLongDash()
    {
        Debug.Log("long dash activated");
        dashSpeed = 3.0f;
    }
    public void activateDoubleDash()
    {
        Debug.Log("double dash activated");
        doubleDash = true;
    }
    void PlayDashSound()
    {
        if (!playerAudio.isPlaying){
            playerAudio.PlayOneShot(playerAudio.clip);
        }
    }
    public void TakeDamage()
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

    private void enemyKillEffect(Collider2D col){
        int rand = Random.Range(0, 4);
        if (rand == 0){
            Instantiate(bloodPool1,col.gameObject.transform.position, Quaternion.identity);
        } else if(rand == 1){
            Instantiate(bloodPool2,col.gameObject.transform.position, Quaternion.identity);
        } else if(rand == 2){
            Instantiate(bloodPool3,col.gameObject.transform.position, Quaternion.identity);
        } else if(rand == 3){
            Instantiate(bloodPool4,col.gameObject.transform.position, Quaternion.identity);
        }
        Instantiate(deathEffect,col.gameObject.transform.position, Quaternion.identity);
        cam_shake.playerCamShake();

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
                if (killeffect_ready == true){
                    enemyKillEffect(col);
                    killeffect_ready = false;
                }
            }
            else if (col.tag == "Skeleton") {
                Debug.Log("kill");
                // Debug.Log(col.gameObject.GetComponent(dead));
                skeletonController = col.gameObject.GetComponent<SkeletonController>();
                skeletonController.dying = true;
                if (killeffect_ready == true){
                    enemyKillEffect(col);
                    killeffect_ready = false;
                }
            }
        } 
        // =============
        // NOT DASHING
        // =============
        else {
            // Decrease player health upon enemy or damage object (e.g arrow) collision 
            if ((col.tag == "Enemy" || col.tag == "DamageObject" || col.tag == "Skeleton") && !collided) {
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
