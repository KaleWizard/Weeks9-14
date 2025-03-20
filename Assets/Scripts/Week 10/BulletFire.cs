using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletFire : MonoBehaviour
{
    public AnimationCurve height;

    public Transform start;
    public Transform end;

    Vector3 lastPosition;

    float timer = 0f;
    float timerLength = 0.5f;

    SpriteRenderer sr;

    public Button fireButton;

    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    public void FireBullet()
    {
        StartCoroutine(FireBulletCoroutine());
    }

    public IEnumerator FireBulletCoroutine()
    {
        timer = 0f;
        sr.enabled = true;
        transform.position = lastPosition = start.position;
        fireButton.interactable = false;

        while (timer < timerLength)
        {
            timer += Time.deltaTime;
            lastPosition = transform.position;
            transform.position = Vector3.Lerp(start.position, end.position, timer / timerLength);
            transform.position = transform.position + new Vector3(0, height.Evaluate(timer / timerLength), 0);
            UpdateRotation();
            yield return null;
        }

        sr.enabled = false;
    }

    void UpdateRotation()
    {
        transform.right = transform.position - lastPosition;
        lastPosition = transform.position;
    }
}
