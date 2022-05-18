using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponBehaviour : MonoBehaviour
{
    public bool shooting = false;
    public bool Reloding = false;
    public bool WeaponEquip = false;
    public bool infiniteAmmo = false;
    public PlayerState ps;

    [Header("Scripts")]
    public TrainingBots StartEndTraining;
    public EffectOnHit Effect;
    public MouseLook mlsCamera;
    public MouseLook mlsPlayer;

    public Animator CamShake;    
    [Header("PistolAnimators")]    
    public Animator PistolADS;
    public Animator PistolSHOOT;    
    public Animator PistolReloadMagazine;

    [Header("RifleAnimators")]        
    public Animator RifleADS;
    public Animator RifleSHOOT;
    public Animator RifleReloadMagazine;

    [Header("Weapons")]
    public GameObject gunPistol;      
    public GameObject gunRifle;
    public GameObject crosshair;
    public GameObject gunDropSpawn;

    [Header("Recoil")]    
    public float timePressed;


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
            if (ps.magAmmo - 3 == 0)
                AmmoCounterPistol.color = Color.red;
            WeaponEquip = true;
            ps.magAmmo = gunPistol.GetComponent<WeaponState>().currentWeaponAmmo;
            ps.gunType = "Pistol";
            ps.fireRate = 3f;
            ps.reloadTime = 2.1f;
            ps.damage = 30f;
            ps.spreadFactor = 0.05f;
            ps.moveSpeed = 140f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Rifle" && !WeaponEquip) 
        {
            gunPistol.SetActive(false);
            gunRifle.SetActive(true);
            if (ps.magAmmo - 5 == 0)
                AmmoCounterPistol.color = Color.red;
            WeaponEquip = true;
            ps.magAmmo = gunRifle.GetComponent<WeaponState>().currentWeaponAmmo;
            ps.gunType = "Rifle";            
            ps.fireRate = 7f;
            ps.reloadTime = 2.1f;
            ps.damage = 20f;
            ps.spreadFactor = 0.01f;
            ps.moveSpeed = 110f;
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
                shooting = true;
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
                if(!Input.GetButton("Fire1"))
                    shooting = false;
                PistolSHOOT.SetBool("isMouse0", false);
                CamShake.SetBool("PistolisMouse0", false);
            }
            //Pistol Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                //gunPistol.transform.position = startPosition;
                PistolADS.SetBool("isMouse1", true);                
                crosshair.SetActive(false);
                ps.spreadFactor = 0.0f;
                ps.moveSpeed = 80f;
            }
            else
            {                
                PistolADS.SetBool("isMouse1", false);                
                crosshair.SetActive(true);
                ps.spreadFactor = 0.05f;
                ps.moveSpeed = 140f;
            }
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 8 && Reloding == false && shooting == false)
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
                Reloding = false;
                gunPistol.GetComponent<WeaponState>().currentWeaponAmmo = ps.magAmmo;
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
                shooting = true;
                if (ps.magAmmo == 0)
                {
                    FindObjectOfType<AudioManager>().Play("RifleReloadSound");
                    Reloding = true;
                    RifleSHOOT.SetBool("isButtonR", true);
                    RifleReloadMagazine.SetBool("isButtonR", true);
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

                if (!Input.GetButton("Fire1")) 
                {
                    shooting = false;
                    timePressed = 1f;
                }
                    
                RifleSHOOT.SetBool("isMouse0", false);                
                CamShake.SetBool("RifleisMouse0", false);                
            }
            //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                RifleADS.SetBool("isMouse1", true);
                crosshair.SetActive(false);
                ps.spreadFactor = 0f;
                ps.moveSpeed = 50f;
            }
            else
            {
                RifleADS.SetBool("isMouse1", false);
                crosshair.SetActive(true);
                ps.spreadFactor = 0.01f;
                ps.moveSpeed = 110f;
            }
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 20 && Reloding == false && shooting == false)
            {
                FindObjectOfType<AudioManager>().Play("RifleReloadSound");
                RifleSHOOT.SetBool("isButtonR", true);
                RifleReloadMagazine.SetBool("isButtonR", true);
                Reloding = true;
                Invoke("Reload", ps.reloadTime);
            }
            else
            {
                if (Reloding == false)
                {
                    RifleSHOOT.SetBool("isButtonR", false);
                    RifleReloadMagazine.SetBool("isButtonR", false);
                }
            }
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>  
            //Rifle Drop>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButtonDown("Drop"))
            {
                crosshair.SetActive(false);
                WeaponEquip = false;
                Reloding = false;
                gunRifle.GetComponent<WeaponState>().currentWeaponAmmo = ps.magAmmo;
                Instantiate(gunRifle, gunDropSpawn.transform.position, gunDropSpawn.transform.rotation);
                ps.gunType = " ";                
            }
            //Rifle Drop>>>>>>>>>>>>>>>>>>>>>>>
        }
        else
        {
            gunPistol.SetActive(false);
            gunRifle.SetActive(false);
            ps.moveSpeed = 150f;
        }            
    }    
    public void SpreadMath() 
    {       
        if (timePressed >= 7f)
            timePressed = 7f;
        if (timePressed > 3f) 
            ps.spreadFactor *= timePressed;
    }
    private void gunShot()
    {
        
        if (!infiniteAmmo)
            ps.magAmmo--;
        
        if (ps.gunType == "Pistol")
        {
            gunPistol.GetComponent<WeaponState>().currentWeaponAmmo = ps.magAmmo;
            FindObjectOfType<AudioManager>().Play("PistolGunSound");
            PistolSHOOT.SetBool("isMouse0", true);
            CamShake.SetBool("PistolisMouse0", true);
            if (ps.magAmmo - 3 == 0)
                AmmoCounterPistol.color = Color.red;
        }
        else if (ps.gunType == "Rifle"){
            timePressed += 1f + Time.deltaTime;           
            SpreadMath();
            gunRifle.GetComponent<WeaponState>().currentWeaponAmmo = ps.magAmmo;
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
