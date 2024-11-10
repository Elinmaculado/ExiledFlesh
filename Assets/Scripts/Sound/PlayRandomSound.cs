using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips;

    public void PlaySoundOneShot(){
        int randomIndex = Random.Range(0,audioClips.Count);
        float randomPitch = Random.Range(0.8f,1.2f);
        audioSource.pitch = randomPitch;
        audioSource.PlayOneShot(audioClips[randomIndex]);
    }
}
