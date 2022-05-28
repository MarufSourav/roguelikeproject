using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemState : MonoBehaviour
{
    public PlayerState ps;
    public int numOfExtraJump;
    public int numOfDash;
    public bool invulnerableOnDash;
    public float normalMoveSpeed;
    public int magAmmo;
    public int maxAmmo;
    public float fireRate;
    public float reloadTime;
    public float spreadFactor;
    public float recoilAmount;
    public float damage;
    public bool dashIsParry;
    public bool ammoOnParry;
    public bool slowOnParry;
}