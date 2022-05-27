using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeaponBehaviour : MonoBehaviour
{
    public bool Reloding = false;
    public bool WeaponEquip = false;
    public bool infiniteAmmo = false;
    public int rifleGunShotAmmount;
    public float spreadFactor;

    public GameObject MainCamera;
    public PlayerState ps;
    
    [Header("Scripts")]    
    public TrainingBots StartEndTraining;
    public EffectOnHit Effect;

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
    private void Start(){
        spreadFactor = ps.spreadFactor;
    }
    public void ReCalibrateSpreadFactor() { spreadFactor = ps.spreadFactor; }
    void Update()
    {
        if (ps.gunType == "Rifle")//Rifle ---------------------------------------------------------------- // 
        {
            AmmoCounterRifle.text = ps.magAmmo.ToString();
            if (ps.magAmmo > 5)
                AmmoCounterRifle.color = Color.grey;
            else
                AmmoCounterRifle.color = Color.red;

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
                rifleGunShotAmmount = 0;
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
                    rifleGunShotAmmount = 0;
                    timePressed -= 3f * Time.deltaTime;
                    if (timePressed < 0f) { timePressed = 0f; }
                    cameraResetTime = 15f;
                    RifleShoot.transform.localPosition = Vector3.Lerp(RifleShoot.transform.localPosition, Vector3.zero, recoilReset * Time.deltaTime);
                    MainCamera.transform.localRotation = Quaternion.Slerp(MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), cameraResetTime * Time.deltaTime);
                }
            }
            //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            if (Input.GetButton("Fire2") && Reloding == false)
            {
                gunRifle.transform.localPosition = Vector3.Lerp(gunRifle.transform.localPosition, adsWeaponPostion, ps.adsSpeed * Time.deltaTime);
                crosshair.SetActive(false);
                spreadFactor = 0f;
                ps.moveSpeed = 50f;
                GetComponentInChildren<WeaponSway>().multiplier = 0f;
            }
            else
            {
                gunRifle.transform.localPosition = Vector3.Lerp(gunRifle.transform.localPosition, defaultWeaponPostion, ps.adsSpeed * Time.deltaTime);
                crosshair.SetActive(true);
                spreadFactor = ps.spreadFactor;
                ps.moveSpeed = 150f;
                GetComponentInChildren<WeaponSway>().multiplier = 2f;
            }
            //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>           
            if (Input.GetButtonDown("Fire3") && ps.magAmmo < ps.maxAmmo && Reloding == false)
            {
                Reloding = true;
                cameraResetTime = 15f;
                rifleGunShotAmmount = 0;
                Invoke("Reload", ps.reloadTime);
            }
            if (Reloding)
            {
                timePressed = 0f;
                reloadTime += 1f * Time.deltaTime;
                RifleShoot.transform.localPosition = Vector3.Lerp(RifleShoot.transform.localPosition, Vector3.zero, recoilReset * Time.deltaTime);
                MainCamera.transform.localRotation = Quaternion.Slerp(MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), cameraResetTime * Time.deltaTime);
                if (reloadTime > 0.3f && reloadTime < ps.reloadTime * 0.75f)
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
            //Rifle Reload>>>>>>>>>>>>>>>>>>>>>
            //Rifle Parry Animation>>>>>>>>>>>>
            if (ps.parry)
            {
                RifleReload.transform.localPosition = new Vector3(-0.15f, 0f, 0f);
                RifleReload.transform.Rotate(new Vector3(0f, -6f, 0f));
            }
            //Rifle Parry Animation>>>>>>>>>>>>
            //Rifle Drop>>>>>>>>>>>>>>>>>>>>>>>
            /*
            if (Input.GetButtonDown("Drop"))
            {
                reloadTime = 0f;
                CancelInvoke("Reload");
                crosshair.SetActive(false);
                WeaponEquip = false;
                Reloding = false;
                RifleShoot.transform.localPosition = new Vector3(0, 0, 0f);
                RifleShoot.transform.Rotate(new Vector3(0f, 0f, 0f));
                MainCamera.transform.Rotate(new Vector3(0f, 0f, 0f));
                gunRifle.transform.localPosition = defaultWeaponPostion;
                
                gunRifle.GetComponent<WeaponState>().defaultAmmo = ps.magAmmo;
                gunRifle.GetComponent<WeaponState>().maxAmmo = ps.maxAmmo;
                Instantiate(gunRifle, gunDropSpawn.transform.position, gunDropSpawn.transform.rotation);
                ps.gunType = " ";                
            }*/
            //Rifle Drop>>>>>>>>>>>>>>>>>>>>>>>
        }
        else
        {
            gunPistol.SetActive(false);
            gunRifle.SetActive(false);
            ps.moveSpeed = 150f;
        }            
    }    
    public void SpreadMath(){        
        if (timePressed > 0.5f)
            spreadFactor *= timePressed * 4f;
    }
    private void gunShot(){
        rifleGunShotAmmount++;
        if (!infiniteAmmo)
            ps.magAmmo--;        
        SpreadMath();
        Effect.Effect();
        if (ps.gunType == "Pistol")
        {
            MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, 0, 0));
            FindObjectOfType<AudioManager>().Play("PistolGunSound");
            if (ps.magAmmo - 3 == 0)
                AmmoCounterPistol.color = Color.red;
        }
        else if (ps.gunType == "Rifle")
        {
            if (rifleGunShotAmmount >= 15)
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, 0, 0));
            else if (rifleGunShotAmmount >= 10)
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, -ps.recoilAmount, 0));
            else if (rifleGunShotAmmount >= 5)
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, ps.recoilAmount, 0));                
            else
                MainCamera.transform.Rotate(new Vector3(ps.recoilAmount, 0, 0));
            FindObjectOfType<AudioManager>().Play("RifleGunSound");
        }
    }
    private void Reload(){
        if (ps.gunType == "Pistol")
            ps.magAmmo = 8;
        else if (ps.gunType == "Rifle") 
        {
            ps.magAmmo = ps.maxAmmo;
            reloadTime = 0f;
        }
        Reloding = false;
    }
}
