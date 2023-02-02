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


    public enum gate
    {
        ENTER,
        EXIT,
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
                        Destroy(this.gameObject);
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
