using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHolder : MonoBehaviour
{
    // we declare the list to hold the keys
    [SerializeField] private List<Key.KeyType> keysList;

    private void Awake()
    {
        // We create the list
        keysList = new List<Key.KeyType>();
    }

    // Adding a key to the list
    public void AddKey(Key.KeyType keyType)
    {
        Debug.Log("Added key: " + keyType);
        keysList.Add(keyType); 
    }

    // This method is to remove the key from the inventory, but we wont use it for this game
    /*
    public void RemoveKey(Key.KeyType keyType)
    {
        keysList.Remove(keyType);
    }
    */

    public bool ContainsKey(Key.KeyType keyType)
    {
        return keysList.Contains(keyType);
    }


    // When colliding with a key, we check the type of key and add it to the list
    private void OnTriggerEnter(Collider collider)
    {
        // Checks if the object with the collider contains the Key component
        Key key = collider.GetComponent<Key>();
        if (key != null)
        {
            // If it does contain it, we grab the key
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }

        // Checks if the object with the collider is a door that uses a key
        KeyDoor keyDoor = collider.GetComponent<KeyDoor>();
        if (keyDoor != null)
        {
            // Checks if we have the key corresponding to the door
            if (ContainsKey(keyDoor.GetKeyType()))
            {
                //Here we can use the method to remove the key, if we did that in this game... but we dont.
                //RemoveKey(keyDoor.GetKeyType());
                keyDoor.OpenDoor();
            }
        }
    }
}
