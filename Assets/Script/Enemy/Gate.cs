using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy_movement;

public class Gate : MonoBehaviour
{
    [SerializeField] GameObject spawn;
    [SerializeField] GameObject destination;
    public gate type = gate.ENTER;

    public GameObject hubMusic;
    public GameObject levelMusic;

    public Collider[] collider;


    public enum gate
    {
        ENTER,
        EXIT,
    }
    void Update()
    {
        if (spawn.GetComponent<Spawn>().GetTrigger())
        {
            for (int i = 0; i < collider.Length; i++)
            {
                collider[i].enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case gate.ENTER:
                    {
                        spawn.GetComponent<Spawn>().SetCanSpawn(true);
                    }
                    break;
                case gate.EXIT:
                    {
                        other.GetComponent<CharacterController>().enabled = false;
                        other.transform.position = destination.transform.position;
                        other.GetComponent<CharacterController>().enabled = true;
                        hubMusic.SetActive(false);
                        levelMusic.SetActive(true);
                    }
                    break;
            }
        }
    }
}
