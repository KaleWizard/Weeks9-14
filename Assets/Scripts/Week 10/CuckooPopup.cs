using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CuckooPopup : MonoBehaviour
{
    public AnimationCurve height;

    Vector3 startPos;

    float timer = 0f;
    float timerLength = 1f;

    Coroutine popupAnimation = null;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPopup()
    {
        if (popupAnimation == null)
        {
            popupAnimation = StartCoroutine(PopupCoroutine());
        }
    }

    IEnumerator PopupCoroutine()
    {
        timer = 0f;
        while (timer < timerLength)
        {
            timer += Time.deltaTime;
            transform.position = startPos + new Vector3(0, height.Evaluate(timer / timerLength));

            yield return null;
        }
        popupAnimation = null;
    }
}
