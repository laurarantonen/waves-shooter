using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixerMenu;
    public AudioMixer audioMixerGame;

    public void SetMenuVolume(float volume)
    {
        audioMixerMenu.SetFloat("volume", volume);
    }

    public void SetGameVolume(float volume)
    {
        audioMixerGame.SetFloat("volume", volume);
    }
}
