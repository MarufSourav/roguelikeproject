using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EffectOnTrigger : MonoBehaviour
{
    public PlayerState ps;
    public GameObject WeaponRifle;
    private void Start()
    {
        if(!ps.riflePickedUp)
            WeaponRifle.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "RifleModel")
        {
            if (!ps.riflePickedUp) {
                WeaponRifle.transform.position = other.transform.position;
                WeaponRifle.SetActive(true);
                if (other.GetComponent<WeaponState>().defaultAmmo <= other.GetComponent<WeaponState>().maxAmmo * 0.25f)
                    WeaponRifle.GetComponent<RifleStateManager>().AmmoCounterRifle.color = Color.red;
                else
                    WeaponRifle.GetComponent<RifleStateManager>().AmmoCounterRifle.color = Color.grey;
                ps.gunType = "Rifle";
                ps.magAmmo = other.GetComponent<WeaponState>().defaultAmmo;
                ps.maxAmmo = other.GetComponent<WeaponState>().maxAmmo;
                ps.recoilAmount = -0.5f;
                ps.adsSpeed = 15f;
                ps.fireRate = 7f;
                ps.reloadTime = 1.5f;
                ps.damage = 20f;
                ps.spreadFactor = 0.02f;
                ps.riflePickedUp = true;
                GetComponentInChildren<RifleStateManager>().ReCalibrateMoveSpeed();
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Item") 
        {
            ps.numOfExtraJump += other.GetComponent<ItemState>().numOfExtraJump;
            ps.numOfDash += other.GetComponent<ItemState>().numOfDash;
            ps.invulnerableOnDash = other.GetComponent<ItemState>().invulnerableOnDash;
            ps.normalMoveSpeed += other.GetComponent<ItemState>().normalMoveSpeed;
            ps.magAmmo += other.GetComponent<ItemState>().magAmmo;
            ps.maxAmmo += other.GetComponent<ItemState>().maxAmmo;
            ps.fireRate += other.GetComponent<ItemState>().fireRate;
            ps.reloadTime += other.GetComponent<ItemState>().reloadTime;
            ps.spreadFactor += other.GetComponent<ItemState>().spreadFactor;
            ps.recoilAmount -= other.GetComponent<ItemState>().recoilAmount;
            ps.damage += other.GetComponent<ItemState>().damage;
            ps.dashIsParry = other.GetComponent<ItemState>().dashIsParry;
            ps.ammoOnParry = other.GetComponent<ItemState>().ammoOnParry;
            ps.slowOnParry = other.GetComponent<ItemState>().slowOnParry;
            GetComponent<PlayerMovement>().ReCalibrateDash();
            GetComponentInChildren<RifleStateManager>().ReCalibrateSpreadFactor();
            GetComponentInChildren<RifleStateManager>().ReCalibrateMoveSpeed();
            CheckLimit();
            if (SceneManager.GetActiveScene().buildIndex != 0)
                Destroy(other.gameObject);
        }
        if (other.name == "End"){
            SceneManager.LoadScene(0);
        }
    }
    void CheckLimit(){
        if(ps.reloadTime < 0)
            ps.reloadTime = 0f;
        if (ps.spreadFactor < 0)
            ps.spreadFactor = 0f;
        if (ps.recoilAmount > 0)
            ps.recoilAmount = 0f;
    }
}
