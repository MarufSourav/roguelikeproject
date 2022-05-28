using System.Collections;
using UnityEngine;
public class RifleReloadState : RifleBaseState
{
    bool alreadyPlayedP1;
    bool alreadyPlayedP2;
    public override void EnterState(RifleStateManager rifle){
        alreadyPlayedP1 = false;
        alreadyPlayedP2 = false;
        rifle.ps.moveSpeed = rifle.ps.normalMoveSpeed;
        rifle.crosshair.SetActive(true);
        rifle.cameraResetTime = 15f;
        rifle.rifleGunShotAmmount = 0;
        rifle.timePressed = 0f;
    }
    public override void UpdateState(RifleStateManager rifle)
    {
        //ADS Position Reset
        rifle.transform.localPosition = Vector3.Lerp(rifle.transform.localPosition, new Vector3(0.14f, -0.06f, 0.39f), rifle.ps.adsSpeed * Time.deltaTime);
        rifle.reloadTime += 1f * Time.deltaTime;
        //Shooting and Camera Position Reset
        rifle.RifleShoot.transform.localPosition = Vector3.Lerp(rifle.RifleShoot.transform.localPosition, Vector3.zero, rifle.recoilReset * Time.deltaTime);
        rifle.MainCamera.transform.localRotation = Quaternion.Slerp(rifle.MainCamera.transform.localRotation, Quaternion.Euler(0, 0, 0), rifle.cameraResetTime * Time.deltaTime);
        //Rifle Reload Position
        rifle.RifleReload.transform.localPosition = Vector3.Lerp(rifle.RifleReload.transform.localPosition, new Vector3(0.06f, 0f, 0f), (12f / rifle.ps.reloadTime) * Time.deltaTime);
        rifle.RifleReload.transform.localRotation = Quaternion.Slerp(rifle.RifleReload.transform.localRotation, Quaternion.Euler(0f, 0f, -35f), (12f / rifle.ps.reloadTime) * Time.deltaTime);
        //Magazine Position
        if (rifle.reloadTime > 0.3f && rifle.reloadTime < rifle.ps.reloadTime * 0.75f)
        {
            if (!alreadyPlayedP2) {
                rifle.audioSystem.Play("RifleReloadP2");
                alreadyPlayedP2 = true;
            }                
            rifle.RifleMag.transform.localPosition = Vector3.LerpUnclamped(rifle.RifleMag.transform.localPosition, new Vector3(0, -0.2f, -0.045f), (12f / rifle.ps.reloadTime) * Time.deltaTime);
        }
        else
            rifle.RifleMag.transform.localPosition = Vector3.LerpUnclamped(rifle.RifleMag.transform.localPosition, new Vector3(0, 0f, -0.045f), (12f / rifle.ps.reloadTime) * Time.deltaTime);

        if (rifle.reloadTime >= rifle.ps.reloadTime * 0.85f && !alreadyPlayedP1)
        {
            rifle.audioSystem.Play("RifleReloadP1");
            alreadyPlayedP1 = true;
        }
        if (rifle.reloadTime >= rifle.ps.reloadTime)
        {
            rifle.ps.magAmmo = rifle.ps.maxAmmo;
            rifle.reloadTime = 0f;            
            rifle.AmmoCounterRifle.color = Color.grey;
            rifle.SwitchState(rifle.rifleActive);            
        }
    }
}
