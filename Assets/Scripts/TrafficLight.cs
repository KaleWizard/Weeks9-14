using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public SpriteRenderer redLight;
    public SpriteRenderer greenLight;

    Color onRed;
    Color onGreen;

    Color offRed;
    Color offGreen;

    // Start is called before the first frame update
    void Start()
    {
        onRed = redLight.color;
        offRed = Color.Lerp(Color.black, onRed, 0.4f);

        onGreen = greenLight.color;
        offGreen = Color.Lerp(Color.black, onGreen, 0.4f);

        TrafficStop();
    }

    public void TrafficGo()
    {
        redLight.color = offRed;
        greenLight.color = onGreen;
    }

    public void TrafficStop()
    {
        redLight.color = onRed;
        greenLight.color = offGreen;
    }
}
