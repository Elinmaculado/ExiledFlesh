using UnityEngine;
using UnityEngine.UI;

public class TPCanvasController : MonoBehaviour
{
    [SerializeField] private KeyHolder keyHolder;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4;
    void Start()
    {
        // Activamos el primer botón por defecto
        button1.interactable = true;

        // Los otros botones están desactivados inicialmente
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        button2.interactable = keyHolder.ContainsKey(Key.KeyType.Arm);
        button3.interactable = keyHolder.ContainsKey(Key.KeyType.Eye);
        button4.interactable = keyHolder.ContainsKey(Key.KeyType.Parasite);
    }
}
