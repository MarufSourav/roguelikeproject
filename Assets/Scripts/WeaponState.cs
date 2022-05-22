using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponState : MonoBehaviour
{
    public PlayerState ps;
    public int defaultAmmo;    
    public void currentAmmo() 
    {        
        ps.magAmmo = defaultAmmo;
    }
}
