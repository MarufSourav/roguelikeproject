using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawnLogic : MonoBehaviour
{
    public PlayerState ps;
    GameObject spawnLocation;
    EnemyTypeSpawn enemyType;
    public GameObject rangeEnemy;
    public GameObject meleeEnemy;
    public float spawnTime;
    public float spawnDelay;
    int justSpawnedLocation;
    public int roundEnemy;
    // Start is called before the first frame update
    public void startSpawner(){
        roundEnemy = 0;
        InvokeRepeating("SpawnEnemy", spawnTime, spawnDelay);
    }
    public void stopSpawner(){
        CancelInvoke("SpawnEnemy");
    }
    public void SpawnEnemy() 
    {
        //if (justSpawnedLocation > 8)
        //justSpawnedLocation = 1;
        justSpawnedLocation = Random.Range(1, 9);
        if (justSpawnedLocation < 9)  
        {
            if (roundEnemy < FindObjectOfType<LevelLogic>().roundEnemy)
            {
                spawnLocation = GameObject.Find(justSpawnedLocation.ToString());
                enemyType = spawnLocation.GetComponent<EnemyTypeSpawn>();
                if (enemyType.meleeEnemy)
                    Instantiate(meleeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
                else
                    Instantiate(rangeEnemy, spawnLocation.transform.position, spawnLocation.transform.rotation);
                roundEnemy++;
            }
            else
                stopSpawner();
        }
        //justSpawnedLocation++;
    }
}
