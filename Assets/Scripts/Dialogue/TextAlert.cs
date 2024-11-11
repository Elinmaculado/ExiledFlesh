using System.Collections;
using TMPro;
using UnityEngine;

public class TextAlert : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI alertDisplay;
    public static TextAlert instance;
    public float textSpeed;
    [SerializeField] PlayRandomSound sound;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Debug.Log("More tha one text alert");
            Destroy(this);
        }
    }

    public void Alert(string message, float duration){
        StopAllCoroutines();
        alertDisplay.text = string.Empty;
        StartCoroutine(Typeline(message, duration));
    }

    public void Alert(string message){
        Alert(message,2f);
    }

    IEnumerator ClearDelay(float duration){
        yield return new WaitForSeconds(duration);
        alertDisplay.text = string.Empty;
    }

    IEnumerator Typeline(string text, float duration){
        yield   return new WaitForSeconds(textSpeed);
        foreach(char c in text.ToCharArray()){
            alertDisplay.text += c;
            sound.PlaySoundOneShot();
            yield return new WaitForSeconds(textSpeed);
        }
        StartCoroutine(ClearDelay(duration));
    }
}
