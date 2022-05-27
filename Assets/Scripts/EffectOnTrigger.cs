using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EffectOnTrigger : MonoBehaviour
{
    public PlayerState ps;
    public WeaponBehaviour weapon;
    private void Start(){weapon = GetComponent<WeaponBehaviour>();}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rifle" && !weapon.WeaponEquip)
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
            ps.moveSpeed = 150f;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Item") 
        {
            ps.numOfExtraJump += other.GetComponent<ItemState>().numOfExtraJump;
            ps.numOfDash += other.GetComponent<ItemState>().numOfDash;
            ps.dashCoolDown += other.GetComponent<ItemState>().dashCoolDown;
            ps.moveSpeed += other.GetComponent<ItemState>().moveSpeed;
            ps.magAmmo += other.GetComponent<ItemState>().magAmmo;
            ps.maxAmmo += other.GetComponent<ItemState>().maxAmmo;
            ps.fireRate += other.GetComponent<ItemState>().fireRate;
            ps.reloadTime += other.GetComponent<ItemState>().reloadTime;
            ps.spreadFactor += other.GetComponent<ItemState>().spreadFactor;
            ps.recoilAmount -= other.GetComponent<ItemState>().recoilAmount;
            ps.damage += other.GetComponent<ItemState>().damage;
            ps.dashIsParry = other.GetComponent<ItemState>().dashIsParry;
            ps.ammoOnParry = other.GetComponent<ItemState>().ammoOnParry;
            ps.invulnerableOnInput = other.GetComponent<ItemState>().invulnerableOnInput;
            ps.parryCoolDown += other.GetComponent<ItemState>().parryCoolDown;
            ps.invulnerabilityLength += other.GetComponent<ItemState>().invulnerabilityLength;
            ps.invulnerabilityCoolDown += other.GetComponent<ItemState>().invulnerabilityCoolDown;
            other.GetComponent<ItemState>().checkstats();
            GetComponent<PlayerMovement>().ReCalibrateDash();
            GetComponent<WeaponBehaviour>().ReCalibrateSpreadFactor();
            Destroy(other.gameObject);
        }
        if (other.name == "End"){
            SceneManager.LoadScene(0);
        }
    }    
}
