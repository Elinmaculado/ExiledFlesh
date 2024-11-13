using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TPTransision : MonoBehaviour
{
    public Image panelImage;
    public float fadeSpeed;
    public float holdTime;

    private void Start()
    {
        Color color = panelImage.color;
        color.a = 0f;
        panelImage.color = color;
        panelImage.gameObject.SetActive(false);
    }

    public void StartFade()
    {
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut()
    {
        panelImage.gameObject.SetActive(true);

        // Fade in
        for (float i = 0; i < fadeSpeed; i += Time.deltaTime)
        {
            Color color = panelImage.color;
            color.a = i / fadeSpeed;
            panelImage.color = color;
            yield return null;
        }
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 1f);

        // Mantener el panel completamente oscuro por holdTime
        yield return new WaitForSeconds(holdTime);

        // Fade out
        for (float t = 0; t < fadeSpeed; t += Time.deltaTime)
        {
            Color color = panelImage.color;
            color.a = 1f - (t / fadeSpeed);
            panelImage.color = color;
            yield return null;
        }
        panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0f);
        panelImage.gameObject.SetActive(false);
    }
}
