using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextTrigger : MonoBehaviour
{
    public LayerMask shopkeeper;
    public LayerMask Door;

    public Transform camera;
    PlayerInput playerInput;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ray look = new Ray(camera.position, camera.forward);
        //RaycastHit lookingAt;

        //if (Physics.Raycast(look, out lookingAt, 30, shopkeeper))
        //{
        //    ShopkeepText.SetActive(true);
        //}
        //else
        //{
        //    //StartCoroutine(ShopTimer());
        //    ShopkeepText.SetActive(false);
        //}

        //if (Physics.Raycast(look, out lookingAt, 25, Door))
        //{
        //    DoorText.SetActive(true);
        //}
        //else
        //{
        //    //StartCoroutine(DoorTimer());
        //    DoorText.SetActive(false);
        //}
    }

    //private void TextTimeTest()
    //{
    //    new WaitForSeconds(1);
    //}

    //private IEnumerator DoorTimer()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    DoorText.SetActive(false);
    //}
    //private IEnumerator ShopTimer()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    ShopkeepText.SetActive(false);
    //}
}
