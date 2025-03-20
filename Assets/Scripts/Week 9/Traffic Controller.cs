using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrafficController : MonoBehaviour
{
    public UnityEvent TrafficGo;
    public UnityEvent TrafficStop;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TrafficGo.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            TrafficStop.Invoke();
        }
    }
}
