using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarVectorLerp : MonoBehaviour
{
    public Vector2 velocity = Vector2.zero;

    public float speed = 5f;

    AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public void RunTurn()
    {
        StartCoroutine(MoveCar(velocity));
    }

    IEnumerator MoveCar(Vector2 direction)
    {
        if (direction != Vector2.zero)
            transform.up = direction;

        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + direction;

        float dist = 0f;
        float endDist = direction.magnitude;

        while (dist < endDist)
        {
            dist += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPos, endPos, movementCurve.Evaluate(dist / endDist));

            yield return null;
        }
    }
}
