using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private SpriteRenderer fireSprite;
    private Animator fireAnimator;

    // Start is called before the first frame update
    void Start()
    {
        fireSprite = GetComponent<SpriteRenderer>();
        fireAnimator = GetComponent<Animator>();
        // Change fire alpha channel to 0.5
        fireSprite.color = new Color(1f,1f,1f,0.5f);
        // Change fire scale to 0.5
        this.gameObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        StartCoroutine(StartFire(fireSprite));
    }

    IEnumerator StartFire(SpriteRenderer fireSprite)
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.transform.localScale = new Vector3(1f,1f,1f);
        // Stop blinking animation
        fireAnimator.SetBool("castTimeOver", true);
        // Set alpha channels back to default
        fireSprite.color = new Color(1f,1f,1f,1f);
        StartCoroutine(EndFire());
    }
    IEnumerator EndFire()
    {
        yield return new WaitForSeconds(2.0f);
        // Start the end fire animation
        fireAnimator.SetBool("spellOver", true);
        // Destroy fire after 1.1 seconds of fire end animation
        // Destroy(this.gameObject, 1.1f);
        Destroy(transform.parent.gameObject, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
