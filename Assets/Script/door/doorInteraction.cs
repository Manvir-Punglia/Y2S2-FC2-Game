using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class doorInteraction : MonoBehaviour
{
    public LayerMask door;
    public LayerMask shopkeeper;
    public LayerMask interactable;
 
    public Transform camera;
    PlayerInput playerInput;
    public GameObject E;
    public GameObject ShopkeepText;
    public GameObject DoorText;

    private void Start()
    {
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        //E = GameObject.FindGameObjectWithTag("E");
    }
    // Update is called once per frame
    void Update()
    {
        
        Ray look = new Ray(camera.position, camera.forward);
        RaycastHit lookingAt;

        //if (Physics.Raycast(look, out lookingAt, 20, door))
        //{
        //    if (playerInput.actions["Interact"].triggered)
        //    {
        //        //Debug.LogError("Door");
        //        lookingAt.collider.gameObject.GetComponent<door>().open();
        //    }


        //}
        if (Physics.Raycast(look, out lookingAt, 30, shopkeeper))
        {
            ShopkeepText.SetActive(true);
            if (Physics.Raycast(look, out lookingAt, 20, shopkeeper))
            {
                if (playerInput.actions["Interact"].triggered)
                {
                    lookingAt.collider.gameObject.GetComponent<LittleMen>().talk();
                }


            }
        }
        else
        {
            //StartCoroutine(ShopTimer());
            ShopkeepText.SetActive(false);
        }
        //if (Physics.Raycast(look, out lookingAt, 20, shopkeeper))
        //{
        //    if (playerInput.actions["Interact"].triggered)
        //    {
        //        lookingAt.collider.gameObject.GetComponent<LittleMen>().talk();
        //    }


        //}
        if (Physics.Raycast(look, out lookingAt, 25, door))
        {
            DoorText.SetActive(true);
        }
        else
        {
            //StartCoroutine(DoorTimer());
            DoorText.SetActive(false);
        }

        if (Physics.Raycast(look, out lookingAt, 20, interactable))
        {
            E.SetActive(true);
            if (playerInput.actions["Interact"].triggered)
            {
                //Debug.LogError("Door");
                if(lookingAt.collider.gameObject.tag == "normal door")
                {
                    lookingAt.collider.gameObject.GetComponent<door>().open();
                }
            }
        }
        else
        {
            E.SetActive(false);
        }
    }
}
