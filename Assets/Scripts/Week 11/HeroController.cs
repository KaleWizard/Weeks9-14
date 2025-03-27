using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    public Animator animator;

    Vector3 gravity = Vector2.down * 4;

    public Vector3 velocity = Vector3.zero;

    float speed = 30f;

    SpriteRenderer sr;

    public List<AudioClip> footsteps;

    public AudioSource audio;

    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    { 
        velocity += Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        velocity.x *= 0.95f;

        velocity += gravity * Time.deltaTime;

        Vector2 screenCorner = Camera.main.ScreenToWorldPoint(Vector2.zero);
        if (transform.position.y < screenCorner.y)
        {
            transform.position = new Vector2(transform.position.x, screenCorner.y);
            velocity.y = 0f;
            if (!animator.GetBool("Grounded"))
            {
                animator.SetBool("Grounded", true);
                particles.transform.position = transform.position;
                particles.Emit(5);
                //for (int i = 0; i < 5; i++)
                //{
                //    particles.Emit(transform.position, Vector3.up, 0.3f, 2f, Color.white);
                //}
            }
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && animator.GetBool("Grounded"))
        {
            animator.SetTrigger("Jump");
            velocity.y = 3f;
            animator.SetBool("Grounded", false);
        }

        animator.SetFloat("AirSpeedY", velocity.y);
        if (Mathf.Abs(velocity.x) >= 0.1f)
        {
            animator.SetInteger("AnimState", 1);
        } else
        {
            animator.SetInteger("AnimState", 0);
        }

        sr.flipX = velocity.x < 0;

        transform.position += velocity * Time.deltaTime;
    }

    public void PlayFootstep()
    {
        audio.PlayOneShot(footsteps[Random.Range(0, footsteps.Count)]);
    }
}
