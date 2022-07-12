using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 crosshairPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = crosshairPos;
    }
}
