using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bannerDeath : MonoBehaviour
{

    public bannerManager manager;
    public GameObject chooseAnotherText;
    bool canErrorText = true;
    bool isDead = false;
    bool hasChosen = false;

    public string whichBanner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            if (hasChosen)
            {
                chooseBanner(whichBanner);
            }
        
    }

    public void setChosen(string chosenString)
    {
        whichBanner = chosenString;
    }
    public void setHasChosen(bool choice)
    {
        hasChosen = choice;
    }
    public void chooseBanner(string bannerType)
    {
        if(manager.getAmount(bannerType) <= 0)
        {
            StartCoroutine(errorText());
            StopCoroutine(errorText());
        }
        else if(manager.getAmount(bannerType) > 0)
        {
            manager.decrease(bannerType);
            isDead = false;
            hasChosen = false;
            whichBanner = "";
        }
    }

    public IEnumerator errorText()
    {
        if (canErrorText)
        {
            canErrorText = false;
            chooseAnotherText.SetActive(true);
            yield return new WaitForSeconds(3);
            chooseAnotherText.SetActive(false);
            canErrorText = true;
        }
    }
}
