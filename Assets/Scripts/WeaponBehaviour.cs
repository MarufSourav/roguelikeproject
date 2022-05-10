using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponBehaviour : MonoBehaviour
{
    private float hitForce = 1f;
    public bool Reloding = false;
    public bool WeaponEquip = false;

    public PlayerState ps;    

    [Header("Animators")]
    public Animator SHOOT;
    public Animator ADS;
    public Animator ReloadMagazine;
    public Animator CamShake;

    public GameObject crosshair;
    public GameObject gunMuzzle;
    public GameObject gunDropSpawn;
    public GameObject hitmarker;

    [Header("Weapons")]
    public GameObject gunPistol;
    public GameObject gunRifle;
    public GameObject gunSniper;    

    [Header("WeaponAmmoCounter")]
    public TextMeshProUGUI AmmoCounterPistol;
    public TextMeshProUGUI AmmoCounterRifle;
    public TextMeshProUGUI AmmoCounterSniper;

    private float nextTimeToFire = 0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pistol" && WeaponEquip == false)
        {
            gunPistol.SetActive(true);
            gunRifle.SetActive(false);
            gunSniper.SetActive(false);
            WeaponEquip = true;
            ps.gunType = "Pistol";
            ps.magAmmo = 8;
            ps.fireRate = 3f;
            ps.reloadTime = 2.1f;
            ps.damage = 30f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Rifle" && WeaponEquip == false)
        {
            AmmoCounterRifle.text = ps.magAmmo.ToString(); ;
            gunPistol.SetActive(false);
            gunRifle.SetActive(true);
            gunSniper.SetActive(false);
            WeaponEquip = true;
            ps.gunType = "Rifle";
            ps.magAmmo = 20;
            ps.fireRate = 7f;
            ps.reloadTime = 1.7f;
            ps.damage = 14f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Sniper" && WeaponEquip == false)
        {
            AmmoCounterSniper.text = ps.magAmmo.ToString();
            crosshair.SetActive(false);
            gunPistol.SetActive(false);
            gunRifle.SetActive(false);
            gunSniper.SetActive(true);
            WeaponEquip = true;
            ps.gunType = "Sniper";
            ps.magAmmo = 4;
            ps.fireRate = 0.8f;
            ps.reloadTime = 2.6f;
            ps.damage = 70f;
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        if (ps.gunType == "Pistol")//Pistol ---------------------------------------------------------------- //
        {
            AmmoCounterPistol.text = ps.magAmmo.ToString();
            //Pistol Fire>>>>>>>>>>>>>>>>>>>>>>>                      
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && ps.magAmmo != 0 && Reloding == false)
            {
                nextTimeToFire = Time.time + 1f / ps.fireRate;
                gunShot();
            }
            else
            {
                SHOOT.SetBool("isMouse0", false);
                CamShake.SetBool("isMouse0", false);
            }
            //Pistol Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
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
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 8 && Reloding == false)
            {
                FindObjectOfType<AudioManager>().Play("PistolReloadSound");
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
            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>  

            if (Input.GetButtonDown("Drop"))
            {                
                crosshair.SetActive(false);
                WeaponEquip = false;
                Instantiate(gunPistol, gunDropSpawn.transform.position, gunPistol.transform.rotation);
                ps.gunType = " ";
            }
        }
        else if (ps.gunType == "Rifle") //Rifle ---------------------------------------------------------------- //
        {
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

            if (Input.GetButtonDown("Drop"))
            {
                crosshair.SetActive(false);
                ps.gunType = " ";
            }
        }
        else if (ps.gunType == "Sniper") //Sniper ---------------------------------------------------------------- //
        {
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
            if (Input.GetButtonDown("Drop"))
            {
                ps.gunType = " ";
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
        FindObjectOfType<AudioManager>().Play("PistolGunSound");
        if (ps.gunType == "Pistol")
        {
            SHOOT.SetBool("isMouse0", true);
            CamShake.SetBool("isMouse0", true);
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
            EnemyDamageZone target = hit.transform.GetComponent<EnemyDamageZone>();
            if (hit.transform.name == "Head")
            {
                hitmarker.gameObject.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.TakeDamage(ps.damage * 4);
            }
            else if (hit.transform.name == "Body")
            {
                hitmarker.gameObject.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.TakeDamage(ps.damage);
            }
            else if (hit.transform.name == "LegRight" || hit.transform.name == "LegLeft") 
            {
                hitmarker.gameObject.SetActive(true);
                Invoke("hitmarkerActive", .2f);
                target.TakeDamage(ps.damage * 0.5f);
            }
            if (hit.rigidbody != null) 
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce, ForceMode.Impulse);
            }
        }
    }
    private void hitmarkerActive()
    {
        hitmarker.gameObject.SetActive(false);
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
