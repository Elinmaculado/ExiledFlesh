using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPActivattion : MonoBehaviour
{
    public GameObject TeleporationPanel;
    public GameObject TeleporationCanvas;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleporationPanel.SetActive(true);
            TeleporationCanvas.GetComponent<TPCanvasController>().CheckKeys();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleporationPanel.SetActive(false);
        }
    }
}
