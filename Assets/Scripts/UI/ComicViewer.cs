using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicViewer : MonoBehaviour
{
    public GameObject[] images;
    private PlayRandomSound sound;
    public float delay;
    public GameObject player;

    private void Start()
    {
        sound = GetComponent<PlayRandomSound>();
    }
    public void Activation()
    {
        StartCoroutine(ActivationSequence());
    }

    private IEnumerator ActivationSequence()
    {
        player.SetActive(false);
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(true);
            sound.PlaySoundOneShot();

            yield return new WaitForSeconds(delay);

            images[i].gameObject.SetActive(false);
        }
        player.SetActive(true);

    }
}
