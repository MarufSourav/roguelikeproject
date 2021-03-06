using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerState : ScriptableObject
{
    //Training Range
    public int AmountToFrag;

    //Weapon ID
    public string gunType;

    //Movement Stats    
    public int numOfExtraJump;
    public int numOfDash;
    public float sensitivity;
    public float moveSpeed;
    public float adsMoveSpeed;
    public float normalMoveSpeed;

    //Dash Stats
    public float dashCoolDown;
    public float dashSpeed;
    public float jumpForce;

    //Weapon Stats
    public bool riflePickedUp;
    public int magAmmo;
    public int maxAmmo;
    public float adsSpeed;   
    public float fireRate;
    public float reloadTime;
    public float spreadFactor;
    public float recoilAmount;
    public float damage;

    //Parry Stats
    public bool parry;
    public bool readyToParry;
    public bool dashIsParry;
    public bool ammoOnParry;
    public bool slowOnParry;
    public float parryWindow;
    public float parryCoolDown;

    //Invulnerability Stats
    public bool invulnerable;
    public bool invulnerableOnDash;
}