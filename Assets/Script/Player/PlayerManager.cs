using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public GameObject bannerSacrifice;
    public PauseGame pause;

    int _health = 3;
    int _money = 0;
    int _newMoney = 0;
    [SerializeField] private float _invincableDuration = 1f;

    public GameObject player;

    public GameObject bloodScreen;

    public GameObject playerDiedText;

    bannerManager banner;

    bool _invincable = false;

    public GameObject spawn;

    [SerializeField] private TextMeshProUGUI moneyDisplay;
    [SerializeField] private TextMeshProUGUI newMoneyDisplay;

    public GameObject _mainCamera;
    public GameObject _lockedCamera;

    public GameObject hubMusic;
    public GameObject levelMusic;

    bool canDedText = true;

    public AudioSource tookDamageSound;

    // Start is called before the first frame update
    void Start()
    {
        newMoneyDisplay.gameObject.SetActive(false);
        banner = GetComponent<bannerManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            //AddMoney(5);
            _mainCamera.SetActive(true);
            _lockedCamera.SetActive(false);
            GetComponent<PlayerController>().setNoLooking(true);
            bannerSacrifice.SetActive(false);
            GetComponent<CharacterController>().enabled = true;
        }
        DisplayMoney();

        CheckIfDead();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _health--;
        }
        
    }


    public void TakeDamage()
    {
        if (!_invincable)
        {
            StartCoroutine(TookDamage());
        }
    }

    private void CheckIfDead()
    {
        if (_health <= 0)
        {
            Die();
            
        }
    }

    public void Alive()
    {
        _mainCamera.SetActive(true);
        _lockedCamera.SetActive(false);
        GetComponent<PlayerController>().setNoLooking(true);
        bannerSacrifice.SetActive(false);
        GetComponent<CharacterController>().enabled = true;
    }

    private void Die()
    {
        if (banner.getHasAnyBanners())
        {
            //hubMusic.SetActive(true);
            levelMusic.SetActive(false);
            _mainCamera.SetActive(false);
            _lockedCamera.SetActive(true);
            //Time.timeScale = 0;
            GetComponent<PlayerController>().setNoLooking(false);
            bannerSacrifice.SetActive(true);
            GetComponent<CharacterController>().enabled = false;

            player.transform.position = spawn.transform.position;

            _health = 3;

            //Debug.Log("DED");
        }

        else
        {
            Application.Quit();
        }


    }

    private void DisplayMoney()
    {
        moneyDisplay.text = _money.ToString();
        if (_newMoney > 0)
        {
            StartCoroutine(TempMoneyDisplay());
        }
        

    }

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int health)
    {
        _health = health;
    }

    public int GetMoney()
    {
        return _money;
    }

    public void SetMoney(int money)
    {
         _money = money;
    }

    public void AddMoney(int money)
    {
        _newMoney = money;
    }

    IEnumerator TempMoneyDisplay()
    {

        newMoneyDisplay.gameObject.SetActive(true);
        newMoneyDisplay.text = _newMoney.ToString();
        yield return new WaitForSeconds(0.5f);
        _money += _newMoney;
        newMoneyDisplay.gameObject.SetActive(false);
        _newMoney = 0;

    }

    
    IEnumerator diedText()
    {
        canDedText = false;
        
        yield return new WaitForSeconds(5);
        playerDiedText.SetActive(false);
        GetComponent<CharacterController>().enabled = true;

        _mainCamera.SetActive(true);
        _lockedCamera.SetActive(false);
        canDedText = true;
    }

    IEnumerator TookDamage()
    {
        tookDamageSound.Play();
        _invincable = true;

        _health--;

        StartCoroutine(DamageFeedback());

        yield return new WaitForSeconds(_invincableDuration);

        _invincable = false;

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy_Bullet"))
        {
            TakeDamage();
        }
    }

    IEnumerator DamageFeedback()
    {
        bloodScreen.SetActive(true);
        CameraShake.Instance.DamageShake(5f, .3f);
        yield return new WaitForSeconds(1f);
        bloodScreen.SetActive(false);
    }
}
