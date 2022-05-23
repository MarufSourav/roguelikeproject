using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public PlayerState ps;
    public int spawnenhance;
    public int spawnability;
    GameObject enhanceSpawner;
    GameObject abilitySpawner;

    public GameObject MaxAmmoIncrease;
    public GameObject MaxDashIncrease;
    public GameObject MaxJumpIncrease;
    
    public GameObject EnableDashParry;
    public GameObject EnableAmmoOnParry;
    public GameObject EnableLimitedInvulnerable;

    bool spawnedAbility;
    private void Start()
    {
        spawnedAbility = false;
        enhanceSpawner = GameObject.Find("enhanceSpawner");
        abilitySpawner = GameObject.Find("abilitySpawner");
    }
    public void SpawnEnhance() 
    {
        spawnenhance = Random.Range(1, 4);
        if (spawnenhance != 4) 
        {
            if (spawnenhance == 1)
                Instantiate(MaxAmmoIncrease, enhanceSpawner.transform.position, enhanceSpawner.transform.rotation);
            else if (spawnenhance == 2)
                Instantiate(MaxDashIncrease, enhanceSpawner.transform.position, enhanceSpawner.transform.rotation);
            else
                Instantiate(MaxJumpIncrease, enhanceSpawner.transform.position, enhanceSpawner.transform.rotation);
        }
    }
    public void SpawnAbility()
    {
        spawnedAbility = false;
        while (!spawnedAbility)
        {
            if (ps.dashIsParry && ps.ammoOnParry && ps.invulnerableOnInput)
                break;
            spawnability = Random.Range(1, 4);
            if (spawnability != 4)
            {
                if (spawnability == 1)
                    if (!ps.dashIsParry) 
                    {
                        Instantiate(EnableDashParry, abilitySpawner.transform.position, abilitySpawner.transform.rotation);
                        spawnedAbility = true;
                    }
                if (spawnability == 2)
                    if (!ps.ammoOnParry) 
                    {
                        Instantiate(EnableAmmoOnParry, abilitySpawner.transform.position, abilitySpawner.transform.rotation);
                        spawnedAbility = true;
                    }
                if (spawnability == 3)
                    if (!ps.invulnerableOnInput)
                    {
                        Instantiate(EnableLimitedInvulnerable, abilitySpawner.transform.position, abilitySpawner.transform.rotation);
                        spawnedAbility = true;
                    }                        
            }
        }
    }
}
