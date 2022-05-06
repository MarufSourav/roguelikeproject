using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Animator SHOOT;
    public Animator ADS;    
    public Animator ReloadMagazine;    
    public GameObject crosshair;
    public GameObject gunMuzzle;
    public float damage = 15f;
    public float range = 100f;
    public float fireRate = 15f;
    public float hitForce = 20f;
    public int magAmmo = 8;
    private bool Reloding = false;

    private float nextTimeToFire = 0f;

    void Update()
    {        
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && magAmmo != 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            gunShot();
        }
        else 
        {
            SHOOT.SetBool("isMouse0", false);                  
        }

        if (Input.GetButton("Fire2"))
        {
            ADS.SetBool("isMouse1", true);
            crosshair.SetActive(false);
        }
        else 
        {
            ADS.SetBool("isMouse1", false);
            crosshair.SetActive(true);
        }

        if (Input.GetButton("Fire3") && magAmmo < 8) 
        {
            SHOOT.SetBool("isButtonR", true);
            ReloadMagazine.SetBool("isButtonR", true);
            Reloding = true;
            Invoke("Reload", 2.1f);           
        }
        else
        {
            SHOOT.SetBool("isButtonR", false);
            ReloadMagazine.SetBool("isButtonR", false);
        }
    }
    private void gunShot() 
    {
        if (!Reloding) 
        {
            SHOOT.SetBool("isMouse0", true);
            magAmmo--;
            RaycastHit hit;
            if (Physics.Raycast(gunMuzzle.transform.position, gunMuzzle.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                EnemyMisc target = hit.transform.GetComponent<EnemyMisc>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce, ForceMode.Impulse);
                }
            }
        }        
    }
    private void Reload() 
    {
        magAmmo = 8;
        Reloding = false;
    }
}
