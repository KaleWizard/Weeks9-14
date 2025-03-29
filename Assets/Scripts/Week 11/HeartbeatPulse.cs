using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatPulse : MonoBehaviour
{
    public AnimationCurve pulsePattern;

    TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        transform.position = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2f));
        trailRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 screenMin = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 screenMax = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        Vector2 pos = transform.position;
        pos.x += Time.deltaTime * 10f;

        pos.y = pulsePattern.Evaluate((pos.x - screenMin.x) / (screenMax.x - screenMin.x)) * 4f;

        transform.position = pos;

        if (pos.x > screenMax.x)
        {
            StartCoroutine(ResetPosition(screenMin));
        }
    }

    IEnumerator ResetPosition(Vector2 screenMin)
    {
        Vector2 pos = transform.position;
        pos.x = screenMin.x;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(0.05f);
        transform.position = pos;
        yield return null;
        trailRenderer.emitting = true;
    }
}
