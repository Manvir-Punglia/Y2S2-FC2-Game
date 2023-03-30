using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LittleMen : MonoBehaviour
{
    //Animator animator;
    public bool _canInteract = true;
    PlayerManager player;
    //public GameObject option1;
    //public GameObject option2;
    //public GameObject option3;
    //public GameObject mainText;
    //public GameObject cameraFix;
    //public GameObject Npc;
    public gun Auto;
    public gun Pistol;

    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponentInParent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

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
            //animator.SetBool("Purchase", true);
            //_canInteract = false;

            PlayerPrefs.SetInt("health", player.GetHealth());
            PlayerPrefs.SetInt("money", player.GetMoney());
            PlayerPrefs.SetInt("AutoloadedAmmo", Auto.getCurrAmmo());
            PlayerPrefs.SetInt("AutostoredAmmo", Auto.getStoredAmmo());
            PlayerPrefs.SetInt("PistolloadedAmmo", Pistol.getCurrAmmo());
            PlayerPrefs.SetInt("PistolstoredAmmo", Pistol.getStoredAmmo());

            PlayerPrefs.Save();

            SceneManager.LoadScene("ShopInterface");

            //player.setNoLooking(false);

            //option1.SetActive(true);
            //option2.SetActive(true);
            //option3.SetActive(true);
            //mainText.SetActive(true);
            //cameraFix.SetActive(true);
            //Npc.SetActive(true);
            //Gun.SetActive(false);

            gameManager.enabled = true;
        }
    }
    //public void setCanInteract(bool newCaninteract)
    //{
    //    _canInteract = newCaninteract;
    //    //animator.SetBool("Purchase", false);
    //}
}
