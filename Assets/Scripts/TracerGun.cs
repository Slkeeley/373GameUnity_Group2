using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 20f;

    public float fireRate = 20f; 
    public Camera fpsCam;
    public int bullets;
    public int ammoCapacity; 
    public float reloadTime; 

    private float nextTimeToFire = 0f;
    private bool firing = false; 
    public GameObject muzzleFlash;
    // Start is called before the first frame update
    void Start()
    {
        muzzleFlash.SetActive(false);
        bullets = ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {

        if (bullets <= 0)
        {
            
            StartCoroutine(reload());
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                firing = true;
                bullets = bullets - 2;
                Shoot();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                firing = false;
                muzzleFlash.SetActive(false);
            }        
        }

        if(Input.GetKey(KeyCode.R))
        {
            StartCoroutine(reload());
        }
    }
    private void LateUpdate()
    {
        muzzleEffect();
    }

    void Shoot()
    {
        RaycastHit hit;
       if( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
          PropDestruct prop=  hit.transform.GetComponent<PropDestruct>();
            if(prop!=null)
            {
                Debug.Log("prop not null");
                prop.TakeDamage(damage);
            } 
        }      
    }

    void muzzleEffect()
    {
        if (firing) {  muzzleFlash.SetActive(true);}
        else{muzzleFlash.SetActive(false);}
    }
    IEnumerator reload()
    {
        firing = false;
        //play animation if we make one
        yield return new WaitForSeconds(reloadTime);
        bullets = ammoCapacity; 
    }
}
