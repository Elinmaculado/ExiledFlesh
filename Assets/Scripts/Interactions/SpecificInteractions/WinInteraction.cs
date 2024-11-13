using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinInteraction : MonoBehaviour, IEInteractable
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject winPanel;
    public void Interact(GameObject interactor)
    {
        winPanel.SetActive(true);
        player.SetActive(false);
    }
}
