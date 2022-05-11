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

    [Header("Animators")]
    public Animator SHOOT;
    public Animator ADS;
    public Animator ReloadMagazine;
    public Animator CamShake;

    public GameObject crosshair;    
    public GameObject gunDropSpawn;    

    [Header("Weapons")]
    public GameObject gunPistol;      

    [Header("WeaponAmmoCounter")]
    public TextMeshProUGUI AmmoCounterPistol;    

    private float nextTimeToFire = 0f;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pistol" && WeaponEquip == false)
        {
            gunPistol.SetActive(true);            
            WeaponEquip = true;
            ps.gunType = "Pistol";
            ps.magAmmo = 8;
            ps.fireRate = 3f;
            ps.reloadTime = 2.1f;
            ps.damage = 30f;
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
        else
            gunPistol.SetActive(false);
    }
    private void gunShot()
    {
        if (!infiniteAmmo)
            ps.magAmmo--;
        
        if (ps.gunType == "Pistol")
        {
            FindObjectOfType<AudioManager>().Play("PistolGunSound");
            SHOOT.SetBool("isMouse0", true);
            CamShake.SetBool("isMouse0", true);
            if (ps.magAmmo - 3 == 0)
                AmmoCounterPistol.color = Color.red;
        }       
        Effect.Effect();
    }
    private void Reload()
    {
        if (ps.gunType == "Pistol")           
            ps.magAmmo = 8;                    
        
        AmmoCounterPistol.color = Color.white;        
        Reloding = false;
    }
}
