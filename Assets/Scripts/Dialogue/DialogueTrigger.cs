using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dialogueSystem;
    int index = 0;
    public List<DialogueList> dialogues;

    [System.Serializable]
    public class DialogueList{
        public List<string> dialogues;
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")){
            return;
        }
        if(!dialogueSystem.CanSetDialogues(dialogues[index].dialogues)){
            return;
        }
        if(other.gameObject.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.StateMachine.ChangeState(player.PlayerInvincibleState);
        }
        index++;
        if(index==dialogues.Count){
            gameObject.SetActive(false);
        }
    }
}
