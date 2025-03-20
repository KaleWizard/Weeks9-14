using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficTank : MonoBehaviour
{
    bool isGoing = false;

    public float speed = 3f;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGoing) return;
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        Vector3 boundsExtents = sr.sprite.bounds.extents;


        if (transform.position.x - boundsExtents.x < Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x)
        {
            speed *= -1;
            sr.flipX = false;
        } else if (transform.position.x + boundsExtents.x > Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x)
        {
            speed *= -1;
            sr.flipX = true;
        }
    }

    public void Go() 
    {
        isGoing = true;
    }

    public void Stop()
    {
        isGoing = false;
    }
}
