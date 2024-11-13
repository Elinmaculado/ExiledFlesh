using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundChanger : MonoBehaviour
{
    [SerializeField] Image transitionPanel;
    [SerializeField] Image backgroundImage;
    [SerializeField] List<Sprite> backgroundSprites;
    [SerializeField] float transitionDelayMin;
    [SerializeField] float transitionDelayMax;

    private void Start()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        float alpha = transitionPanel.color.a;
        while (alpha > 0)
        {
            alpha -= 0.05f;
            Color c = transitionPanel.color;
            c.a = alpha;
            transitionPanel.color = c;
            yield return new WaitForSeconds(0.1f);
            
        }
        
        yield return new WaitForSeconds(3.5f);

        alpha = transitionPanel.color.a;
        while (alpha < 1)
        {
            alpha += 0.05f;
            Color c = transitionPanel.color;
            c.a = alpha;
            transitionPanel.color = c;
            yield return new WaitForSeconds(0.1f);
        }

        int i = Random.Range(0, backgroundSprites.Count);
        backgroundImage.sprite = backgroundSprites[i];

        StartCoroutine(Transition());
    }

}
