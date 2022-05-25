using UnityEngine;
[System.Serializable]
public class ItemStruct
{
    public string Name;
    public GameObject item;
    public Sprite itemSprite;
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
}
