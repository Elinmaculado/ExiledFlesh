using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicViewer : MonoBehaviour
{
    public GameObject[] images;
    private PlayRandomSound sound;
    public float delay;
    public GameObject player;
    public AudioListener audioListener;

    private void Start()
    {
        sound = GetComponent<PlayRandomSound>();
        audioListener.enabled = false;
    }
    public void Activation()
    {
        StartCoroutine(ActivationSequence());
    }

    private IEnumerator ActivationSequence()
    {
        player.SetActive(false);
        audioListener.enabled = true;
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(true);
            sound.PlaySoundOneShot();

            yield return new WaitForSeconds(delay);

            images[i].gameObject.SetActive(false);
        }
        player.SetActive(true);
        audioListener.enabled = false;
    }
}
