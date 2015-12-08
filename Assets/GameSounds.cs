using UnityEngine;
using System.Collections;

public class GameSounds : MonoBehaviour {

    public AudioClip[] sounds;
    public void playSound(int soundNo)
    {
        GetComponent<AudioSource>().clip = sounds[soundNo];
        GetComponent<AudioSource>().Play();
    }
}
