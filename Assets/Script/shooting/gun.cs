using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class gun : MonoBehaviour
{
    private PlayerInput playerInput;

    public GameObject bullet;
    public Transform gunObject;

    public Animator animator;

    protected float bulSpeed = 3000f;
    protected bool canAttack = true;
    protected float attackSpeed = 0.5f;
    protected float maxAttackSpeed = 0.5f;
    protected float lastAttack = 0f;

    bool canSwap = true;

    protected int currentAmmo = 10;
    protected int maxAmmo = 10;
    protected int storedAmmo = 45;
    protected int maxStoredAmmo = 45;
    protected int reloadTime = 3;

    protected bool auto = false;

    bool reloading = false;

    bool canReload = true;

    bool canAutoShoot = true;

    bannerManager banner;

    bool canShootAnim = true;

    //public LittleMen playingGod;

    RecoilShake recoil;

    public AudioSource gunSound;

    private void Start()
    {
        banner = GetComponentInParent<bannerManager>();
        playerInput = GetComponentInParent<PlayerInput>();
        //playingGod = GameObject.FindGameObjectWithTag("ShopKeeper").GetComponent<LittleMen>();
    }
    // Update is called once per frame
    void Update()
    {
        attackSpeed = maxAttackSpeed - banner.getStats(3);




        //Debug.Log(currentAmmo + "/" + storedAmmo + " : " + maxAmmo);

        if (currentAmmo == 0 || playerInput.actions["Reload"].triggered)
        {

            if (canReload && storedAmmo != 0)
            {
                StartCoroutine(reload(reloadTime));
                StopCoroutine(reload(reloadTime));
            }
            
        }

        if (!canAttack && !reloading)
        {
            lastAttack += Time.deltaTime;

            if (lastAttack >= attackSpeed)
            {
                canAttack = true;
                lastAttack = 0f;
            }
        }
        if (!auto)
        {
            if (Input.GetMouseButtonDown(0) && canAttack && currentAmmo > 0)
            {

                gunSound.Play();
                if (canShootAnim)
                {
                    StartCoroutine(shootAnimation());
                    StopCoroutine(shootAnimation());
                }
                currentAmmo = currentAmmo - 1;

                canAttack = false;
                var BulletClone = Instantiate(bullet, gunObject.position, Quaternion.identity);

                RecoilShake.Instance.triggerRecoil(true);

                BulletClone.GetComponent<Rigidbody>().AddForce(gunObject.forward * bulSpeed);


            }
        }
        if (auto)
        {
            if (Input.GetMouseButton(0) && canAttack && !reloading)
            {
                if (canAutoShoot)
                {
                    StartCoroutine(autoShoot());
                    StopCoroutine(autoShoot());

                    if (canShootAnim)
                    {
                        StartCoroutine(shootAnimation());
                        StopCoroutine(shootAnimation());
                    }
                }
                

            }
        }

        //if (reloading)
        //{
        //    playingGod.setCanInteract(false);
        //}
        //else
        //{
        //    playingGod.setCanInteract(true);
        //}

    }

    IEnumerator autoShoot()
    {
        canSwap = false;
        currentAmmo = currentAmmo - 1;
        canAttack = false;
        canAutoShoot = false;
        var BulletClone = Instantiate(bullet, gunObject.position, Quaternion.identity);
        BulletClone.GetComponent<Rigidbody>().AddForce(gunObject.forward * bulSpeed);
        yield return new WaitForSeconds(0.2f);
        canAttack = true;
        canAutoShoot = true;
        canSwap = true;
    }

    IEnumerator reload(int reloadTime)
    {
        canSwap = false;
        canReload = false;
        reloading = true;
        canAttack = false;

        animator.SetBool("Reload", true);
        

        yield return new WaitForSeconds(reloadTime);
        animator.SetBool("Reload", false);
        canReload = true;
        reloading = false;
        canAttack = true;
        if (storedAmmo - (maxAmmo - currentAmmo) >= 0)
        {

            storedAmmo = storedAmmo - (maxAmmo - currentAmmo);
            currentAmmo = maxAmmo;

        }
        else
        {
            currentAmmo = currentAmmo + storedAmmo;
            storedAmmo = 0;
        }

        canSwap = true;
    }

    IEnumerator shootAnimation()
    {
        canShootAnim = false;
        animator.SetBool("Shoot", true);
        yield return new WaitForSeconds(attackSpeed);
        animator.SetBool("Shoot", false);
        canShootAnim = true;
    }

    public int getCurrAmmo()
    {
        return currentAmmo;
    }

    public int getStoredAmmo()
    {
        //Debug.LogError(storedAmmo);
        return storedAmmo;
    }

    public void setAmmo(int ammo)
    {
        if ((storedAmmo + ammo) <= maxStoredAmmo)
        {
            storedAmmo = storedAmmo + ammo;
        }
        else
        {
            storedAmmo = maxStoredAmmo;
        }
    }

    public void hardSetCurrAmmo(int ammo)
    {
        currentAmmo = ammo;
    }

    public void hardSetUnloadedAmmo(int ammo)
    {
        storedAmmo = ammo;
    }

    public bool getCanSwap()
    {
        return canSwap;
    }

}
