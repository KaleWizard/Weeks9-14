using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerEventGym : MonoBehaviour
{
    public Sprite hoverSprite;
    Sprite defaultSprite;

    Image image;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        defaultSprite = image.sprite;
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

    public void PointerDown()
    {
        Instantiate(prefab);
    }
}
