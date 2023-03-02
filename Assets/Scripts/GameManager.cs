using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StoryBlock
{
    [TextArea]
    public string story;
    [Header("Button 1")]
    public string option1Text;
    public int option1BlockId;
    [Header("Button 2")]
    public string option2Text;
    public int option2BlockId;
    [Header("Button 3")]
    public string option3Text;
    public int option3BlockId;

    

    public StoryBlock(string story, string option1Text = "", string option2Text = "", string option3Text = "",
        int option1BlockId = -1, int option2BlockId = -1, int option3BlockId = -1)
    {

        this.story = story;
        this.option1Text = option1Text;
        this.option2Text = option2Text;
        this.option3Text = option3Text;
        this.option1BlockId = option1BlockId;
        this.option2BlockId = option2BlockId;
        this.option3BlockId = option3BlockId;

    }
}

public class GameManager : MonoBehaviour
{



    [Header("UI elements")]
    public Text mainText;
    public Button option1;
    public Button option2;
    public Button option3;
    //PlayerManager player;
    //LittleMen shopkeep;

    public GameObject op1;
    public GameObject op2;
    public GameObject op3;
    public GameObject mt;
    public GameObject cameraFix;
    public GameObject Npc;
    //public GameObject AmmoSpawn;
    //public GameObject HealthSpawn;
    //public GameObject Ammo;
    //public GameObject Health;
    //public gun Gun;
    int ammoPrice = 30;
    int healthPrice = 60;

    int playerAmmo;
    int playerHealth;
    int playerCoins;

    public StoryBlock[] storyBlocks =
    {
        new StoryBlock("Page 1", "Option 1", "Option 2", "Option3", 1, 2),
        new StoryBlock("Page 2", "Option 1", "Option 2", "Option3", 3, 2),
        new StoryBlock("Page 3", "Option 1", "Option 2", "Option3", 5, 7),
        new StoryBlock("Page 4", "Option 1", "Option 2", "Option3", 5, 4),
        new StoryBlock("Page 5", "Option 1", "Option 2", "Option3", 5, 2),
        new StoryBlock("Page 6", "Option 1", "Option 2", "Option3", 6, 7),
        new StoryBlock("Page 7"),
        new StoryBlock("Page 8"),
    };


    StoryBlock currentBlock;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        //shopkeep = GameObject.FindGameObjectWithTag("ShopKeeper").GetComponent<LittleMen>();
        DisplayBlock(storyBlocks[0]);
        playerCoins = PlayerPrefs.GetInt("money");
        playerAmmo = PlayerPrefs.GetInt("storedAmmo");
        playerHealth = PlayerPrefs.GetInt("health");
        Cursor.lockState = CursorLockMode.None;
    }

    void DisplayBlock(StoryBlock block)
    {
        mainText.text = block.story;
        option1.GetComponentInChildren<Text>().text = block.option1Text;
        option2.GetComponentInChildren<Text>().text = block.option2Text;
        option3.GetComponentInChildren<Text>().text = block.option3Text;

        currentBlock = block;
    }

    public void Button1Clicked()
    {
        StartCoroutine(DelayDisplayBlock(storyBlocks[currentBlock.option1BlockId]));
        //DisplayBlock(storyBlocks[currentBlock.option1BlockId]);

        switch (currentBlock.option1BlockId)
        {
            case 8:
                if (playerCoins >= healthPrice)
                {
                    if (playerHealth < 3)
                    {
                        playerCoins = (playerCoins - healthPrice);
                        playerHealth = (playerHealth + 1);
                    }
                }
                break;
            case 9:

                if(playerCoins >= ammoPrice)
                {
                    playerCoins = (playerCoins - ammoPrice);
                    playerAmmo = (playerAmmo + 15);
                }
                
                break;
        }
    }

    public void Button2Clicked()
    {
        StartCoroutine(DelayDisplayBlock(storyBlocks[currentBlock.option2BlockId]));
        //DisplayBlock(storyBlocks[currentBlock.option2BlockId]);
    }

    public void Button3Clicked()
    {
        StartCoroutine(DelayDisplayBlock(storyBlocks[currentBlock.option3BlockId]));
        //DisplayBlock(storyBlocks[currentBlock.option2BlockId]);
        //player.setNoLooking(true);
        //shopkeep.setCanInteract(true);
        //Gun.SetActive(true);

        //op1.SetActive(false);
        //op2.SetActive(false);
        //op3.SetActive(false);
        //mt.SetActive(false);
        //cameraFix.SetActive(false);
        //Npc.SetActive(false);

        PlayerPrefs.SetInt("health", playerHealth);
        PlayerPrefs.SetInt("money", playerCoins);
        PlayerPrefs.SetInt("storedAmmo", playerAmmo);

        PlayerPrefs.Save();

        SceneManager.LoadScene("Hub");

        this.enabled = false;

    }

    private IEnumerator DelayDisplayBlock(StoryBlock block)
    {

        yield return new WaitForSeconds(1);
        DisplayBlock(block);

    }

}