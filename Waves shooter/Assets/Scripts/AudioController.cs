using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    AudioSource levelMusic;
    AudioSource bossMusic;
    public int startTime = 4;
    public int firstClipLength = 82;

    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        levelMusic = audios[0];
        bossMusic = audios[1];

        // Plays an Audio Clip after 4 seconds
        levelMusic.PlayScheduled(AudioSettings.dspTime + startTime);
        bossMusic.PlayScheduled(AudioSettings.dspTime + startTime + firstClipLength);
    }

}
