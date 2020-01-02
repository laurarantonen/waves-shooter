using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay = 4;

    void Start()
    {
        // Plays an Audio Clip after 4 seconds
        audioSource.PlayDelayed(delay);
    }

}
