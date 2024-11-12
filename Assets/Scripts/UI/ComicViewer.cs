using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicViewer : MonoBehaviour
{
    public GameObject[] images;
    public float delay;
    public PlayerController playerController;

    private void Start()
    {
        
    }
    public void Activation()
    {
        //playerController.enabled = false;
        playerController.PlayerHiddenState.exitPoint = playerController.transform.position;
        playerController.StateMachine.ChangeState(playerController.PlayerHiddenState);
        StartCoroutine(ActivationSequence());
        playerController.StateMachine.ChangeState(playerController.PlayerIdleState);
    }

    private IEnumerator ActivationSequence()
    {
        for (int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(true);

            yield return new WaitForSeconds(delay);

            images[i].gameObject.SetActive(false);
        }
        playerController.enabled = true;

    }
}
