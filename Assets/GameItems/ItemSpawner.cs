using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemSpawner : MonoBehaviour
{
    int spawnenhance;
    GameObject enhanceSpawner;
    /*
    public GameObject MaxAmmoIncrease;
    public GameObject MaxDashIncrease;
    public GameObject MaxJumpIncrease;
    
    public GameObject EnableDashParry;
    public GameObject EnableAmmoOnParry;
    public GameObject EnableLimitedInvulnerable;

    */
    public ItemStruct[] items;
    private void Start()
    {
        enhanceSpawner = GameObject.Find("enhanceSpawner");
    }
    public void SpawnEnhance() 
    {
        spawnenhance = Random.Range(0, items.Length);
        items[spawnenhance].item.GetComponent<SpriteRenderer>().sprite = items[spawnenhance].itemSprite;
        items[spawnenhance].item.GetComponent<ItemState>().numOfExtraJump = items[spawnenhance].numOfExtraJump;
        items[spawnenhance].item.GetComponent<ItemState>().numOfDash = items[spawnenhance].numOfDash;
        items[spawnenhance].item.GetComponent<ItemState>().dashCoolDown = items[spawnenhance].dashCoolDown;
        items[spawnenhance].item.GetComponent<ItemState>().moveSpeed = items[spawnenhance].moveSpeed;
        items[spawnenhance].item.GetComponent<ItemState>().magAmmo = items[spawnenhance].magAmmo;
        items[spawnenhance].item.GetComponent<ItemState>().maxAmmo = items[spawnenhance].maxAmmo;
        items[spawnenhance].item.GetComponent<ItemState>().fireRate = items[spawnenhance].fireRate;
        items[spawnenhance].item.GetComponent<ItemState>().reloadTime = items[spawnenhance].reloadTime;
        items[spawnenhance].item.GetComponent<ItemState>().spreadFactor = items[spawnenhance].spreadFactor;
        items[spawnenhance].item.GetComponent<ItemState>().recoilAmount = items[spawnenhance].recoilAmount;
        items[spawnenhance].item.GetComponent<ItemState>().damage = items[spawnenhance].damage;
        items[spawnenhance].item.GetComponent<ItemState>().dashIsParry = items[spawnenhance].dashIsParry;
        items[spawnenhance].item.GetComponent<ItemState>().ammoOnParry = items[spawnenhance].ammoOnParry;
        items[spawnenhance].item.GetComponent<ItemState>().invulnerableOnInput = items[spawnenhance].invulnerableOnInput;
        items[spawnenhance].item.GetComponent<ItemState>().parryCoolDown = items[spawnenhance].parryCoolDown;
        items[spawnenhance].item.GetComponent<ItemState>().invulnerabilityLength = items[spawnenhance].invulnerabilityLength;
        items[spawnenhance].item.GetComponent<ItemState>().invulnerabilityCoolDown = items[spawnenhance].invulnerabilityCoolDown;        
        Instantiate(items[spawnenhance].item, enhanceSpawner.transform.position, enhanceSpawner.transform.rotation);
    }    
}
