using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : Movement
{
    public float cooldown = 3.0f;
    public float wizardSpeed = 0.4f;
    public float lastUsed;
    private GameObject player;
    public GameObject spellPrefab;
    private bool startCasting = false;
    private bool firstSpellCasted = false;
    private bool inRange = false;
    private Animator wizardAnimator;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(countdownToCast());
        wizardAnimator = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Get relative direction to player
        float x = player.transform.position.x - transform.position.x;
        float y = player.transform.position.y - transform.position.y;        
        // If player is in casting range and allowed to shoot
        if (startCasting && inRange) {
            // Update animation to activate idle
            wizardAnimator.SetFloat("xySpeed", 0);
            // If first spell is not yet casted
            if (!firstSpellCasted) {
                // Cast spell on player's current position
                Cast();
            } 
            else {
                // If cooldown is over, cast spell
                if (Time.time - lastUsed > cooldown) {
                    Cast(); 
                }
            }
        }
        // If player is not in casting range
        if (!inRange) {
            wizardAnimator.SetFloat("xySpeed", (Mathf.Abs(x) + Mathf.Abs(y)));
            UpdateMovement(new Vector3(x,y,0).normalized * wizardSpeed);
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
    // Wait 1.5 seconds before wizard casts spell when it is spawned
    IEnumerator countdownToCast() 
    {
        yield return new WaitForSeconds(1.5f);
        // Start casting after waiting for an initial 1.5 seconds
        startCasting = true;
    }
    IEnumerator startCastAnimation() 
    {
        // Let animation play for 0.3 seconds
        yield return new WaitForSeconds(0.3f);
        // Get player's position
        Vector3 playerPos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        // Spawn spell at player's position
        GameObject spell = Instantiate(spellPrefab, playerPos, Quaternion.identity);
        // Set casting to false
        wizardAnimator.SetBool("casting", false);
    }
    void Cast()
    {   
        wizardAnimator.SetBool("casting", true);
        firstSpellCasted = true;
        // Update last time arrow was shot
        lastUsed = Time.time;
        // Start cast animation and instantiate spell prefab
        StartCoroutine(startCastAnimation());
    }
}
