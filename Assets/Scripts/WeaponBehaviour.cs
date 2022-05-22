using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponBehaviour : MonoBehaviour
{
    public bool Reloding = false;
    public bool WeaponEquip = false;
    public bool infiniteAmmo = false;
    public GameObject MainCamera;
    public PlayerState ps;

    [Header("Scripts")]    
    public TrainingBots StartEndTraining;
    public EffectOnHit Effect;
    public MouseLook mlsCamera;
    public MouseLook mlsPlayer;

    [Header("Animation")]
    public Vector3 defaultWeaponPostion;
    public Vector3 adsWeaponPostion;
    public GameObject PistolReload;
    public GameObject RifleReload;
    public GameObject PistolShoot;
    public GameObject RifleShoot;
    public GameObject PistolMag;
    public GameObject RifleMag;
    public float reloadTime;

    [Header("Weapons")]
    public GameObject gunPistol;
    public GameObject gunRifle;
    public GameObject crosshair;
    public GameObject gunDropSpawn;

    [Header("Recoil")]    
    public float timePressed;
    public float cameraResetTime;
    public float recoilReset;

    [Header("WeaponAmmoCounter")]
    public TextMeshProUGUI AmmoCounterPistol;
    public TextMeshProUGUI AmmoCounterRifle;

    private float nextTimeToFire = 0f;
    
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
                    Reloding = true;
                    Invoke("Reload", ps.reloadTime);
                }
                else
                {
                    PistolShoot.transform.localPosition = new Vector3(0, 0, -0.07f);
                    PistolShoot.transform.Rotate(new Vector3(-5f, 0, 0)); 
                    nextTimeToFire = Time.time + 1f / ps.fireRate;
                    gunShot();
                }
            }
            else 
            {
                cameraResetTime = 8f;
                PistolShoot.transform.localPosition = Vector3.Lerp(PistolShoot.transform.localPosition, new Vector3(0, -0.01f, -0.1f), recoilReset * Time.deltaTime);
                PistolShoot.transform.localRotation = Quaternion.Slerp(PistolShoot.transform.localRotation, Quaternion.Euler(0, 0, 0), recoilReset * Time.deltaTime);
                MainCamera.transform.localRotation = Quaternion.Slerp(MainCamera.transform.localRotation,Quaternion.Euler(0, 0, 0), cameraResetTime * Time.deltaTime);
            }
            //Pistol Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                GetComponentInChildren<WeaponSway>().multiplier = 0f;
                gunPistol.transform.localPosition = Vector3.Lerp(gunPistol.transform.localPosition, adsWeaponPostion, ps.adsSpeed * Time.deltaTime);
                crosshair.SetActive(false);
                if (ps.spreadFactor > 0.0f) 
                {
                    ps.spreadFactor -= 0.01f * ps.adsSpeed * Time.deltaTime * 2f;
                    if (ps.spreadFactor < 0)
                        ps.spreadFactor = 0f;
                }
                ps.moveSpeed = 80f;
            }
            else
            {
                GetComponentInChildren<WeaponSway>().multiplier = 2f;
                gunPistol.transform.localPosition = Vector3.Lerp(gunPistol.transform.localPosition, defaultWeaponPostion, ps.adsSpeed * Time.deltaTime);
                crosshair.SetActive(true);
                if (ps.spreadFactor < 0.05f)
                {
                    ps.spreadFactor += 0.01f * ps.adsSpeed * Time.deltaTime * 2;
                    if (ps.spreadFactor > 0.05f)ps.spreadFactor = 0.05f;
                }
                ps.moveSpeed = 140f;
            }
            //Pistol ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>
            if (Reloding)
            {
                reloadTime += 1f * Time.deltaTime;
                PistolReload.transform.localPosition = Vector3.Lerp(PistolReload.transform.localPosition, new Vector3(0.15f, 0f, 0f), (12f / ps.reloadTime) * Time.deltaTime);
                PistolReload.transform.localRotation = Quaternion.Slerp(PistolReload.transform.localRotation, Quaternion.Euler(0f, 0f, -50f), (12f / ps.reloadTime) * Time.deltaTime);
            }
            else
            {
                PistolReload.transform.localPosition = Vector3.Lerp(PistolReload.transform.localPosition, Vector3.zero, (12f / ps.reloadTime) * Time.deltaTime);
                PistolReload.transform.localRotation = Quaternion.Slerp(PistolReload.transform.localRotation, Quaternion.Euler(0, 0, 0), (12f / ps.reloadTime) * Time.deltaTime);
            }
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 8 && Reloding == false)
            {
                Reloding = true;
                Invoke("Reload", ps.reloadTime);
            }
            //Pistol Reload>>>>>>>>>>>>>>>>>>>>>  
            //Pistol Drop>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButtonDown("Drop"))
            {
                reloadTime = 0f;
                CancelInvoke("Reload");
                crosshair.SetActive(false);
                WeaponEquip = false;
                Reloding = false;
                gunPistol.GetComponent<WeaponState>().defaultAmmo = ps.magAmmo;
                Instantiate(gunPistol, gunDropSpawn.transform.position, gunPistol.transform.rotation);
                ps.gunType = " ";
            }
            //Pistol Drop>>>>>>>>>>>>>>>>>>>>>>>
        }
        else if (ps.gunType == "Rifle")//Rifle ---------------------------------------------------------------- // 
        {
            AmmoCounterRifle.text = ps.magAmmo.ToString();
            //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>
            if (ps.magAmmo == 0)
            {
                cameraResetTime = 15f;
                RifleShoot.transform.localPosition = Vector3.Lerp(RifleShoot.transform.localPosition, Vector3.zero, recoilReset * Time.deltaTime);
                MainCamera.transform.localRotation = Quaternion.Slerp(MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), cameraResetTime * Time.deltaTime);
                if (Input.GetButtonDown("Fire1"))
                {
                    Reloding = true;                                       
                    Invoke("Reload", ps.reloadTime);
                }
            }
            else 
            {
                if (Input.GetButton("Fire1"))
                {
                    if(!Reloding)
                        timePressed += 1f * Time.deltaTime;
                    if (timePressed > 2f)
                        timePressed = 2f;
                    if (Time.time >= nextTimeToFire && Reloding == false)
                    {
                        RifleShoot.transform.localPosition = new Vector3(0, 0, -0.02f);
                        cameraResetTime = 0.1f;
                        nextTimeToFire = Time.time + 1f / ps.fireRate;
                        gunShot();
                    }
                    RifleShoot.transform.localPosition = Vector3.Lerp(RifleShoot.transform.localPosition, Vector3.zero, recoilReset * Time.deltaTime);
                }
                else 
                {
                    timePressed -= 3f * Time.deltaTime;
                    if (timePressed < 0f) { timePressed = 0f; }
                    cameraResetTime = 15f;                    
                    MainCamera.transform.localRotation = Quaternion.Slerp(MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), cameraResetTime * Time.deltaTime);
                }
            }            
            //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                gunRifle.transform.localPosition = Vector3.Lerp(gunRifle.transform.localPosition, adsWeaponPostion, ps.adsSpeed * Time.deltaTime);
                crosshair.SetActive(false);
                ps.spreadFactor = 0f;
                ps.moveSpeed = 50f;
                GetComponentInChildren<WeaponSway>().multiplier = 0f;
            }
            else
            {
                gunRifle.transform.localPosition = Vector3.Lerp(gunRifle.transform.localPosition, defaultWeaponPostion, ps.adsSpeed * Time.deltaTime);
                crosshair.SetActive(true);
                ps.spreadFactor = 0.01f;
                ps.moveSpeed = 110f;
                GetComponentInChildren<WeaponSway>().multiplier = 2f;
            }
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>
           
            if (Reloding)
            {
                timePressed = 0f;
                reloadTime += 1f * Time.deltaTime;
                RifleShoot.transform.localPosition = Vector3.Lerp(RifleShoot.transform.localPosition, Vector3.zero, recoilReset * Time.deltaTime);
                MainCamera.transform.localRotation = Quaternion.Slerp(MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), cameraResetTime * Time.deltaTime);
                if (reloadTime > 0.3f  && reloadTime < ps.reloadTime * 0.75f)
                    RifleMag.transform.localPosition = Vector3.LerpUnclamped(RifleMag.transform.localPosition, new Vector3(0, -0.2f, -0.045f), (12f / ps.reloadTime) * Time.deltaTime);
                else
                    RifleMag.transform.localPosition = Vector3.LerpUnclamped(RifleMag.transform.localPosition, new Vector3(0, 0f, -0.045f), (12f / ps.reloadTime) * Time.deltaTime);

                RifleReload.transform.localPosition = Vector3.Lerp(RifleReload.transform.localPosition, new Vector3(0.06f, 0f, 0f), (12f / ps.reloadTime) * Time.deltaTime);
                RifleReload.transform.localRotation = Quaternion.Slerp(RifleReload.transform.localRotation, Quaternion.Euler(0f, 0f, -35f), (12f / ps.reloadTime) * Time.deltaTime);               
            }
            else
            {
                RifleReload.transform.localPosition = Vector3.Lerp(RifleReload.transform.localPosition, Vector3.zero, (12f / ps.reloadTime) * Time.deltaTime);
                RifleReload.transform.localRotation = Quaternion.Slerp(RifleReload.transform.localRotation, Quaternion.Euler(0, 0, 0), (12f / ps.reloadTime) * Time.deltaTime);
            } 
            
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < 20 && Reloding == false)
            {
                Reloding = true;
                cameraResetTime = 15f;
                RifleShoot.transform.localPosition = Vector3.Lerp(RifleShoot.transform.localPosition, Vector3.zero, recoilReset * Time.deltaTime);
                MainCamera.transform.localRotation = Quaternion.Slerp(MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), cameraResetTime * Time.deltaTime);
                Invoke("Reload", ps.reloadTime);
            }
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>  
            //Rifle Drop>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButtonDown("Drop"))
            {
                reloadTime = 0f;
                CancelInvoke("Reload");
                crosshair.SetActive(false);
                WeaponEquip = false;
                Reloding = false;
                gunRifle.GetComponent<WeaponState>().defaultAmmo = ps.magAmmo;
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
        if (timePressed > 0.5f)
            ps.spreadFactor *= timePressed * 4f;
    }
    private void gunShot()
    {        
        if (!infiniteAmmo)
            ps.magAmmo--;        
        SpreadMath();
        Effect.Effect();
        if (ps.gunType == "Pistol")
        {
            MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, 0, 0));
            gunPistol.GetComponent<WeaponState>().defaultAmmo = ps.magAmmo;
            FindObjectOfType<AudioManager>().Play("PistolGunSound");
            if (ps.magAmmo - 3 == 0)
                AmmoCounterPistol.color = Color.red;
        }
        else if (ps.gunType == "Rifle")
        {
            if (timePressed == 2f)
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, 0, 0));
            else if (timePressed > 1.5f)
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, -ps.recoilAmount, 0));
            else if (timePressed > 1f)
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, ps.recoilAmount, 0));
            else
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, 0, 0));
            gunRifle.GetComponent<WeaponState>().defaultAmmo = ps.magAmmo;
            FindObjectOfType<AudioManager>().Play("RifleGunSound");
            if (ps.magAmmo - 5 == 0)
                AmmoCounterRifle.color = Color.red;
        }
    }
    
    private void Reload()
    {
        if (ps.gunType == "Pistol")
            ps.magAmmo = 8;
        else if (ps.gunType == "Rifle") 
        {
            ps.magAmmo = 20;
            reloadTime = 0f;
        }
            

        AmmoCounterPistol.color = Color.white;
        AmmoCounterRifle.color = Color.white;
        Reloding = false;
    }
}
