using UnityEngine;

public class RifleActiveState : RifleBaseState
{
    float nextTimeToFire;
    Vector3 defaultWeaponPostion;
    Vector3 adsWeaponPostion;
    public override void EnterState(RifleStateManager rifle){
        nextTimeToFire = 0f;
        defaultWeaponPostion = new Vector3(0.14f, -0.06f, 0.39f);
        adsWeaponPostion = new Vector3(0f, -0.038f, 0.39f);
    }
    public override void UpdateState(RifleStateManager rifle)
    {
        if (rifle.ps.magAmmo <= rifle.ps.maxAmmo * 0.25f)
            rifle.AmmoCounterRifle.color = Color.red;
        rifle.AmmoCounterRifle.text = rifle.ps.magAmmo.ToString();
        //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>
        rifle.RifleReload.transform.localPosition = Vector3.Lerp(rifle.RifleReload.transform.localPosition, Vector3.zero, (12f / rifle.ps.reloadTime) * Time.deltaTime);
        rifle.RifleReload.transform.localRotation = Quaternion.Slerp(rifle.RifleReload.transform.localRotation, Quaternion.Euler(0, 0, 0), (12f / rifle.ps.reloadTime) * Time.deltaTime);
        if (rifle.ps.magAmmo == 0){
            rifle.cameraResetTime = 15f;
            rifle.RifleShoot.transform.localPosition = Vector3.Lerp(rifle.RifleShoot.transform.localPosition, Vector3.zero, rifle.recoilReset * Time.deltaTime);
            rifle.MainCamera.transform.localRotation = Quaternion.Slerp(rifle.MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), rifle.cameraResetTime * Time.deltaTime);
        }            
        else{
            if (Input.GetButton("Fire1")){
                rifle.cameraResetTime = 0.1f;
                rifle.timePressed += 1f * Time.deltaTime;
                if (rifle.timePressed > 2f)
                    rifle.timePressed = 2f;

                if (Time.time >= nextTimeToFire)
                {
                    rifle.rifleGunShotAmmount++;
                    rifle.RifleShoot.transform.localPosition = new Vector3(0, 0, -0.02f);
                    nextTimeToFire = Time.time + 1f / rifle.ps.fireRate;
                    gunShot(rifle);
                }
                rifle.RifleShoot.transform.localPosition = Vector3.Lerp(rifle.RifleShoot.transform.localPosition, Vector3.zero, rifle.recoilReset * Time.deltaTime);                
            }
            else
            {
                rifle.cameraResetTime = 15f;
                rifle.rifleGunShotAmmount = 0;
                rifle.timePressed -= 3f * Time.deltaTime;
                if (rifle.timePressed < 0f)
                    rifle.timePressed = 0f;                
                rifle.RifleShoot.transform.localPosition = Vector3.Lerp(rifle.RifleShoot.transform.localPosition, Vector3.zero, rifle.recoilReset * Time.deltaTime);
                rifle.MainCamera.transform.localRotation = Quaternion.Slerp(rifle.MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), rifle.cameraResetTime * Time.deltaTime);
            }
        }
        //Rifle Fire>>>>>>>>>>>>>>>>>>>>>>>
        //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
        if (Input.GetButton("Fire2"))
        {
            rifle.transform.localPosition = Vector3.Lerp(rifle.transform.localPosition, adsWeaponPostion, rifle.ps.adsSpeed * Time.deltaTime);
            rifle.crosshair.SetActive(false);
            rifle.spreadFactor = 0f;
            rifle.ps.moveSpeed = 50f;
            rifle.GetComponentInParent<WeaponSway>().multiplier = 0f;
        }
        else
        {
            rifle.transform.localPosition = Vector3.Lerp(rifle.transform.localPosition, defaultWeaponPostion, rifle.ps.adsSpeed * Time.deltaTime);
            rifle.crosshair.SetActive(true);
            rifle.spreadFactor = rifle.ps.spreadFactor;
            rifle.ps.moveSpeed = 150f;
            rifle.GetComponentInParent<WeaponSway>().multiplier = 2f;
        }
        //Rifle ADS>>>>>>>>>>>>>>>>>>>>>>>>
        if (Input.GetButtonDown("Fire3") && rifle.ps.magAmmo < rifle.ps.maxAmmo)
            rifle.SwitchState(rifle.rifleReload);
    }
    void gunShot(RifleStateManager rifle)
    {
        if (!rifle.infiniteAmmo)
            rifle.ps.magAmmo--; 
        if (rifle.timePressed > 0.5f)
            rifle.spreadFactor *= rifle.timePressed * 4f;
        rifle.Effect.Effect();
        if (rifle.ps.gunType == "Rifle")
        {
            if (rifle.rifleGunShotAmmount >= 15)
                rifle.MainCamera.Rotate(new Vector3(rifle.ps.recoilAmount, 0, 0));
            else if (rifle.rifleGunShotAmmount >= 10)
                rifle.MainCamera.Rotate(new Vector3(rifle.ps.recoilAmount, -rifle.ps.recoilAmount, 0));
            else if (rifle.rifleGunShotAmmount >= 5)
                rifle.MainCamera.Rotate(new Vector3(rifle.ps.recoilAmount, rifle.ps.recoilAmount, 0));
            else
                rifle.MainCamera.Rotate(new Vector3(rifle.ps.recoilAmount, 0, 0));
            rifle.audioSystem.Play("RifleGunSound");
        }
    }
}