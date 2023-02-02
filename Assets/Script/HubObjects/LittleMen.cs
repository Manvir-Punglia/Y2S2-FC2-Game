using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleMen : MonoBehaviour
{
    Animator animator;
    public bool _canInteract = true;
    PlayerController player;
    public GameObject option1;
    public GameObject option2;
    public GameObject option3;
    public GameObject mainText;
    public GameObject cameraFix;
    public GameObject Npc;
    public GameObject Gun;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        gameManager = GameObject.FindGameObjectWithTag("Finish").GetComponent<GameManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void talk()
    {
        if (_canInteract)
        {
            animator.SetBool("Purchase", true);
            _canInteract = false;

            player.setNoLooking(false);

            option1.SetActive(true);
            option2.SetActive(true);
            option3.SetActive(true);
            mainText.SetActive(true);
            cameraFix.SetActive(true);
            Npc.SetActive(true);
            Gun.SetActive(false);

            gameManager.enabled = true;
        }
    }
    public void setCanInteract(bool newCaninteract)
    {
        _canInteract = newCaninteract;
        animator.SetBool("Purchase", false);
    }
}
