using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GunShoot : MonoBehaviour
{
    private StarterAssetsInputs __input;
    private float bulletSpeed = 2000;
    public float damage = 10f;
    public float range = 100f;

    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject bulletprefab;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject muzzleFlash;

    // Start is called before the first frame update

    private void Awake()
    {
        // muzzleFlash = Instantiate(muzzleFlashPrefab, new Vector3(1.40543771f,2.05784941f,0.930000007f), Quaternion.identity);
    }

    void Start()
    {
        __input = transform.root.GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (__input.shoot)
        {
            Shoot();
            __input.shoot = false;
        }
    }

    void Shoot()
    {
        muzzleFlash.SetActive(true);
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            GameObject bullet = Instantiate(bulletprefab, hit.point, Quaternion.LookRotation(hit.normal));
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
            Destroy(bullet, 2);
        }
        StartCoroutine(muzzleFlashDisable());
    }

    IEnumerator muzzleFlashDisable()
    {
        yield return new WaitForSeconds(0.5f);
        muzzleFlash.SetActive(false);
    }

}