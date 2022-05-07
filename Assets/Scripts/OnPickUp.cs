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
            if (gameObject.name == "PistolW (1)") 
            {
                ps.gunType = "Pistol";
                ps.magAmmo = 8;
                ps.fireRate = 3f;
                ps.reloadTime = 2.1f;
                ps.damage = 26f;                
            }                
            else if (gameObject.name == "RifleW (1)")
            {
                ps.gunType = "Rifle";
                ps.magAmmo = 20;
                ps.fireRate = 7f;
                ps.reloadTime = 1.7f;
                ps.damage = 14f;
            }
            else if (gameObject.name == "SniperW (1)")
            {
                ps.gunType = "Sniper";
                ps.magAmmo = 4;
                ps.fireRate = 0.8f;
                ps.reloadTime = 2.6f;
                ps.damage = 70f;
            }
            Destroy(gameObject);
        }
        
    }
}
