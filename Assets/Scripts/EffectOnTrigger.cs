using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class EffectOnTrigger : MonoBehaviour
{
    public PlayerState ps;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pistol" && !GetComponent<WeaponBehaviour>().WeaponEquip)
        {
            GetComponent<WeaponBehaviour>().gunPistol.SetActive(true);
            GetComponent<WeaponBehaviour>().gunRifle.SetActive(false);
            if (ps.magAmmo - 3 == 0)
                GetComponent<WeaponBehaviour>().AmmoCounterPistol.color = Color.red;
            GetComponent<WeaponBehaviour>().WeaponEquip = true;
            ps.magAmmo = GetComponent<WeaponBehaviour>().gunPistol.GetComponent<WeaponState>().currentWeaponAmmo;
            ps.gunType = "Pistol";
            ps.fireRate = 3f;
            ps.reloadTime = 2.1f;
            ps.damage = 30f;
            ps.spreadFactor = 0.05f;
            ps.moveSpeed = 140f;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Rifle" && !GetComponent<WeaponBehaviour>().WeaponEquip)
        {
            GetComponent<WeaponBehaviour>().gunPistol.SetActive(false);
            GetComponent<WeaponBehaviour>().gunRifle.SetActive(true);
            if (ps.magAmmo - 5 == 0)
                GetComponent<WeaponBehaviour>().AmmoCounterPistol.color = Color.red;
            GetComponent<WeaponBehaviour>().WeaponEquip = true;
            ps.magAmmo = GetComponent<WeaponBehaviour>().gunRifle.GetComponent<WeaponState>().currentWeaponAmmo;
            ps.gunType = "Rifle";
            ps.fireRate = 7f;
            ps.reloadTime = 2.1f;
            ps.damage = 20f;
            ps.spreadFactor = 0.01f;
            ps.moveSpeed = 110f;
            Destroy(other.gameObject);
        }
        if (other.name == "End"){ SceneManager.LoadScene(1); }
    }    
}
