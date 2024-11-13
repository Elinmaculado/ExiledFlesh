using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
using System.Reflection;
using System.Linq;

public class Dialogue : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] List<string> lines;
    public float textSpeed;

    private int index;
    [SerializeField] PlayRandomSound sound;
    [SerializeField] PlayerController playerController;
    

    private void Start() {
        dialogueBox.SetActive(false);
        textDisplay.text = string.Empty;
    }
    void Update()
    {
        if (PlayNext())
        {
            if (textDisplay.text == lines[index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textDisplay.text = lines[index];
            }
        }
    }

    private bool PlayNext()
    {
        return (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && dialogueBox.activeInHierarchy;
    }

    void StartDialogue(){
        StopAllCoroutines();
        index = 0;
        textDisplay.text = string.Empty;
        dialogueBox.SetActive(true);
        StartCoroutine(Typeline());
    }

    IEnumerator Typeline(){
            yield   return new WaitForSeconds(textSpeed);
        foreach(char c in lines[index].ToCharArray()){
            textDisplay.text += c;
            sound.PlaySoundOneShot();
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
            playerController.StateMachine.ChangeState(playerController.PlayerIdleState);
            textDisplay.text = string.Empty;
            index = 0;
            dialogueBox.SetActive(false);
        }
    }

    public bool CanSetDialogues(List<string> newLines){
        if(index==0){
            lines = newLines;
            StartDialogue();
            return true;
        }
        return false;
    }

    public void AppedDialogs(List<string> dialogs){
        lines.AddRange(dialogs);
        StopAllCoroutines();
        dialogueBox.SetActive(true);
        StartCoroutine(Typeline());
    }

}
