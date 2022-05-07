using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPickUp : MonoBehaviour
{
    public PlayerState ps;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player") 
        {
            if (gameObject.name == "PistolW") 
            {
                ps.gunType = "Pistol";
                ps.magAmmo = 8;
                ps.fireRate = 1.8f;
                ps.reloadTime = 2.1f;
                ps.damage = 26f;                
            }                
            else if (gameObject.name == "RifleW")
            {
                ps.gunType = "Rifle";
                ps.magAmmo = 20;
                ps.fireRate = 7f;
                ps.reloadTime = 1.7f;
                ps.damage = 14f;
            }
            else if (gameObject.name == "SniperW")
            {
                ps.gunType = "Sniper";
                ps.magAmmo = 4;
                ps.fireRate = 0.8f;
                ps.reloadTime = 2.6f;
                ps.damage = 70f;
            }           
        }        
    }
}
