using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchController : MonoBehaviour
{
    public Transform switchRotater;

    bool isOn;

    float rotationAmount = 30f;

    public UnityEvent<bool> onToggle;

    // Start is called before the first frame update
    void Start()
    {
        onToggle = new UnityEvent<bool>();
        TurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOn)
            {
                TurnOff();
            } else
            {
                TurnOn();
            }
        }
    }

    public void TurnOn()
    {
        isOn = true;
        switchRotater.eulerAngles = new Vector3(0, 0, rotationAmount);
        onToggle.Invoke(true);
    }

    public void TurnOff()
    {
        isOn = false;
        switchRotater.eulerAngles = new Vector3(0, 0, -1 * rotationAmount);
        onToggle.Invoke(false);
    }
}
