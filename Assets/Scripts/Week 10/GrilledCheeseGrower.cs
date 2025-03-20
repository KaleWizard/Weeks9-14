using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrilledCheeseGrower : MonoBehaviour
{
    public Transform bottomBread;
    public Transform cheese;
    public Transform topBread;


    public AnimationCurve growth;

    float timer = 0f;
    float timerLength = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        bottomBread.localScale = cheese.localScale = topBread.localScale = Vector3.zero;
        StartCoroutine(GrowGrilledCheese());
    }

    IEnumerator GrowGrilledCheese()
    {
        yield return StartCoroutine(GrowPiece(bottomBread.transform));
        yield return StartCoroutine(GrowPiece(cheese.transform));
        yield return StartCoroutine(GrowPiece(topBread.transform));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GrowPiece(Transform t)
    {
        timer = 0f;

        while (timer < timerLength)
        {
            timer += Time.deltaTime;
            t.localScale = Vector3.one * growth.Evaluate(timer / timerLength);

            yield return null;
        }
    }
}
