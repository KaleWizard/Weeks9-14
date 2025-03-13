using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    Vector2 direction;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.insideUnitCircle.normalized;
        speed = Random.Range(3f, 15f);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos += direction * speed * Time.deltaTime;
        transform.position = pos;
    }
}
