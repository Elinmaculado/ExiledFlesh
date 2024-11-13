using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType key;
    [SerializeField] private Dialogue dialogueSystem;
    [SerializeField] private string collectMessage;
    [SerializeField] UnityEvent onCollect;

    public enum KeyType
    {
        Blocked,
        Arm,
        Eye,
        Parasite,
        None,
        SlidingPuzzle,
        RotatingPuzzle,
        Path,
        FaithElevator
    }

    public KeyType GetKeyType()
    {
        return key;
    }

    private void OnTriggerEnter(Collider other)
    {
        AddKey(other);
    }

    public void AddKey(Collider other)
    {
        if (other.TryGetComponent(out KeyHolder keyHolder))
        {
            keyHolder.AddKey(key);
            onCollect.Invoke();
            TextAlert.instance.Alert(collectMessage, 2);
            Destroy(gameObject);
        }
    }

    public void AddKey(GameObject other)
    {
        if (other.TryGetComponent(out KeyHolder keyHolder))
        {
            keyHolder.AddKey(key);
            onCollect.Invoke();
            TextAlert.instance.Alert(collectMessage, 2);
            Destroy(gameObject);
        }
    }

}
