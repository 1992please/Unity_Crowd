using UnityEngine;
using System.Collections;

public class PlayerSounds : MonoBehaviour
{
    public AudioClip[] sounds;
    // Use this for initialization
    public void playSound(int soundNo)
    {
        GetComponent<AudioSource>().clip = sounds[soundNo];
        GetComponent<AudioSource>().Play();
    }
}
