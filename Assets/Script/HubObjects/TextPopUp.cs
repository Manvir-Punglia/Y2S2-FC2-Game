using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{

    public GameObject textCanvas;
    private bool textCanvasActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !textCanvasActivated)
        {
            textCanvas.SetActive(true);
            textCanvasActivated = true;

            // Remove the text canvas after 5 seconds
            Invoke("DeactivateTextCanvas", 5f);
        }
    }

    private void DeactivateTextCanvas()
    {
        textCanvas.SetActive(false);
    }
}
