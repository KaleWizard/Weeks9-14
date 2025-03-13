using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventDemo : MonoBehaviour
{
    public Sprite hoverSprite;
    Sprite defaultSprite;

    Image image;

    EventTrigger et;

    public float t = 0f;
    public float timerLength = 3f;

    [SerializeField] UnityEvent OnTimerFinished;
    [SerializeField] UnityEvent OnTimerReset;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        defaultSprite = image.sprite;
        et = gameObject.GetComponent<EventTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (t > timerLength)
        {
            OnTimerFinished.Invoke();
        } else
        {
            t += Time.deltaTime;
        }
    }

    public void PointerEnter()
    {
        image.sprite = hoverSprite;
        transform.localScale = Vector3.one * 1.2f;
    }

    public void PointerExit()
    {
        image.sprite = defaultSprite;
        transform.localScale = Vector3.one;
    }

    public void PointerDrag()
    {
        transform.position = Input.mousePosition;
    }

    public void PointerDown()
    {
        t = 0;
        OnTimerReset.Invoke();
    }
}
