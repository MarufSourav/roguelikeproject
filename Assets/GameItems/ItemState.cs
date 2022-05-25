using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour
{
    public PlayerState ps;
    public int numOfExtraJump;
    public int numOfDash;
    public float dashCoolDown;
    public float moveSpeed;
    public int magAmmo;
    public int maxAmmo;
    public float fireRate;
    public float reloadTime;
    public float spreadFactor;
    public float recoilAmount;
    public float damage;
    public bool dashIsParry;
    public bool ammoOnParry;
    public bool invulnerableOnInput;
    public float parryCoolDown;
    public float invulnerabilityLength;
    public float invulnerabilityCoolDown;
    public void checkstats() {
        if (ps.ammoOnParry){
            ps.maxAmmo++;
            ps.magAmmo++;
        }
        if (ps.dashIsParry)
            ps.numOfDash++;
        if (ps.invulnerableOnInput) {
            ps.numOfDash++;
            ps.maxAmmo++;
            ps.magAmmo++;
            ps.numOfExtraJump++;
        }
    }
}