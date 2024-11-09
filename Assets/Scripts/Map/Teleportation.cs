using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Transform floor1;
    [SerializeField] private Transform floor2;
    [SerializeField] private Transform floor3;
    [SerializeField] private Transform floor4;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void TeleportFloor1()
    {
        Invoke("Floor1", 0.5f);
    }

    public void Floor1()
    {
        player.transform.position = floor1.transform.position;
    }

    public void TeleportFloor2()
    {
        Invoke("Floor2", 0.5f);
    }

    public void Floor2()
    {
        player.transform.position = floor2.transform.position;
    }

    public void TeleportFloor3()
    {
        Invoke("Floor3", 0.5f);
    }

    public void Floor3()
    {
        player.transform.position = floor3.transform.position;
    }

    public void TeleportFloor4()
    {
        Invoke("Floor4", 0.5f);
    }

    public void Floor4()
    {
        player.transform.position = floor4.transform.position;
    }
}
