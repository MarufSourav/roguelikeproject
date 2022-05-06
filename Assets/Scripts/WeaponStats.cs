using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public PlayerState ps;
    private float damage;
    private float fireRate;    
    private void WeaponPickUp()
    {
        if (ps.gunID == 1) 
        {
            damage = 15f;
            fireRate = 4f;
        }
    }
}