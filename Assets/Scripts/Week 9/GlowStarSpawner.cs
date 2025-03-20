using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStarSpawner : MonoBehaviour
{
    float t = 0f;

    float timerEnd = 3f;

    public GlowStar star;

    public SwitchController switchController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        
        if (t > timerEnd)
        {
            GlowStar newStar = Instantiate(star);
            switchController.onToggle.AddListener(newStar.ToggleOn);
            Destroy(gameObject);
        }
    }
}
