using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    bool canOpen = true;
    Animator animator;
    public doorType type = doorType.NORMAL;
    key Key;

    public Material noKey;
    public Material hasKey;

    public List<GameObject> keySlots = new List<GameObject>();

    public enum doorType
    {
        NORMAL,
        BOSS,
    }
    private void Start()
    {
        Key = GameObject.FindGameObjectWithTag("Player").GetComponent<key>();

        switch (gameObject.tag)
        {
            case "normal door":
                type = doorType.NORMAL;
                break;

            case "boss door":
                type = doorType.BOSS;
                break;
        }
        
        animator = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        switch (type)
        {
            case doorType.BOSS:
                if (Key.getKey() > 0)
                {
                    keySlots[Key.getKey() - 1].GetComponent<MeshRenderer>().material = hasKey;
                }
                break;
        }

    }
    public void open()
    {
        if (canOpen)
        {
            switch (type)
            {
                case doorType.NORMAL:
                    canOpen = false;
                    animator.SetBool("isOpen", true);
                    break;
                case doorType.BOSS:
                    if (Key.getKey() == 4)
                    {
                        canOpen = false;
                        animator.SetBool("isOpen", true);
                    }
                    break;
            }    
        }
        
    }
}
