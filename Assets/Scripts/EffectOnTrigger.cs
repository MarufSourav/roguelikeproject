using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EffectOnTrigger : MonoBehaviour
{
    public PlayerState ps;
    public GameObject WeaponRifle;
    private void Start(){
        WeaponRifle.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rifle")
        {
            WeaponRifle.SetActive(true);
            if (other.GetComponent<WeaponState>().defaultAmmo <= other.GetComponent<WeaponState>().maxAmmo * 0.25f)
                WeaponRifle.GetComponent<RifleStateManager>().AmmoCounterRifle.color = Color.red;
            else
                WeaponRifle.GetComponent<RifleStateManager>().AmmoCounterRifle.color = Color.grey;
            ps.magAmmo = other.GetComponent<WeaponState>().defaultAmmo;
            ps.maxAmmo = other.GetComponent<WeaponState>().maxAmmo;
            ps.gunType = "Rifle";
            ps.recoilAmount = -0.5f;            
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
            GetComponentInChildren<RifleStateManager>().ReCalibrateSpreadFactor();
            Destroy(other.gameObject);
        }
        if (other.name == "End"){
            SceneManager.LoadScene(0);
        }
    }    
}
