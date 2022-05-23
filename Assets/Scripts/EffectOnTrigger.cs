using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EffectOnTrigger : MonoBehaviour
{
    public PlayerState ps;
    public WeaponBehaviour weapon;
    private void Start(){
        weapon = GetComponent<WeaponBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pistol" && !weapon.WeaponEquip)
        {
            weapon.gunPistol.SetActive(true);
            weapon.gunRifle.SetActive(false);
            if (ps.magAmmo - 3 == 0)
                weapon.AmmoCounterPistol.color = Color.red;
            weapon.WeaponEquip = true;
            weapon.defaultWeaponPostion = weapon.gunPistol.transform.localPosition;
            weapon.adsWeaponPostion.x = 0.0023f;
            weapon.adsWeaponPostion.y = -0.027f;
            weapon.adsWeaponPostion.z = 0.607f;
            ps.magAmmo = weapon.gunPistol.GetComponent<WeaponState>().defaultAmmo;
            ps.recoilAmount = -2f;
            ps.gunType = "Pistol";
            ps.adsSpeed = 10f;
            ps.fireRate = 3f;
            ps.reloadTime = 2f;
            ps.damage = 30f;
            ps.spreadFactor = 0.05f;
            ps.moveSpeed = 140f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Rifle" && !weapon.WeaponEquip)
        {
            weapon.gunPistol.SetActive(false);
            weapon.gunRifle.SetActive(true);
            if (ps.magAmmo - 5 == 0)
                weapon.AmmoCounterPistol.color = Color.red;
            weapon.WeaponEquip = true;
            weapon.defaultWeaponPostion = weapon.gunRifle.transform.localPosition;
            weapon.adsWeaponPostion.x = 0f;
            weapon.adsWeaponPostion.y = -0.038f;
            weapon.adsWeaponPostion.z = 0.39f;
            ps.magAmmo = weapon.gunRifle.GetComponent<WeaponState>().defaultAmmo;
            ps.maxAmmo = weapon.gunRifle.GetComponent<WeaponState>().maxAmmo;
            ps.recoilAmount = -0.5f;
            ps.gunType = "Rifle";
            ps.adsSpeed = 15f;
            ps.fireRate = 7f;
            ps.reloadTime = 1.5f;
            ps.damage = 20f;
            ps.spreadFactor = 0.02f;
            ps.moveSpeed = 110f;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "MaxAmmoIncrease(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log("Ammo + 1");
            ps.maxAmmo++;
            ps.magAmmo++;
        }
        else if (other.gameObject.name == "MaxDashIncrease(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log("Dash + 1");
            ps.numOfDash++;
            FindObjectOfType<PlayerMovement>().ReCalibrateDash();
        }
        else if (other.gameObject.name == "MaxJumpIncrease(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log("Jump + 1");
            ps.numOfExtraJump++;
        }
        else if (other.gameObject.name == "EnableDashParry(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log("DashParry Active");
            ps.dashIsParry = true;
        }
        else if (other.gameObject.name == "EnableAmmoOnParry(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log("AmmoParry On Active");
            ps.ammoOnParry = true;
        }
        else if (other.gameObject.name == "EnableLimitedInvulnerable(Clone)")
        {
            Destroy(other.gameObject);
            Debug.Log("Invulnerability Active");
            ps.invulnerableOnInput = true;
        }
        if (other.name == "End"){
            SceneManager.LoadScene(0);
        }
    }    
}
