using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] List<string> lines;
    public float textSpeed;

    private int index;
    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0) && dialogueBox.activeInHierarchy){
            if(textDisplay.text == lines[index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textDisplay.text = lines[index];
            }
       } 
    }

    void StartDialogue(){
        index = 0;
        textDisplay.text = string.Empty;
        dialogueBox.SetActive(true);
        StartCoroutine(Typeline());
    }

    IEnumerator Typeline(){
            yield   return new WaitForSeconds(textSpeed);
        foreach(char c in lines[index].ToCharArray()){
            textDisplay.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        index++;
        if(index < lines.Count){
            textDisplay.text = string.Empty;
            StartCoroutine(Typeline());
        }
        else{
            dialogueBox.SetActive(false);
        }
    }

    public bool CanSetDialogues(List<string> newLines){
        if(index>=lines.Count){
            lines = newLines;
            StartDialogue();
            return true;
        }
        return false;
    }


}
