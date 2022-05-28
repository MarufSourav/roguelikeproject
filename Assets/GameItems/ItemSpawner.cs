using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemSpawner : MonoBehaviour
{
    int spawnenhance;
    GameObject enhanceSpawner;
    public ItemStruct[] items;
    private void Start(){
        enhanceSpawner = GameObject.Find("enhanceSpawner");
    }
    public void SpawnItem() 
    {
        spawnenhance = Random.Range(0, items.Length);
        items[spawnenhance].item.GetComponent<SpriteRenderer>().sprite = items[spawnenhance].itemSprite;
        items[spawnenhance].item.GetComponent<ItemState>().numOfExtraJump = items[spawnenhance].numOfExtraJump;
        items[spawnenhance].item.GetComponent<ItemState>().numOfDash = items[spawnenhance].numOfDash;
        items[spawnenhance].item.GetComponent<ItemState>().invulnerableOnDash = items[spawnenhance].invulnerableOnDash;
        items[spawnenhance].item.GetComponent<ItemState>().normalMoveSpeed = items[spawnenhance].normalMoveSpeed;
        items[spawnenhance].item.GetComponent<ItemState>().magAmmo = items[spawnenhance].magAmmo;
        items[spawnenhance].item.GetComponent<ItemState>().maxAmmo = items[spawnenhance].maxAmmo;
        items[spawnenhance].item.GetComponent<ItemState>().fireRate = items[spawnenhance].fireRate;
        items[spawnenhance].item.GetComponent<ItemState>().reloadTime = items[spawnenhance].reloadTime;
        items[spawnenhance].item.GetComponent<ItemState>().spreadFactor = items[spawnenhance].spreadFactor;
        items[spawnenhance].item.GetComponent<ItemState>().recoilAmount = items[spawnenhance].recoilAmount;
        items[spawnenhance].item.GetComponent<ItemState>().damage = items[spawnenhance].damage;
        items[spawnenhance].item.GetComponent<ItemState>().dashIsParry = items[spawnenhance].dashIsParry;
        items[spawnenhance].item.GetComponent<ItemState>().ammoOnParry = items[spawnenhance].ammoOnParry;
        items[spawnenhance].item.GetComponent<ItemState>().slowOnParry = items[spawnenhance].slowOnParry;
        Instantiate(items[spawnenhance].item, enhanceSpawner.transform.position, enhanceSpawner.transform.rotation);
    }    
}
