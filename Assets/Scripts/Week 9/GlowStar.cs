using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStar : MonoBehaviour
{
    public AnimationCurve sizeCurve;
    public AnimationCurve sizeWobble;

    public float tCurve = 0f;
    public float tWobble = 0f;

    public bool isOn = false;

    public Color startColor = Color.white;
    public Color endColor = Color.white;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        ToggleOn(false);
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.Lerp(startColor, endColor, tCurve);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOn) 
        {
            tCurve -= Time.deltaTime;
        } else
        {
            tCurve += Time.deltaTime;
            tWobble = (tWobble + Time.deltaTime) % 2f;
        }

        tCurve = Mathf.Clamp(tCurve, 0f, 1f);

        transform.localScale = Vector3.one * (sizeCurve.Evaluate(tCurve) + sizeWobble.Evaluate(tWobble));

        sr.color = Color.Lerp(startColor, endColor, tCurve);
    }

    public void ToggleOn(bool toggle)
    {
        isOn = toggle;
    }
}
