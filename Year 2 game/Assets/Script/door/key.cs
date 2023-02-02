using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    public Transform spawn;
    public GameObject player;
    public GameObject keyText;
    public int keyCount = 0;

    public GameObject hubMusic;
    public GameObject levelMusic;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "key")
        {
            Destroy(collision.gameObject);
            keyCount++;
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = spawn.position;
            player.GetComponent<CharacterController>().enabled = true;
            keyText.SetActive(false);
            hubMusic.SetActive(true);
            levelMusic.SetActive(false);
        }
    }

    public int getKey()
    {
        return keyCount;
    }
}
