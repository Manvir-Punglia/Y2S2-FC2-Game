using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetLevels : MonoBehaviour
{
    private Renderer renderer;
    public Color newColor = new Color(0, 1, 0, 1); // Green color
    public Color newColor2 = new Color(1, 0, 1, 0); // Green color


    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("dungeon1", 0);
            PlayerPrefs.SetInt("dungeon2", 0);
            PlayerPrefs.SetInt("dungeon3", 0);
            PlayerPrefs.SetInt("dungeon4", 0);
            renderer.material.color = newColor;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            renderer.material.color = newColor2;
        }

    }
}
