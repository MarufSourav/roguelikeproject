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
            WeaponRifle.transform.position = other.transform.position;
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
            ps.normalMoveSpeed = ps.moveSpeed;
            GetComponentInChildren<RifleStateManager>().ReCalibrateMoveSpeed();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Item") 
        {
            ps.numOfExtraJump += other.GetComponent<ItemState>().numOfExtraJump;
            ps.numOfDash += other.GetComponent<ItemState>().numOfDash;
            if(!ps.invulnerableOnDash)
                ps.invulnerableOnDash = other.GetComponent<ItemState>().invulnerableOnDash;
            ps.normalMoveSpeed += other.GetComponent<ItemState>().normalMoveSpeed;
            ps.magAmmo += other.GetComponent<ItemState>().magAmmo;
            ps.maxAmmo += other.GetComponent<ItemState>().maxAmmo;
            ps.fireRate += other.GetComponent<ItemState>().fireRate;
            ps.reloadTime += other.GetComponent<ItemState>().reloadTime;
            ps.spreadFactor += other.GetComponent<ItemState>().spreadFactor;
            ps.recoilAmount -= other.GetComponent<ItemState>().recoilAmount;
            ps.damage += other.GetComponent<ItemState>().damage;
            if(!ps.dashIsParry)
                ps.dashIsParry = other.GetComponent<ItemState>().dashIsParry;
            if(!ps.ammoOnParry)
                ps.ammoOnParry = other.GetComponent<ItemState>().ammoOnParry;
            if (!ps.slowOnParry)
                ps.slowOnParry = other.GetComponent<ItemState>().slowOnParry;
            GetComponent<PlayerMovement>().ReCalibrateDash();
            GetComponentInChildren<RifleStateManager>().ReCalibrateSpreadFactor();
            GetComponentInChildren<RifleStateManager>().ReCalibrateMoveSpeed();
            Destroy(other.gameObject);
        }
        if (other.name == "End"){
            SceneManager.LoadScene(2);
        }
    }    
}
