using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Sound : MonoBehaviour
{
    public AudioSource src;
    public List<AudioClip> SoundAudioClips;

    public void PlaySound(int index)
    {
        src.PlayOneShot(SoundAudioClips[index]);
    }
}
