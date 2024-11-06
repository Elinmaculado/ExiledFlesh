using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType key;
    [SerializeField] private Dialogue dialogueSystem;
    [SerializeField] private string collectMessage;

    public enum KeyType
    {
        Blocked,
        Arm,
        Eye,
        Parasite,
        None
    }

    public KeyType GetKeyType()
    {
        return key;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent(out KeyHolder keyHolder)){
            keyHolder.AddKey(key);
            TextAlert.instance.Alert(collectMessage,2);
            Destroy(gameObject);
        }
    }

    
}
