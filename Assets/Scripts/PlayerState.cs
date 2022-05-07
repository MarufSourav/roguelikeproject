using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class PlayerState : ScriptableObject
{
    public string gunType;
    public int magAmmo;
    public float fireRate;
    public float reloadTime;
    public float damage;    
}