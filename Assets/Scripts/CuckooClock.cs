using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuckooClock : MonoBehaviour
{
    public AudioClip cuckooClockSound;

    AudioSource cuckooClockSource;

    private void Start()
    {
        cuckooClockSource = GetComponent<AudioSource>();
    }

    public void PlayCuckoo()
    {
        cuckooClockSource.PlayOneShot(cuckooClockSound);
    }
}
