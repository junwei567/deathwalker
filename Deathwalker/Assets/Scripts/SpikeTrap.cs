using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpikeTrap : MonoBehaviour
{
//   PlayerController dn = gameObject.GetComponent<PlayerController>();


  //   [SerializeField] private float damage;

  [Header("Spiketrap Timers")]

//   spikes/damage after x seconds
  [SerializeField] private float activationDelay;
//   spikes/damage stay for y seconds
  [SerializeField] private float activeTime;

  [SerializeField] public IntegerSO playerHealthSO;

  private Animator anim;

  private SpriteRenderer spriteRend;

  private bool triggered; // when trap is triggered
  private bool active; // when trap is active and hurt player

  private void Awake()
  {
    anim = GetComponent<Animator>();
    spriteRend = GetComponent<SpriteRenderer>();
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Player")
    {
      if (!triggered)
      {
        StartCoroutine(ActivateSpiketrap());
        // trigger the spiketrap
      }
      if (active)
        collision.GetComponent<PlayerController>().TakeDamage();
        // playerHealthSO.Value--;
    }
  }
  private IEnumerator ActivateSpiketrap()
  {
    triggered = true;
    // spriteRend.color = Color.red;

    //Wait for delay, activate trap, turn on animation, return color back to normal
    yield return new WaitForSeconds(activationDelay);
    // spriteRend.color = Color.blue;
    active = true;
    anim.SetBool("activated", true);


    //Wait until X seconds, deactivate trap and reset all variables and animator
    yield return new WaitForSeconds(activeTime);
    
    active = false;
    triggered = false;
    anim.SetBool("activated", false);
    // to deactivate the trap
  }
}
