using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponBehaviour : MonoBehaviour
{
    private float hitForce = 1f;    
    public bool Reloding = false;

    public PlayerState ps;

    [Header("Animators")]
    public Animator SHOOT;
    public Animator ADS;    
    public Animator ReloadMagazine;
    
    public GameObject crosshair;
    public GameObject gunMuzzle;

    [Header("Weapons")]
    public GameObject gunPistol;
    public GameObject gunRifle;
    public GameObject gunSniper;

    public TextMeshProUGUI AmmoCounterPistol;
    public TextMeshProUGUI AmmoCounterRifle;
    public TextMeshProUGUI AmmoCounterSniper;

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (ps.gunType == "Pistol")//Pistol ---------------------------------------------------------------- //
        {
            AmmoCounterPistol.text = ps.magAmmo.ToString();
            gunPistol.SetActive(true);
            gunRifle.SetActive(false);
            gunSniper.SetActive(false);
            //Pistol Fire>>>>>>>>>>>>>>>>>>>>>>>            
                      
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ps.magAmmo != 0 && Reloding == false)
            {
                nextTimeToFire = Time.time + 1f / ps.fireRate;
                gunShot();
            }
            else
            {
                SHOOT.SetBool("isMouse0", false);
            }

            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                ADS.SetBool("isMouse1", true);
                crosshair.SetActive(false);
            }
            else
            {
                ADS.SetBool("isMouse1", false);
                crosshair.SetActive(true);
            }

            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 8 && Reloding == false)
            {
                Reloding = true;
                SHOOT.SetBool("isButtonR", true);
                ReloadMagazine.SetBool("isButtonR", true);
                Invoke("Reload", ps.reloadTime);
            }
            else
            {
                if (Reloding == false)
                {
                    SHOOT.SetBool("isButtonR", false);
                    ReloadMagazine.SetBool("isButtonR", false);
                }
            }
        }
        else if (ps.gunType == "Rifle") //Rifle ---------------------------------------------------------------- //
        {
            AmmoCounterRifle.text = ps.magAmmo.ToString(); ;
            gunPistol.SetActive(false);
            gunRifle.SetActive(true);
            gunSniper.SetActive(false);
            //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && ps.magAmmo != 0 && Reloding == false)
            {
                nextTimeToFire = Time.time + 1f / ps.fireRate;
                gunShot();
            }            

            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2"))
            {
                crosshair.SetActive(false);
            }
            else
            {                
                crosshair.SetActive(true);
            }

            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 20 && Reloding == false)
            {
                Reloding = true;                
                Invoke("Reload", ps.reloadTime);
            }            
        }
        else if (ps.gunType == "Sniper") //Sniper ---------------------------------------------------------------- //
        {
            AmmoCounterSniper.text = ps.magAmmo.ToString();
            crosshair.SetActive(false);
            gunPistol.SetActive(false);
            gunRifle.SetActive(false);
            gunSniper.SetActive(true);            
            //Sniper Fire>>>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ps.magAmmo != 0 && Reloding == false)
            {
                nextTimeToFire = Time.time + 1f / ps.fireRate;
                gunShot();
            }

            /*Sniper ADS>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2"))
            {
                
            }
            else
            {
               
            }*/

            //Sniper Reload>>>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 4 && Reloding == false)
            {
                Reloding = true;                
                Invoke("Reload", ps.reloadTime);
            }            
        }
        else
        {
            gunPistol.SetActive(false);
            gunRifle.SetActive(false);
            gunSniper.SetActive(false);
        }
    }
    private void gunShot() 
    {
        ps.magAmmo--;
        if (ps.gunType == "Pistol")
        {
            SHOOT.SetBool("isMouse0", true);
            if (ps.magAmmo - 3 == 0)
                AmmoCounterPistol.color = Color.red;
        }
        else if (ps.gunType == "Rifle")
        {
            if (ps.magAmmo - 5 == 0)
                AmmoCounterRifle.color = Color.red;
        }
        else 
        {
            if (ps.magAmmo - 1 == 0)
                AmmoCounterSniper.color = Color.red;
        }

       
              
        RaycastHit hit;
        if (Physics.Raycast(gunMuzzle.transform.position, gunMuzzle.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            EnemyMisc target = hit.transform.GetComponent<EnemyMisc>();
            if (target != null)
            {
                target.TakeDamage(ps.damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce, ForceMode.Impulse);
            }
        }               
    }
    private void Reload() 
    {
        if (ps.gunType == "Pistol")
            ps.magAmmo = 8;
        else if (ps.gunType == "Rifle")
            ps.magAmmo = 20;
        else if (ps.gunType == "Sniper")
            ps.magAmmo = 4;

        AmmoCounterPistol.color = Color.white;
        AmmoCounterRifle.color = Color.white;
        AmmoCounterSniper.color = Color.white;
        Reloding = false;
    }
}
