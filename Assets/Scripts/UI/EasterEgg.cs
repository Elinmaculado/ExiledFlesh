using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    int totalClicks = 19;
    int i = 0;
    [SerializeField] GameObject gusano;

    public void Click()
    {
        Debug.Log(i);
        i++;
        if (i == totalClicks)
        {
            StartCoroutine(GusanoMiqulester());
        }
    }

    IEnumerator GusanoMiqulester()
    {
        gusano.SetActive(true);
        i = 0;
        yield return new WaitForSeconds(5f);
        gusano.SetActive(false);
    }
}
