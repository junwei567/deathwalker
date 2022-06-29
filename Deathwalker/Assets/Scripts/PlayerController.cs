using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponentInParent<BoxCollider2D>();
    }
    void FixedUpdate()
    {
        // a => -1, d -> 1
        float x = Input.GetAxisRaw("Horizontal");
        // s => -1, w -> 1
        float y = Input.GetAxisRaw("Vertical");

        // Reset moveDelta
        moveDelta = new Vector3(x,y,0);

        // Swap sprite direction
        if (moveDelta.x > 0) {
            transform.localScale = Vector3.one;
        } else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // A BoxCast is conceptually like dragging a box through the Scene in a particular direction. Any object making contact with the box can be detected and reported.
        // Make sure we can move in this direction by casting a box there first, if box returns null, we're free to move
        // y-direction box cast
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Box"));
        if (hit.collider == null) {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        // x-direction box cast
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Box"));
        if (hit.collider == null) {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
