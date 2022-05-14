using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponBehaviour : MonoBehaviour
{
    public bool Reloding = false;
    public bool WeaponEquip = false;
    public bool infiniteAmmo = false;

    public PlayerState ps;

    [Header("Scripts")]
    public TrainingBots StartEndTraining;
    public EffectOnHit Effect;

    public Animator CamShake;
    [Header("PistolAnimators")]    
    public Animator PistolADS;
    public Animator PistolSHOOT;    
    public Animator PistolReloadMagazine;

    [Header("RifleAnimators")]        
    public Animator RifleADS;
    public Animator RifleSHOOT;
    //public Animator RifleReloadMagazine;

    [Header("Weapons")]
    public GameObject gunPistol;      
    public GameObject gunRifle;
    public GameObject crosshair;
    public GameObject gunDropSpawn;

    [Header("WeaponAmmoCounter")]
    public TextMeshProUGUI AmmoCounterPistol;
    public TextMeshProUGUI AmmoCounterRifle;

    private float nextTimeToFire = 0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pistol" && !WeaponEquip)
        {
            gunPistol.SetActive(true);
            gunRifle.SetActive(false);
            WeaponEquip = true;
            ps.gunType = "Pistol";
            ps.magAmmo = 8;
            ps.fireRate = 3f;
            ps.reloadTime = 2.1f;
            ps.damage = 30f;
            ps.spreadFactor = 0.05f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Rifle" && !WeaponEquip) 
        {
            gunPistol.SetActive(false);
            gunRifle.SetActive(true);
            WeaponEquip = true;
            ps.gunType = "Rifle";
            ps.magAmmo = 20;
            ps.fireRate = 7f;
            ps.reloadTime = 2.1f;
            ps.damage = 20f;
            ps.spreadFactor = 0.01f;
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        if (ps.gunType == "Pistol")//Pistol ---------------------------------------------------------------- //
        {
            AmmoCounterPistol.text = ps.magAmmo.ToString();
            //Pistol Fire>>>>>>>>>>>>>>>>>>>>>>>                      
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && Reloding == false)
            {
                if (ps.magAmmo == 0)
                {
                    FindObjectOfType<AudioManager>().Play("PistolReloadSound");
                    Reloding = true;
                    PistolSHOOT.SetBool("isButtonR", true);
                    PistolReloadMagazine.SetBool("isButtonR", true);
                    Invoke("Reload", ps.reloadTime);
                }
                else
                {
                    nextTimeToFire = Time.time + 1f / ps.fireRate;
                    gunShot();
                }

            }
            else
            {
                PistolSHOOT.SetBool("isMouse0", false);
                CamShake.SetBool("PistolisMouse0", false);
            }
            //Pistol Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                PistolADS.SetBool("isMouse1", true);
                crosshair.SetActive(false);
                ps.spreadFactor = 0.0f;
            }
            else
            {
                PistolADS.SetBool("isMouse1", false);
                crosshair.SetActive(true);
                ps.spreadFactor = 0.05f;
            }
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 8 && Reloding == false)
            {
                FindObjectOfType<AudioManager>().Play("PistolReloadSound");
                Reloding = true;
                PistolSHOOT.SetBool("isButtonR", true);
                PistolReloadMagazine.SetBool("isButtonR", true);
                Invoke("Reload", ps.reloadTime);
            }
            else
            {
                if (Reloding == false)
                {
                    PistolSHOOT.SetBool("isButtonR", false);
                    PistolReloadMagazine.SetBool("isButtonR", false);
                }
            }
            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>  
            //Pistol Drop>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButtonDown("Drop"))
            {
                crosshair.SetActive(false);
                WeaponEquip = false;
                Instantiate(gunPistol, gunDropSpawn.transform.position, gunPistol.transform.rotation);
                ps.gunType = " ";
            }
            //Pistol Drop>>>>>>>>>>>>>>>>>>>>>>>
        }
        else if (ps.gunType == "Rifle")//Rifle ---------------------------------------------------------------- // 
        {
            AmmoCounterRifle.text = ps.magAmmo.ToString();
            //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>                      
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && Reloding == false)
            {
                if (ps.magAmmo == 0)
                {
                    FindObjectOfType<AudioManager>().Play("RifleReloadSound");
                    Reloding = true;
                    //RifleSHOOT.SetBool("isButtonR", true);
                    //RifleReloadMagazine.SetBool("isButtonR", true);
                    Invoke("Reload", ps.reloadTime);
                }
                else
                {
                    nextTimeToFire = Time.time + 1f / ps.fireRate;
                    gunShot();
                }
            }
            else
            {
                RifleSHOOT.SetBool("isMouse0", false);             
                CamShake.SetBool("RifleisMouse0", false);
            }
            //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                RifleADS.SetBool("isMouse1", true);
                crosshair.SetActive(false);
                ps.spreadFactor = 0.0f;
            }
            else
            {
                RifleADS.SetBool("isMouse1", false);
                crosshair.SetActive(true);
                ps.spreadFactor = 0.01f;
            }
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 20 && Reloding == false)
            {
                //FindObjectOfType<AudioManager>().Play("RifleReloadSound");
                Reloding = true;
                //RifleSHOOT.SetBool("isButtonR", true);
                //RifleReloadMagazine.SetBool("isButtonR", true);
                Invoke("Reload", ps.reloadTime);
            }
            else
            {
                if (Reloding == false)
                {
                    //RifleSHOOT.SetBool("isButtonR", false);
                    //RifleReloadMagazine.SetBool("isButtonR", false);
                }
            }
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>  
            //Rifle Drop>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButtonDown("Drop"))
            {
                crosshair.SetActive(false);
                WeaponEquip = false;
                Instantiate(gunRifle, gunDropSpawn.transform.position, gunRifle.transform.rotation);
                ps.gunType = " ";
            }
            //Rifle Drop>>>>>>>>>>>>>>>>>>>>>>>
        }
        else
        {
            gunPistol.SetActive(false);
            gunRifle.SetActive(false);
        }            
    }
    private void gunShot()
    {
        if (!infiniteAmmo)
            ps.magAmmo--;
        
        if (ps.gunType == "Pistol")
        {
            FindObjectOfType<AudioManager>().Play("PistolGunSound");
            PistolSHOOT.SetBool("isMouse0", true);
            CamShake.SetBool("PistolisMouse0", true);
            if (ps.magAmmo - 3 == 0)
                AmmoCounterPistol.color = Color.red;
        }
        else if (ps.gunType == "Rifle")
        {
            FindObjectOfType<AudioManager>().Play("RifleGunSound");
            RifleSHOOT.SetBool("isMouse0", true);
            CamShake.SetBool("RifleisMouse0", true);
            if (ps.magAmmo - 5 == 0)
                AmmoCounterRifle.color = Color.red;
        }
        Effect.Effect();
    }
    private void Reload()
    {
        if (ps.gunType == "Pistol")
            ps.magAmmo = 8;
        else if (ps.gunType == "Rifle")
            ps.magAmmo = 20;
        
        AmmoCounterPistol.color = Color.white;
        AmmoCounterRifle.color = Color.white;
        Reloding = false;
    }
}
