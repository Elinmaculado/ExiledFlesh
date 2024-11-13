using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Key.KeyType doorKey;

    public Key.KeyType GetKeyType()
    {
        return doorKey; 
    }
}
