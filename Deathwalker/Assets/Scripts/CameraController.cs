using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    public float boundX = 0.15f;
    public float boundY = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = player.position.x - transform.position.x;
        if (Mathf.Abs(deltaX) > boundX) {
            if (transform.position.x < player.position.x) {
                // enable smooth movement of the camera
                delta.x = deltaX - boundX;
            }
            else {

                delta.x = deltaX + boundX;
            }
        }

        float deltaY = player.position.y - transform.position.y;
        if (Mathf.Abs(deltaY) > boundY) {
            if (transform.position.y < player.position.y) {
                // enable smooth movement of the camera
                delta.y = deltaY - boundY;
            }
            else {

                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
