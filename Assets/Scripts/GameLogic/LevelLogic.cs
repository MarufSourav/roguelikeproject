using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLogic : MonoBehaviour
{
    [Range(3, 20)] public int amountOfSpawn;    
    public int enemyAmount;
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
        enemyAmount = Random.Range(3, amountOfSpawn);
        end.SetActive(false);        
        ps.AmountToFrag = enemyAmount;
        for (int i = 1; i <= ps.AmountToFrag; i++){
            spawnLocation = GameObject.Find(Random.Range(1, amountOfSpawn).ToString());            
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
        }
    }
}
