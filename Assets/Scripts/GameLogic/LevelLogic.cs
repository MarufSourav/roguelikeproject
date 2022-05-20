using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    [Range(1, 20)] public int amountOfSpawn;
    [Range(1, 6)] public int amountToSpawn;
    public PlayerState ps;
    public GameObject rangeEnemy;
    public GameObject meleeEnemy;
    GameObject spawnLocation;
    EnemyTypeSpawn enemyType;    
    public GameObject end;
    void SpawnBots()
    {        
        if (enemyType.meleeEnemy)
        {
            Instantiate(meleeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
            enemyType.botActive = true;
        }
        else 
        {
            Instantiate(rangeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
            enemyType.botActive = true;
        }                    
    }
    private void Start()
    {
        end.SetActive(false);        
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
            end.SetActive(true);            
            Debug.Log("You Finished The Level");
        }
    }
}
