using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    private float x = 1.0f;
    private SpriteRenderer sprite;
    public GameObject blood;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (x>0){
            x-=0.0005f;
            sprite.color = new Color(1,1,1,x);
        }
        else{
            Destroy(blood);
        }
    }
}
