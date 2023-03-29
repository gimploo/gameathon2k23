using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.UI;
using TMPro;

public class GunShoot : MonoBehaviour
{
    private StarterAssetsInputs __input;
    private float bulletSpeed = 2000;
    public float damage = 10f;
    public float range = 100f;

    [SerializeField] public int totalBullets;
    public int currentBulletsLeft;

    [SerializeField] private TMP_Text prompt;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject muzzleFlash;

    private bool isGunAvailable;

    // Start is called before the first frame update

    private void Awake()
    {
        currentBulletsLeft = totalBullets = 10;
        isGunAvailable = true;
        __input = transform.root.GetComponent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (currentBulletsLeft == 0)
            StartCoroutine(GunReload());

        if (Input.GetKeyDown(KeyCode.R)) {
            currentBulletsLeft = 0;
            StartCoroutine(GunReload());
        }

        if (__input.shoot && currentBulletsLeft != 0)
        {
            Shoot();
            __input.shoot = false;
        }
    }

    IEnumerator GunReload()
    {
        yield return new WaitForSeconds(2.0f);
        currentBulletsLeft = totalBullets;
        updateBulletPrompt();
    }

    void Shoot()
    {
        if (!isGunAvailable) return;

        muzzleFlash.SetActive(true);
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            GameObject bullet = Instantiate(bulletprefab, hit.point, Quaternion.LookRotation(hit.normal));
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            currentBulletsLeft -= 1;
            updateBulletPrompt();
            isGunAvailable = false;
        }
        StartCoroutine(muzzleFlashDisable());
        StartCoroutine(cooldownBeforeNextShot());
    }

    private void updateBulletPrompt()
    {
        prompt.text = System.String.Format("{0}/{1}", currentBulletsLeft, totalBullets);
    }

    IEnumerator cooldownBeforeNextShot()
    {
        yield return new WaitForSeconds(0.5f);
        isGunAvailable = true;
    }

    IEnumerator muzzleFlashDisable()
    {
        yield return new WaitForSeconds(0.5f);
        muzzleFlash.SetActive(false);
    }

}