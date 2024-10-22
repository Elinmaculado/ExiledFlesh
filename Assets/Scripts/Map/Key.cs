using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType key;

    public enum KeyType
    {
        Arm,
        Eye,
        Parasite
    }

    public KeyType GetKeyType()
    {
        return key;
    }
}
