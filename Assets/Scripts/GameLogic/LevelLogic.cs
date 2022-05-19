using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
    [Range(1, 20)] public int amountOfSpawn;
    [Range(1, 6)] public int amountToSpawn;
    public PlayerState ps;
    public GameObject rangeEnemy;
    public GameObject meleeEnemy;
    GameObject spawnLocation;
    EnemyTypeSpawn enemyType;
    public  Light spotLight;
    void SpawnBots()
    {        
        if (enemyType.meleeEnemy)
        {
            Debug.Log("Spawned Enemy Type Melee");
            Instantiate(meleeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
            enemyType.botActive = true;
        }
        else 
        {
            Debug.Log("Spawned Enemy Type Range");
            Instantiate(rangeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
            enemyType.botActive = true;
        }                    
    }
    private void Start()
    {
        spotLight.enabled = false;
        ps.AmountToFrag = amountToSpawn;
        for (int i = 1; i <= amountToSpawn; i++){
            spawnLocation = GameObject.Find(Random.Range(1, amountOfSpawn).ToString());
            Debug.Log(spawnLocation.name);
            enemyType = spawnLocation.GetComponent<EnemyTypeSpawn>();            
            if (!enemyType.botActive)
                SpawnBots();
            else
                i--;
        }        
    }
    private void Update()
    {
        if (ps.AmountToFrag == 0) 
        {
            spotLight.enabled = true;
            Debug.Log("You Finished The Level");
        }
    }
}
