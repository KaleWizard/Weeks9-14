using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridClickMovement : MonoBehaviour
{
    public List<Sprite> invalidSprites;

    public Tilemap ground;

    Vector3Int target = Vector3Int.zero;
    Vector3 targetPosition;

    Vector3 direction;
    float walkingSpeed = 5f;
    float speed = 0f;

    bool targetNotReached = false;

    public Animator anim;

    public List<AudioClip> footsteps;

    public AudioSource audioSource;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateTarget();
        }
        if (targetNotReached)
        {
            Move();
        }
        anim.SetFloat("Speed", speed);
        sr.flipX = direction.x >= 0;
    }

    void UpdateTarget()
    {
        Vector3Int newTarget = ground.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (ValidCell(newTarget))
        {
            target = newTarget;
            targetNotReached = true;
            targetPosition = ground.CellToWorld(newTarget) + (Vector3) Vector2.one * 0.5f;
            direction = (targetPosition - transform.position).normalized;
            speed = walkingSpeed;
        }
    }

    bool ValidCell(Vector3Int cell)
    {
        return !invalidSprites.Contains((ground.GetTile(cell) as Tile).sprite);
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetNotReached = false;
            speed = 0f;
            return;
        }
        transform.position += direction * speed * Time.deltaTime;
    }

    void PlayFootstep()
    {
        if (!ValidCell(ground.WorldToCell(transform.position)))
        {
            audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Count)]);
        }
    }
}
