using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType key;

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
            Destroy(gameObject);
        }
    }
}
